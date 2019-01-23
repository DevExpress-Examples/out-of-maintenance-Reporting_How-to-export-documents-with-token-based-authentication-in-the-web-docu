Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Http
Imports System.Web.Http
Imports Microsoft.Owin.Security.OAuth
Imports Newtonsoft.Json.Serialization

Namespace T680906
	Public Module WebApiConfig
		Public Sub Register(ByVal config As HttpConfiguration)
			' Web API configuration and services
			' Configure Web API to use only bearer token authentication.
			config.SuppressDefaultHostAuthentication()
			config.Filters.Add(New HostAuthenticationFilter(OAuthDefaults.AuthenticationType))

			' Web API routes
			config.MapHttpAttributeRoutes()

			config.Routes.MapHttpRoute(name:= "DefaultApi", routeTemplate:= "api/{controller}/{id}", defaults:= New With {Key .id = RouteParameter.Optional})

			' Enforce HTTPS
			config.Filters.Add(New T680906.Filters.RequireHttpsAttribute())
		End Sub
	End Module
End Namespace
