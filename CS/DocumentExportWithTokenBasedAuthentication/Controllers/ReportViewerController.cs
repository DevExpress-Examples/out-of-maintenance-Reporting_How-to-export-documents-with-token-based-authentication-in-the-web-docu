using DevExpress.Web.Mvc.Controllers;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using T680906.Services;
using System.Web.Mvc;

namespace T680906.Controllers
{
    public class ReportViewerController : WebDocumentViewerApiControllerBase
    {
        IExportResultProvider documentExportService;
        public ReportViewerController() {
            documentExportService = (IExportResultProvider)DefaultWebDocumentViewerContainer.Current.GetService(typeof(IExportResultProvider));
        }

        [Authorize]
        public override ActionResult Invoke() {
            return base.Invoke();
        }

        [HttpGet]
        public ActionResult GetExportResult(string token, string fileName) {
            ExportResult exportResult;
            if (!documentExportService.TryGetExportResult(token, out exportResult)) {
                return new HttpNotFoundResult("Exported document was not found. Try to export the document once again.");
            }
            var fileResult = File(exportResult.GetBytes(), exportResult.ContentType);
            if (exportResult.ContentDisposition != System.Net.Mime.DispositionTypeNames.Inline) {
                fileResult.FileDownloadName = exportResult.FileName;
            }

            return fileResult;
        }
    }
}