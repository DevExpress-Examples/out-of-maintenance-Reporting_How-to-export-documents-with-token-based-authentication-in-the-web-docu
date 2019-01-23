Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.Google
Imports Microsoft.Owin.Security.OAuth
Imports Owin
Imports T680906.Providers
Imports T680906.Models

Namespace T680906
	Partial Public Class Startup
		Private Shared privateOAuthOptions As OAuthAuthorizationServerOptions
		Public Shared Property OAuthOptions() As OAuthAuthorizationServerOptions
			Get
				Return privateOAuthOptions
			End Get
			Private Set(ByVal value As OAuthAuthorizationServerOptions)
				privateOAuthOptions = value
			End Set
		End Property

		Private Shared privatePublicClientId As String
		Public Shared Property PublicClientId() As String
			Get
				Return privatePublicClientId
			End Get
			Private Set(ByVal value As String)
				privatePublicClientId = value
			End Set
		End Property

		' For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		Public Sub ConfigureAuth(ByVal app As IAppBuilder)
			' Configure the db context and user manager to use a single instance per request
			app.CreatePerOwinContext(AddressOf ApplicationDbContext.Create)
			app.CreatePerOwinContext(Of ApplicationUserManager)(AddressOf ApplicationUserManager.Create)

			' Enable the application to use a cookie to store information for the signed in user
			' and to use a cookie to temporarily store information about a user logging in with a third party login provider
			'app.UseCookieAuthentication(new CookieAuthenticationOptions());
			'app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			' Configure the application for OAuth based flow
			PublicClientId = "self"
			OAuthOptions = New OAuthAuthorizationServerOptions With {
				.TokenEndpointPath = New PathString("/Token"),
				.Provider = New ApplicationOAuthProvider(PublicClientId),
				.AuthorizeEndpointPath = New PathString("/api/Account/ExternalLogin"),
				.AccessTokenExpireTimeSpan = TimeSpan.FromDays(14)
			}

			' Enable the application to use bearer tokens to authenticate users
			app.UseOAuthBearerTokens(OAuthOptions)

			' Uncomment the following lines to enable logging in with third party login providers
			'app.UseMicrosoftAccountAuthentication(
			'    clientId: "",
			'    clientSecret: "");

			'app.UseTwitterAuthentication(
			'    consumerKey: "",
			'    consumerSecret: "");

			'app.UseFacebookAuthentication(
			'    appId: "",
			'    appSecret: "");

			'app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
			'{
			'    ClientId = "",
			'    ClientSecret = ""
			'});
		End Sub
	End Class
End Namespace
