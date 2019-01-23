Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Web.Http

Namespace T680906.Results
	Public Class ChallengeResult
		Implements IHttpActionResult

		Public Sub New(ByVal loginProvider As String, ByVal controller As ApiController)
			Me.LoginProvider = loginProvider
			Request = controller.Request
		End Sub

		Public Property LoginProvider() As String
		Public Property Request() As HttpRequestMessage

		Public Function ExecuteAsync(ByVal cancellationToken As CancellationToken) As Task(Of HttpResponseMessage) Implements IHttpActionResult.ExecuteAsync
			Request.GetOwinContext().Authentication.Challenge(LoginProvider)

			Dim response As New HttpResponseMessage(HttpStatusCode.Unauthorized)
			response.RequestMessage = Request
			Return Task.FromResult(response)
		End Function
	End Class
End Namespace
