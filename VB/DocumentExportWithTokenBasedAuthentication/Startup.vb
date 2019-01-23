Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.Owin
Imports Owin

<Assembly: OwinStartup(GetType(T680906.Startup))>

Namespace T680906
	Partial Public Class Startup
		Public Sub Configuration(ByVal app As IAppBuilder)
			ConfigureAuth(app)
		End Sub
	End Class
End Namespace
