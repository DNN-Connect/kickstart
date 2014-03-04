Imports Connect.Modules.Kickstart.Entities
Imports DotNetNuke.Services.Journal

Namespace Connect.Modules.Kickstart

    Public Class frmIdea
        Inherits KickstartModuleBase


#Region "Event Handlers"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init


            If Request.IsAuthenticated = False Then
                Response.Redirect(NavigateURL(PortalSettings.LoginTabId, "", "ReturnUrl=" & Server.UrlEncode(Request.RawUrl)))
            End If

            ReadQuerystring()

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not Project Is Nothing Then
                If Project.CreatedBy = UserInfo.UserID Or KickstartSettings.CanApproveProject Then
                    If Not Page.IsPostBack Then
                        BindProject()
                    End If                    
                Else
                    Response.Redirect(NavigateURL(KickstartSettings.ProjectListTabId))
                End If
            Else

                If ActionMode.ToLower <> "create" Then
                    Response.Redirect(NavigateURL(KickstartSettings.ProjectListTabId))
                End If

            End If

        End Sub

        Private Sub cmdCreate_Click(sender As Object, e As EventArgs) Handles cmdCreate.Click

            If Project Is Nothing Then
                CreateIdea()
            Else
                UpdateIdea()
            End If

        End Sub

#End Region

#Region "Templating"


#End Region

#Region "Private Methods"

        Private Sub BindProject()

            cmdCreate.Text = Localization.GetString("UpdateIdea", LocalResourceFile)
            txtContent.Text = Project.Content
            txtSubject.Text = Project.Subject
            txtSummary.Text = Project.Summary

        End Sub

        Private Sub UpdateIdea()

            Project.Subject = txtSubject.Text
            Project.Summary = txtSummary.Text
            Project.Content = txtContent.Text

            Dim blnNotifyCreator As Boolean = False
            Dim blnNotifyApprovers As Boolean = False

            If Project.IsVisible = False Then
                If KickstartSettings.CanApproveProject Then
                    Project.IsVisible = True
                    blnNotifyCreator = True
                Else
                    blnNotifyApprovers = True
                End If
            Else
                If KickstartSettings.CanApproveProject = False Then
                    Project.IsVisible = False
                    blnNotifyApprovers = True
                End If
            End If

            ProjectController.Update(Project)

            If blnNotifyApprovers Then
                Integration.NotificationController.SendPendingProjectNotification(Project, PortalId, ProjectUrl(Project.ProjectId), True)
            End If

            If blnNotifyCreator Then
                Integration.NotificationController.SendVisibilityChangedNotification(Project, UserInfo, ProjectUrl(Project.ProjectId))
            End If

            Response.Redirect(ProjectUrl(Project.ProjectId.ToString))

        End Sub

        Private Sub CreateIdea()

            If txtContent.Text.Length > 0 AndAlso txtSummary.Text.Length > 0 AndAlso txtContent.Text.Length > 0 Then

                Dim objProject As New ProjectInfo

                objProject.DateDeleted = Null.NullDate
                objProject.DateDelivered = Null.NullDate
                objProject.DateLocked = Null.NullDate
                objProject.DateScheduled = Null.NullDate
                objProject.DeletedBy = Null.NullInteger
                objProject.IsDeleted = False
                objProject.IsDelivered = False
                objProject.IsLocked = False
                objProject.IsVisible = False
                objProject.LeadBy = Null.NullInteger
                objProject.LockedBy = Null.NullInteger
                objProject.PlatformRssUrl = Null.NullString
                objProject.ProjectPlatform = Null.NullString
                objProject.ProjectUrl = Null.NullString
                objProject.Status = Enums.ProjectStatus.Suggested
                objProject.TeamMembers = 0
                objProject.Views = 0
                objProject.Votes = 0
                objProject.Comments = 0
                objProject.ContentItemId = 0
                objProject.CreatedBy = UserInfo.UserID
                objProject.DateCreated = Date.Now
                objProject.ModuleId = ModuleId
                objProject.Subject = txtSubject.Text
                objProject.Summary = txtSummary.Text
                objProject.Content = txtContent.Text

                objProject.ProjectId = ProjectController.Add(objProject)

                If objProject.ProjectId <> Null.NullInteger Then

                    Integration.NotificationController.SendPendingProjectNotification(objProject, PortalId, ProjectUrl(objProject.ProjectId), False)

                End If

                Response.Redirect(ProjectUrl(objProject.ProjectId.ToString))

            End If

        End Sub

#End Region

        

        

    End Class

End Namespace
