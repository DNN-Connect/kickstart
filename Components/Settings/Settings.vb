Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Entities.Portals
Imports Connect.Modules.Kickstart.Entities

Namespace Connect.Modules.Kickstart
    Public Class KickstartSettings

        Public ModuleTheme As String = Null.NullString
        Public ThemePath As String = Null.NullString
        Public ViewMode As String = Null.NullString
        Public ProjectListTabId As Integer = Null.NullInteger
        Public ProjectDetailsTabId As Integer = Null.NullInteger
        Public Portalsettings As PortalSettings
        Public CanDeleteComment As Boolean = False
        Public CanEditComment As Boolean = False
        Public CanComment As Boolean = False
        Public CanAddProject As Boolean = False
        Public CanParticipate As Boolean = False
        Public CanEditProject As Boolean = False
        Public CanApproveProject As Boolean = False
        Public CanLockProject As Boolean = False
        Public CanDeleteProject As Boolean = False
        Public CanVote As Boolean = False
        Public CanSubscribe As Boolean = False
        Public ModuleConfiguration As ModuleInfo

        Public Sub New(ActiveModule As ModuleInfo, ActiveProject As ProjectInfo)
            Me.New(ActiveModule)

            Dim blnIsAuthenticated As Boolean = HttpContext.Current.Request.IsAuthenticated
            Dim blnIsModuleEditor As Boolean = (blnIsAuthenticated AndAlso DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(ActiveModule.ModulePermissions, "EDIT"))

            CanEditProject = (blnIsModuleEditor Or ActiveProject.LeadBy = UserController.GetCurrentUserInfo.UserID)

        End Sub

        Public Sub New(ActiveModule As ModuleInfo)

            Portalsettings = PortalController.GetCurrentPortalSettings
            ModuleConfiguration = ActiveModule

            Dim ModuleSettings As Hashtable = ActiveModule.ModuleSettings

            If ModuleSettings.Contains("Kickstart_ModuleTheme") Then
                ModuleTheme = ModuleSettings("Kickstart_ModuleTheme").ToString
                ThemePath = GetThemePath()
            End If

            If ModuleSettings.Contains("Kickstart_ViewMode") Then
                ViewMode = ModuleSettings("Kickstart_ViewMode").ToString
            End If

            If ModuleSettings.Contains("Kickstart_DetailsTabId") Then
                If IsNumeric(ModuleSettings("Kickstart_DetailsTabId")) Then
                    ProjectDetailsTabId = Convert.ToInt32(ModuleSettings("Kickstart_DetailsTabId"))
                End If
            End If

            If ModuleSettings.Contains("Kickstart_ListTabId") Then
                If IsNumeric(ModuleSettings("Kickstart_ListTabId")) Then
                    ProjectListTabId = Convert.ToInt32(ModuleSettings("Kickstart_ListTabId"))
                End If
            End If


            Dim blnIsAuthenticated As Boolean = HttpContext.Current.Request.IsAuthenticated
            Dim blnIsModuleEditor As Boolean = (blnIsAuthenticated AndAlso DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(ActiveModule.ModulePermissions, "EDIT"))
            Dim blnIsAdmin As Boolean = (blnIsAuthenticated AndAlso UserController.GetCurrentUserInfo.IsInRole(GetPortalSettings.AdministratorRoleName))

            CanAddProject = blnIsAuthenticated
            CanApproveProject = blnIsModuleEditor
            CanComment = blnIsAuthenticated
            CanDeleteComment = blnIsModuleEditor
            CanDeleteProject = blnIsModuleEditor
            CanEditComment = blnIsModuleEditor
            CanEditProject = blnIsModuleEditor
            CanLockProject = blnIsModuleEditor
            CanVote = blnIsAuthenticated
            CanSubscribe = blnIsAuthenticated
            CanParticipate = blnIsAuthenticated

        End Sub

        Private Function GetThemePath() As String

            Dim absTemplateFolderPath As String = HttpRuntime.AppDomainAppPath & "Desktopmodules\Connect\Kickstart\Templates\"
            absTemplateFolderPath += ModuleTheme & "\"

            Return absTemplateFolderPath.Replace("\\", "\")

        End Function

    End Class

End Namespace

