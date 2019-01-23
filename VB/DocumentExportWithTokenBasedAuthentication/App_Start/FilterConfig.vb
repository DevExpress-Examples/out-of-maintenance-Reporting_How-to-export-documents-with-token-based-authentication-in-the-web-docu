Imports System.Web
Imports System.Web.Mvc

Namespace T680906
	Public Class FilterConfig
		Public Shared Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)
			filters.Add(New HandleErrorAttribute())
			filters.Add(New RequireHttpsAttribute())
		End Sub
	End Class
End Namespace
