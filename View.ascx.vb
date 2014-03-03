
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security.Membership
Imports Telerik.Web.UI
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Entities.Profile
Imports DotNetNuke.Web.UI.WebControls
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Common.Lists

Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Framework.JavaScriptLibraries

Namespace Connect.Modules.Kickstart

    Public Class View
        Inherits KickstartModuleBase

#Region "Private Members"

        Private _controlToLoad As String = ""

#End Region

#Region "Event Handlers"

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins)

            LoadControlType()
            Me.Page.ClientScript.RegisterClientScriptInclude("ConnectKickstart", ResolveClientUrl(Me.TemplateSourceDirectory & "/js/kickstart.js"))

        End Sub

#End Region

#Region "Private Methods"

        Private Sub LoadControlType()

            Select Case ViewMode.ToLower
                Case "projectslist"
                    _controlToLoad = Me.TemplateSourceDirectory & "/ViewControls/ProjectsList.ascx"
                Case "projectsdetail"
                    _controlToLoad = Me.TemplateSourceDirectory & "/ViewControls/ProjectsDetail.ascx"
                Case "participation"
                    _controlToLoad = Me.TemplateSourceDirectory & "/ViewControls/ProjectMembers.ascx"
                Case "funding"
                    _controlToLoad = Me.TemplateSourceDirectory & "/ViewControls/Funding.ascx"
                Case Else
                    plhControls.Controls.Add(New LiteralControl(Localization.GetString("MustConfigure", LocalResourceFile)))
                    Exit Sub
            End Select

            If ActionMode <> "" Then
                If ViewMode.ToLower = "projectsdetail" Then
                    Select Case ActionMode.ToLower
                        Case "create"
                            _controlToLoad = Me.TemplateSourceDirectory & "/ViewControls/frmIdea.ascx"
                        Case "becomelead"
                            _controlToLoad = Me.TemplateSourceDirectory & "/ViewControls/frmBecomeLead.ascx"
                        Case "editproject"
                            _controlToLoad = Me.TemplateSourceDirectory & "/ViewControls/frmProject.ascx"
                        Case "participate"
                            _controlToLoad = Me.TemplateSourceDirectory & "/ViewControls/frmParticipate.ascx"
                        Case "fund"
                            _controlToLoad = Me.TemplateSourceDirectory & "/ViewControls/frmFund.ascx"
                    End Select
                End If
            End If

            Dim objPortalModuleBase As KickstartModuleBase = CType(Me.LoadControl(_controlToLoad), KickstartModuleBase)
            objPortalModuleBase.ModuleConfiguration = Me.ModuleConfiguration
            objPortalModuleBase.ID = System.IO.Path.GetFileNameWithoutExtension(_controlToLoad)
            ' Load the appropriate control
            '
            plhControls.Controls.Add(objPortalModuleBase)

        End Sub

#End Region

    End Class

End Namespace

