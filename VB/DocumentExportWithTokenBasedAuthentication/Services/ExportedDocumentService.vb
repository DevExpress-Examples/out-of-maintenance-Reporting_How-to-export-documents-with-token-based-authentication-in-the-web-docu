Imports DevExpress.XtraReports.Web.ClientControls
Imports DevExpress.XtraReports.Web.WebDocumentViewer
Imports Newtonsoft.Json
Imports System
Imports System.Collections.Concurrent
Imports System.IO
Imports System.Net.Mime
Imports System.Security.Cryptography
Imports System.Security.Policy

Namespace T680906.Services
	Public Interface IExportResultProvider
		Function TryGetExportResult(ByVal oneTimeToken As String, ByRef exportResult As ExportResult) As Boolean
	End Interface
	Public Class ExportedDocumentService
		Implements IWebDocumentViewerExportResultUriGenerator, IExportResultProvider

		Private ReadOnly basePath As String
		Private ReadOnly baseUrl As String
		Private Const metaFileExt As String = ".meta"
		Private Const dataFileExt As String = ".data"

		Private documents As New ConcurrentDictionary(Of String, ExportResult)()
		Public Sub New(ByVal basePath As String, ByVal baseUrl As String)
			Me.basePath = basePath
			Me.baseUrl = baseUrl
			If Not Directory.Exists(basePath) Then
				Directory.CreateDirectory(basePath)
			End If
		End Sub
		Public Function CreateUri(ByVal exportOperationId As String, ByVal exportedDocument As ExportedDocument) As String Implements IWebDocumentViewerExportResultUriGenerator.CreateUri
			Dim oneTimeToken = GetOneTimeAccessToken()
			Dim exportResult = New ExportResult() With {
				.FileName = exportedDocument.FileName,
				.ExportOperationId = exportOperationId,
				.ContentType = exportedDocument.ContentType,
				.ContentDisposition = If(exportedDocument.ContentDisposition, DispositionTypeNames.Attachment)
			}
			exportResult.AssignBytes(exportedDocument.Bytes)
			SaveInMemory(oneTimeToken, exportResult)
			SaveToFile(oneTimeToken, exportResult)

			Return baseUrl & "?token=" & oneTimeToken
		End Function
		Public Function TryGetExportResult(ByVal oneTimeToken As String, <System.Runtime.InteropServices.Out()> ByRef exportResult As ExportResult) As Boolean Implements IExportResultProvider.TryGetExportResult
			Return TryLoadFromMemory(oneTimeToken, exportResult)
			'return TryLoadFromFile(oneTimeToken, out exportResult);
		End Function
		Private Sub SaveInMemory(ByVal oneTimeToken As String, ByVal exportResult As ExportResult)
			documents.AddOrUpdate(oneTimeToken, exportResult, Function(_id, _result) exportResult)
		End Sub

		Private Sub SaveToFile(ByVal oneTimeToken As String, ByVal exportResult As ExportResult)
			Dim jsonString = JsonConvert.SerializeObject(exportResult)
			File.WriteAllText(Path.Combine(basePath, oneTimeToken & metaFileExt), jsonString)

			'using (var fileWriter = File.CreateText(Path.Combine(basePath, oneTimeToken + metaFileExt))) {
			'    new JsonSerializer().Serialize(fileWriter, exportResult);
			'}
			File.WriteAllBytes(Path.Combine(basePath, oneTimeToken & dataFileExt), exportResult.GetBytes())
		End Sub
		Private Function TryLoadFromMemory(ByVal oneTimeToken As String, ByRef exportResult As ExportResult) As Boolean
			Return documents.TryRemove(oneTimeToken, exportResult)
		End Function

		Private Function TryLoadFromFile(ByVal oneTimeToken As String, ByRef exportResult As ExportResult) As Boolean
			Dim metaFilePath = Path.Combine(basePath, oneTimeToken & metaFileExt)
			If File.Exists(metaFilePath) Then
				Dim metaJson = File.ReadAllText(metaFilePath)
				exportResult = JsonConvert.DeserializeObject(Of ExportResult)(metaJson)
				File.WriteAllBytes(Path.Combine(basePath, oneTimeToken & dataFileExt), exportResult.GetBytes())
				Dim data = File.ReadAllBytes(Path.Combine(basePath, oneTimeToken & dataFileExt))
				exportResult.AssignBytes(data)
				Return True
			End If
			exportResult = Nothing
			Return False
		End Function

		Private Function GetOneTimeAccessToken() As String
			Dim data(15) As Byte
			Using rngCryptoServiceProvider = New RNGCryptoServiceProvider()
				rngCryptoServiceProvider.GetBytes(data)
			End Using
			Return (New Guid(data)).ToString("N")
		End Function
	End Class

	Public Class ExportResult
		Private documentBytes() As Byte
		Public Property FileName() As String
		Public Property ContentType() As String
		Public Property ExportOperationId() As String
		Public Property ContentDisposition() As String
		Private privateTimeStamp As Date
		Public Property TimeStamp() As Date
			Get
				Return privateTimeStamp
			End Get
			Private Set(ByVal value As Date)
				privateTimeStamp = value
			End Set
		End Property
		Public Sub New()
			TimeStamp = Date.UtcNow
		End Sub
		Public Sub AssignBytes(ByVal data() As Byte)
			documentBytes = data
		End Sub
		Public Function GetBytes() As Byte()
			Return documentBytes
		End Function
	End Class
End Namespace