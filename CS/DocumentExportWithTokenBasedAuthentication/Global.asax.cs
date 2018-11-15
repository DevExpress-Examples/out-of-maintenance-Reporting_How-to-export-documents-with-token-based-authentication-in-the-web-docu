using DevExpress.XtraReports.Web.WebDocumentViewer;
using T680906.Services;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace T680906
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.ReadOnly;
            DefaultWebDocumentViewerContainer.Register<IWebDocumentViewerReportResolver, ReportResolver>();
            var exportedDocumentService = new ExportedDocumentService(Server.MapPath("~/App_Data/ExportedDocuments/"), "/ReportViewer/GetExportResult");
            DefaultWebDocumentViewerContainer.RegisterSingleton<IWebDocumentViewerExportResultUriGenerator>(exportedDocumentService);
            DefaultWebDocumentViewerContainer.RegisterSingleton<IExportResultProvider>(exportedDocumentService);

            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DevExpress.Web.Mvc.MVCxWebDocumentViewer.StaticInitialize();
        }
    }
}
