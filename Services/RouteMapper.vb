﻿Imports System
Imports DotNetNuke.Web.Api

Namespace Services
    Public Class RouteMapper
        Implements IServiceRouteMapper

        Public Sub RegisterRoutes(mapRouteManager As IMapRoute) Implements IServiceRouteMapper.RegisterRoutes

            mapRouteManager.MapHttpRoute("Connect/Kickstart", "default", "{controller}/{action}", New String() {"Connect.Modules.Kickstart.Services"})

        End Sub

    End Class
End Namespace

