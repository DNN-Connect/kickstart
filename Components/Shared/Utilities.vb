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

        Public Shared Function GetParticipationUsers(objProject As ProjectInfo, IncludeCreator As Boolean) As List(Of UserInfo)

            Dim objUsers As New List(Of UserInfo)

            Dim participants As List(Of ParticipantInfo) = ParticipantController.ListByProject(objProject.ProjectId)
            For Each p As ParticipantInfo In participants

                Dim blnAdd As Boolean = True
                For Each u As UserInfo In objUsers
                    If u.UserID = p.UserId Then
                        blnAdd = False
                        Exit For
                    End If
                Next

                Dim objUser As UserInfo = UserController.GetUserById(GetPortalSettings.PortalId, p.UserId)
                If objUser Is Nothing Then blnAdd = False

                If blnAdd Then
                    objUsers.Add(objUser)
                End If

            Next

            If IncludeCreator Then
                Dim objCreator As UserInfo = UserController.GetUserById(GetPortalSettings.PortalId, objProject.CreatedBy)
                If Not objCreator Is Nothing Then

                    Dim blnAdd As Boolean = True
                    For Each u As UserInfo In objUsers
                        If u.UserID = objCreator.UserID Then
                            blnAdd = False
                            Exit For
                        End If
                    Next

                    If blnAdd Then
                        objUsers.Add(objCreator)
                    End If

                End If
            End If

            Return objUsers

        End Function

        Public Shared Function IsTeamMember(UserId As Integer, objProject As ProjectInfo, IncludeCreator As Boolean) As Boolean

            For Each objUser As UserInfo In GetParticipationUsers(objProject, IncludeCreator)
                If objUser.UserID = UserId Then
                    Return True
                End If
            Next

            Return False

        End Function

        Public Shared Function GetParticipators(objProject As ProjectInfo, PortalId As Integer) As List(Of ParticipantInfo)

            Dim objParticiapants As New List(Of ParticipantInfo)

            Dim participants As List(Of ParticipantInfo) = ParticipantController.ListByProject(objProject.ProjectId)
            For Each p As ParticipantInfo In participants

                Dim blnAdd As Boolean = True
                For Each u As ParticipantInfo In objParticiapants
                    If u.UserId = p.UserId Then
                        u.ListRoles.Add(p.ProjectRole.ToString)
                        blnAdd = False
                        Exit For
                    End If
                Next

                If blnAdd Then

                    p.ListRoles = New List(Of String)
                    p.ListRoles.Add(p.ProjectRole.ToString)

                    Dim oUser As UserInfo = UserController.GetUserById(PortalId, p.UserId)
                    p.PhotoUrl = "~/ProfilePic.ashx?UserId=" & oUser.UserID.ToString & "&h=32&w=32"
                    p.Displayname = oUser.DisplayName

                    objParticiapants.Add(p)

                End If

            Next

            Return objParticiapants

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
            Return DotNetNuke.Common.Globals.NavigateURL(GetPortalSettings.UserTabId, "", "UserId=" & UserId.ToString)
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


        Public Shared Function NavigateUrl(ProjectId As Integer, TabId As Integer) As String
            Return DotNetNuke.Common.Globals.NavigateURL(TabId, "", "ProjectId=" & ProjectId.ToString)
        End Function

        Public Shared Function NavigateUrl(TabId As Integer, ActionMode As ActionMode) As String
            Return DotNetNuke.Common.Globals.NavigateURL(TabId, "", "Action=" & ActionMode.ToString)
        End Function

        Public Shared Function NavigateUrl(ProjectId As Integer, TabId As Integer, ActionMode As ActionMode) As String
            Return DotNetNuke.Common.Globals.NavigateURL(TabId, "", "Action=" & ActionMode.ToString, "ProjectId=" & ProjectId.ToString)
        End Function

        Public Shared Function NavigateUrl(Settings As KickstartSettings, ProjectId As Integer, ActionMode As ActionMode) As String

            If Not Settings Is Nothing Then
                Return NavigateUrl(ProjectId, Settings.ProjectDetailsTabId, ActionMode)
            End If

            Return NavigateUrl(ProjectId, GetPortalSettings.ActiveTab.TabID)

        End Function

    End Class

End Namespace

