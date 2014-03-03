Imports DotNetNuke.Web.Api
Imports DotNetNuke.Entities.Users
Imports Connect.Modules.Kickstart.Entities


Namespace Connect.Modules.Kickstart

#Region " Security Access Levels "

    Public Enum SecurityAccessLevel As Integer
        Anonymous = 0
        Admin = 1
        ViewModule = 2
        EditModule = 4
        AddProject = 8
        EditProject = 16
        LockProject = 32
        DeleteProject = 64
        ApproveProject = 128
        AddComment = 256
        EditComment = 512
        DeleteComment = 1024
        Lead = 2048
        Creator = 4096
        Authenticated = 8192
    End Enum

#End Region

    Public Class KickstartAuthorizeAttribute
        Inherits AuthorizeAttributeBase
        Implements IOverrideDefaultAuthLevel

#Region "Properties"

        Public Property ProjectId As Integer = Null.NullInteger
        Public Property NotificationId As Integer = Null.NullInteger
        Public Property AccessLevel As SecurityAccessLevel
        Public Property UserInfo As UserInfo
        ' Public Property Security As ContextSecurity

#End Region

#Region "Constructors"

        Public Sub New()
            AccessLevel = SecurityAccessLevel.Admin
        End Sub

        Public Sub New(accessLevel As SecurityAccessLevel)
            Me.AccessLevel = accessLevel
        End Sub

#End Region

#Region " Public Methods "

        Public Overrides Function IsAuthorized(context As AuthFilterContext) As Boolean

            If AccessLevel = SecurityAccessLevel.Anonymous Then Return True ' save time by not going through the code below

            If Not HttpContext.Current.Request.Params("ProjectId") Is Nothing Then
                Integer.TryParse(HttpContext.Current.Request.Params("ProjectId"), ProjectId)
            End If

            Dim portalSettings As DotNetNuke.Entities.Portals.PortalSettings = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings()
            Dim moduleId As Integer = context.ActionContext.Request.FindModuleId
            Dim tabId As Integer = context.ActionContext.Request.FindTabId
            Dim blnAuthenticated As Boolean = False

            If Not HttpContextSource.Current.Request.IsAuthenticated Then
                blnAuthenticated = False
            Else
                UserInfo = UserController.GetCurrentUserInfo
                blnAuthenticated = True
            End If

            Dim Project As ProjectInfo = ProjectController.Get(ProjectId)


            If blnAuthenticated Then

                If AccessLevel = SecurityAccessLevel.Admin Then

                    Return UserInfo.IsInRole(GetPortalSettings.AdministratorRoleName)

                ElseIf AccessLevel = SecurityAccessLevel.Authenticated Then

                    Return True

                ElseIf AccessLevel = SecurityAccessLevel.ViewModule Then

                    Return DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(context.ActionContext.Request.FindModuleInfo().ModulePermissions, "VIEW")

                ElseIf AccessLevel = SecurityAccessLevel.EditModule Then

                    Return DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(context.ActionContext.Request.FindModuleInfo().ModulePermissions, "EDIT")

                ElseIf AccessLevel = SecurityAccessLevel.Lead Then

                    Return UserInfo.UserID = Project.LeadBy

                ElseIf AccessLevel = SecurityAccessLevel.AddComment Then

                    Return True

                ElseIf AccessLevel = SecurityAccessLevel.DeleteComment Then

                    Return DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(context.ActionContext.Request.FindModuleInfo().ModulePermissions, "EDIT")

                ElseIf AccessLevel = SecurityAccessLevel.EditComment Then

                    Return DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(context.ActionContext.Request.FindModuleInfo().ModulePermissions, "EDIT")

                ElseIf AccessLevel = SecurityAccessLevel.Creator Then

                    Return UserInfo.UserID = Project.CreatedBy

                ElseIf AccessLevel = SecurityAccessLevel.AddProject Then

                    Return True

                ElseIf AccessLevel = SecurityAccessLevel.ApproveProject Then

                    Return DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(context.ActionContext.Request.FindModuleInfo().ModulePermissions, "EDIT")

                ElseIf AccessLevel = SecurityAccessLevel.DeleteProject Then

                    Return DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(context.ActionContext.Request.FindModuleInfo().ModulePermissions, "EDIT")

                ElseIf AccessLevel = SecurityAccessLevel.LockProject Then

                    Return DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(context.ActionContext.Request.FindModuleInfo().ModulePermissions, "EDIT")

                ElseIf AccessLevel = SecurityAccessLevel.EditProject Then

                    If DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(context.ActionContext.Request.FindModuleInfo().ModulePermissions, "EDIT") Then
                        Return True
                    End If
                    If UserInfo.UserID = Project.LeadBy Then
                        Return True
                    End If
                    If Project.LeadBy = Null.NullInteger And Project.CreatedBy = UserInfo.UserID Then
                        Return True
                    End If

                End If

            Else

                If AccessLevel = SecurityAccessLevel.ViewModule Then
                    Return DotNetNuke.Security.Permissions.ModulePermissionController.CanViewModule(context.ActionContext.Request.FindModuleInfo())
                End If

            End If


            Return False

        End Function
#End Region

#Region " Private Methods "
#End Region

    End Class

End Namespace

