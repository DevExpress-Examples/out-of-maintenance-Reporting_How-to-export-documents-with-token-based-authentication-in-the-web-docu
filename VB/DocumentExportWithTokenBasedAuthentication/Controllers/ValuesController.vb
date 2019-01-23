Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Namespace T680906.Controllers
	<Authorize>
	Public Class ValuesController
		Inherits ApiController

		' GET api/values
		Public Function [Get]() As String
			Dim userName = Me.RequestContext.Principal.Identity.Name
			Return String.Format("Hello, {0}.", userName)
		End Function
	End Class
End Namespace
