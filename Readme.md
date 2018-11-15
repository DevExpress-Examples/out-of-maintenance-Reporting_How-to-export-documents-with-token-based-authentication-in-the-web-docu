# Web Document Viewer - How to export documents in an application with token-based authentication

The [Web Document Viewer](https://documentation.devexpress.com/AspNet/114491/ASP-NET-MVC-Extensions/Reporting/Document-Viewer/HTML5-Document-Viewer) uses AJAX requests for most API calls excluding the case when a web browser requests the export or print result. In the latter cases, the Web Document Viewer uses the unique identifier for each export operation by default.

The [WebDocumentViewerApiController](https://documentation.devexpress.com/AspNet/DevExpress.Web.Mvc.Controllers.WebDocumentViewerApiController.Invoke.method)'s **Invoke** action processes all requests from the Web Document Viewer. In this example, access to this action is restricted using the [Bearer authentication](https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/individual-accounts-in-web-api).
The **IWebDocumentViewerExportResultUriGenerator** service allows you to override the mechanism of getting export results. 
This example demonstrates how to use Bearer-based authentication in ASP.NET MVC applications and export documents in it.

To accomplish this task, do the following:
- Set up Bearer token authentication.- Assign the [Authorize](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.authorizeattribute) attribute to the Web Document Viewer controller's **Invoke** action.
- Implement the **IWebDocumentViewerExportResultUriGenerator** interface and register it in the service container. In the **CreateUri** method, save an exported document to any storage and return the URI to access it from the client side.
- Enable the asynchronous export mechanism on the Web Document Viewer's client-side.
- Apply the Bearer token to all the reporting AJAX requests.

When the application runs, you should register in the system, log in to it, and open the test report.