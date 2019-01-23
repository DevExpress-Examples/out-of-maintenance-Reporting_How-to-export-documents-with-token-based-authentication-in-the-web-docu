Imports System
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http.Controllers
Imports System.Web.Http.Filters

Namespace T680906.Filters
	Public Class RequireHttpsAttribute
		Inherits AuthorizationFilterAttribute

		Public Property Port() As Integer

		Public Sub New()
			Port = 443
		End Sub

		Public Overrides Sub OnAuthorization(ByVal actionContext As HttpActionContext)
			Dim request = actionContext.Request

			If request.RequestUri.Scheme <> Uri.UriSchemeHttps Then
				Dim response = New HttpResponseMessage()

				If request.Method Is HttpMethod.Get OrElse request.Method Is HttpMethod.Head Then
					Dim uri = New UriBuilder(request.RequestUri)
					uri.Scheme = System.Uri.UriSchemeHttps
					uri.Port = Me.Port

					response.StatusCode = HttpStatusCode.Found
					response.Headers.Location = uri.Uri
				Else
					response.StatusCode = HttpStatusCode.Forbidden
				End If

				actionContext.Response = response
			Else
				MyBase.OnAuthorization(actionContext)
			End If
		End Sub
	End Class

End Namespace