Imports Connect.Modules.Kickstart.Entities
Imports DotNetNuke.Services.Journal

Namespace Connect.Modules.Kickstart

    Public Class frmIdea
        Inherits KickstartModuleBase


#Region "Event Handlers"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

            If Request.IsAuthenticated = False Then
                Response.Redirect(NavigateURL(KickstartSettings.ProjectListTabId))
            End If

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        End Sub

#End Region

#Region "Templating"


#End Region

#Region "Private Methods"


#End Region

        Private Sub cmdCreate_Click(sender As Object, e As EventArgs) Handles cmdCreate.Click
            CreateIdea()
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

                    Integration.NotificationController.SendPendingProjectNotification(objProject, PortalId, ProjectUrl(objProject.ProjectId))

                End If

            End If

        End Sub

    End Class

End Namespace
