Imports DevExpress.Web.Mvc.Controllers
Imports DevExpress.XtraReports.Web.WebDocumentViewer
Imports T680906.Services
Imports System.Web.Mvc

Namespace T680906.Controllers
	Public Class ReportViewerController
		Inherits WebDocumentViewerApiController

		Private documentExportService As IExportResultProvider
		Public Sub New()
			documentExportService = DirectCast(DefaultWebDocumentViewerContainer.Current.GetService(GetType(IExportResultProvider)), IExportResultProvider)
		End Sub

		<Authorize>
		Public Overrides Function Invoke() As ActionResult
			Return MyBase.Invoke()
		End Function

		<HttpGet>
		Public Function GetExportResult(ByVal token As String, ByVal fileName As String) As ActionResult
			Dim exportResult As ExportResult = Nothing
			If Not documentExportService.TryGetExportResult(token, exportResult) Then
				Return New HttpNotFoundResult("Exported document was not found. Try to export the document once again.")
			End If
			Dim fileResult = File(exportResult.GetBytes(), exportResult.ContentType)
			If exportResult.ContentDisposition <> System.Net.Mime.DispositionTypeNames.Inline Then
				fileResult.FileDownloadName = exportResult.FileName
			End If

			Return fileResult
		End Function
	End Class
End Namespace