Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Web.WebDocumentViewer
Imports T680906.Reports

Namespace T680906.Services
	Public Class ReportResolver
		Implements IWebDocumentViewerReportResolver

		Public Function Resolve(ByVal reportEntry As String) As XtraReport Implements IWebDocumentViewerReportResolver.Resolve
			If reportEntry = "testReport" Then
				Return New XtraReport1()
			End If
			Return Nothing
		End Function
	End Class
End Namespace