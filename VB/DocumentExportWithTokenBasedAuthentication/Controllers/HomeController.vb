Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc

Namespace T680906.Controllers
	Public Class HomeController
		Inherits Controller

		Public Function Index() As ActionResult
			ViewBag.Title = "Home Page"

			Return View()
		End Function
	End Class
End Namespace
