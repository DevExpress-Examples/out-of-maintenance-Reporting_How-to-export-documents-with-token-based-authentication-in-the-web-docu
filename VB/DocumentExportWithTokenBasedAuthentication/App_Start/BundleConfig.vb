Imports System.Web
Imports System.Web.Optimization

Namespace T680906
	Public Class BundleConfig
		' For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		Public Shared Sub RegisterBundles(ByVal bundles As BundleCollection)
			' Use the development version of Modernizr to develop with and learn from. Then, when you're
			' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add((New ScriptBundle("~/bundles/modernizr")).Include("~/Scripts/modernizr-*"))

			bundles.Add((New ScriptBundle("~/bundles/bootstrap")).Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"))

			bundles.Add((New StyleBundle("~/Content/css")).Include("~/Content/bootstrap.css", "~/Content/site.css"))


			bundles.Add((New ScriptBundle("~/bundles/app")).Include("~/Scripts/app.js"))

			bundles.Add((New ScriptBundle("~/bundles/init")).Include("~/Scripts/init.js"))

			' Set EnableOptimizations to false for debugging. For more information,
			' visit http://go.microsoft.com/fwlink/?LinkId=301862
			'BundleTable.EnableOptimizations = true;
		End Sub
	End Class
End Namespace
