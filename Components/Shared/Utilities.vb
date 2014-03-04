Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Security.Permissions
Imports Connect.Modules.Kickstart.Entities

Namespace Connect.Modules.Kickstart


    Public Class Utilities

        Public Shared SharedResourceFile As String = "~/Desktopmodules/Connect/Kickstart/App_LocalResources/SharedResources.resx"

        Public Shared Function GetSharedResource(ByVal strKey As String) As String
            Return Localization.GetString(strKey, SharedResourceFile)
        End Function

        Public Shared Function GetSharedJSSafeResource(ByVal strKey As String) As String
            Return Localization.GetSafeJSString(strKey, SharedResourceFile)
        End Function

        Public Shared Function GetProjectParticipants(objProject As ProjectInfo, IncludeCreator As Boolean) As List(Of UserInfo)

            Dim objUsers As New List(Of UserInfo)

            Dim participants As List(Of ParticipantInfo) = ParticipantController.ListByProject(objProject.ProjectId)
            For Each p As ParticipantInfo In participants
                Dim objUser As UserInfo = UserController.GetUserById(GetPortalSettings.PortalId, p.UserId)
                If Not objUser Is Nothing Then
                    objUsers.Add(objUser)
                End If
            Next

            Dim objCreator As UserInfo = UserController.GetUserById(GetPortalSettings.PortalId, objProject.CreatedBy)
            If Not objCreator Is Nothing Then
                objUsers.Add(objCreator)
            End If

            Return objUsers

        End Function

        'Public Shared Function GetProjectParticipants(ProjectId) As List(Of UserInfo)

        '    Dim project As ProjectInfo = ProjectController.Get(ProjectId)
        '    If Not project Is Nothing Then
        '        Return GetProjectParticipants(project)
        '    End If

        '    Return New List(Of UserInfo)

        'End Function

        Public Shared Function IsTeamMember(UserId As Integer, objProject As ProjectInfo) As Boolean

            For Each objUser As UserInfo In GetProjectParticipants(objProject, False)
                If objUser.UserID = UserId Then
                    Return True
                End If
            Next

            Return False

        End Function

        Public Shared Function GetModuleEditors(ModuleId) As List(Of UserInfo)

            Dim objUsers As New List(Of UserInfo)
            Dim ctrlRoles As New RoleController

            Dim ctrlModules As New ModuleController
            Dim objModule As ModuleInfo = ctrlModules.GetModule(ModuleId)
            For Each objPermission As ModulePermissionInfo In objModule.ModulePermissions
                If objPermission.PermissionKey = "EDIT" AndAlso objPermission.AllowAccess Then

                    If Not String.IsNullOrEmpty(objPermission.RoleName) Then

                        Dim permissionUsers As ArrayList = ctrlRoles.GetUsersByRoleName(objModule.PortalID, objPermission.RoleName)
                        For Each objUser As UserInfo In permissionUsers
                            objUsers.Add(objUser)
                        Next

                    End If

                    If Not String.IsNullOrEmpty(objPermission.Username) Then

                        Dim objUser As UserInfo = UserController.GetUserByName(objModule.PortalID, objPermission.Username)
                        If Not objUser Is Nothing Then
                            objUsers.Add(objUser)
                        End If

                    End If

                End If
            Next

            Return objUsers

        End Function

        Public Shared Function GetUserProfileUrl(UserId As Integer)
            Return NavigateURL(GetPortalSettings.UserTabId, "", "UserId=" & UserId)
        End Function

        Public Shared Function FormatCurrency(strIn As String) As String
            Select Case strIn.ToUpper
                Case "USD"
                    Return "$"
                Case "EUR"
                    Return "€"
            End Select
            Return strIn
        End Function

        Public Shared Function FormatAmount(strIn As Decimal) As String
            Return strIn.ToString("0")
        End Function

        Public Shared Function FormatFunding(objFunding As FundingInfo) As String

            Return FormatCurrency(ConfigController.GetConfig(objFunding.ProjectId).FundingCurrency) & " " & FormatAmount(objFunding.Funding)

        End Function

        Public Shared Function FormatProjectVisiblity(objProject As ProjectInfo) As String

            If objProject.IsVisible Then
                Return Utilities.GetSharedResource("Approved")
            Else
                Return Utilities.GetSharedResource("UnApproved")
            End If

        End Function

        Public Shared Function CanEdit(objProject As ProjectInfo) As Boolean

            If HttpContext.Current.Request.IsAuthenticated = False Then
                Return False
            End If

            Dim user As UserInfo = UserController.GetCurrentUserInfo
            For Each objUser As UserInfo In Utilities.GetModuleEditors(objProject.ModuleId)
                If objUser.UserID = user.UserID Then
                    Return True
                End If
            Next

            Return False

        End Function

    End Class

End Namespace

