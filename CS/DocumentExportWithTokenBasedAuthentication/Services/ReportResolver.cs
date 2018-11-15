using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using T680906.Reports;

namespace T680906.Services
{
    public class ReportResolver : IWebDocumentViewerReportResolver
    {
        public XtraReport Resolve(string reportEntry)
        {
            if(reportEntry == "testReport") {
                return new XtraReport1();
            }
            return null;
        }
    }
}