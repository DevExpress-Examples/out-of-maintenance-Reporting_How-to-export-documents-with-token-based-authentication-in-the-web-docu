<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/157709892/21.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T828950)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Web Document Viewer - How to export documents in an application with token-based authentication

For all API calls except print and export operations, the [Web Document Viewer](https://docs.devexpress.com/XtraReports/400221/web-reporting/asp-net-mvc-reporting/document-viewer-in-asp-net-mvc-applications) sends AJAX requests to the server. For print and export operations, a _web browser_ requests the result from the server, but these requests have no headers. On the server side, the [WebDocumentViewerApiController](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.Controllers.WebDocumentViewerApiController)'s **Invoke** action processes all requests. 

When your application uses header-token authentication, this controller action is protected (for instance, with a [Bearer authentication](https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/individual-accounts-in-web-api)). A web browser cannot obtain export and print results, because its requests have no headers and cannot be authenticated.

This example demonstrates how to provide Bearer-based authentication in ASP.NET MVC applications and use the [IWebDocumentViewerExportResultUriGenerator](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IWebDocumentViewerExportResultUriGenerator) service to enable a browser to get export results.

To accomplish this task, do the following:
- Set up Bearer token authentication.
- Assign the [Authorize](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.authorizeattribute) attribute to the Web Document Viewer controller's **Invoke** action.
- Implement the **IWebDocumentViewerExportResultUriGenerator** interface and register it in the service container. In the **CreateUri** method, save an exported document to any storage and return the URI to access it from the client side.
- Enable the asynchronous export mechanism on the Web Document Viewer's client-side.
- Apply the Bearer token to all the reporting AJAX requests.

When the application runs, you should register in the system, log in to it, and open the test report.
