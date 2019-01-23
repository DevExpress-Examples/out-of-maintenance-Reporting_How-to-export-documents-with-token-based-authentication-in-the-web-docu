Imports DevExpress.XtraReports.Web.WebDocumentViewer
Imports T680906.Services
Imports System.Web.Http
Imports System.Web.Mvc
Imports System.Web.Optimization
Imports System.Web.Routing

Namespace T680906
	Public Class WebApiApplication
		Inherits System.Web.HttpApplication

		Protected Sub Application_Start()
			DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.ReadOnly
			DefaultWebDocumentViewerContainer.Register(Of IWebDocumentViewerReportResolver, ReportResolver)()
			Dim exportedDocumentService = New ExportedDocumentService(Server.MapPath("~/App_Data/ExportedDocuments/"), "/ReportViewer/GetExportResult")
			DefaultWebDocumentViewerContainer.RegisterSingleton(Of IWebDocumentViewerExportResultUriGenerator)(exportedDocumentService)
			DefaultWebDocumentViewerContainer.RegisterSingleton(Of IExportResultProvider)(exportedDocumentService)


			AreaRegistration.RegisterAllAreas()
			GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
			RouteConfig.RegisterRoutes(RouteTable.Routes)
			BundleConfig.RegisterBundles(BundleTable.Bundles)

			DevExpress.Web.Mvc.MVCxWebDocumentViewer.StaticInitialize()
		End Sub
	End Class
End Namespace
