Imports Connect.Modules.Kickstart.Entities
Imports Connect.Modules.Kickstart.Entities.ParticipantInfo

Namespace Connect.Modules.Kickstart
    Public Class frmBecomeLead
        Inherits KickstartModuleBase


#Region "Event Handlers"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

            ReadQuerystring()

            If Project Is Nothing Then
                Response.Redirect(NavigateURL(KickstartSettings.ProjectListTabId))
            End If

            If Request.IsAuthenticated = False Then
                Response.Redirect(NavigateURL(PortalSettings.LoginTabId, "", "ReturnUrl=" & Server.UrlEncode(Request.RawUrl)))
            End If

            If Not Page.IsPostBack Then
                BindForm()
            End If

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        End Sub

        Private Sub cmdBecomeLead_Click(sender As Object, e As EventArgs) Handles cmdBecomeLead.Click

            BecomeLead()
            Response.Redirect(NavigateURL(TabId, "", "ProjectId=" & ProjectId.ToString))

        End Sub

        Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

            Response.Redirect(NavigateURL(TabId, "", "ProjectId=" & ProjectId.ToString))

        End Sub

#End Region

#Region "Private Methods"

        Private Sub BecomeLead()

            'check if not already member in another role
            Dim blnIsTeamMember As Boolean = Utilities.IsTeamMember(UserId, Project, False)

            Dim pI As New ParticipantInfo
            pI.ProjectRole = ParticipantRole.Manager
            pI.UserId = UserId
            pI.ProjectId = ProjectId

            'add role to particiapant list
            ParticipantController.Add(pI)

            If blnIsTeamMember = False Then
                Project.LeadBy = UserId
                Project.TeamMembers += 1
                ProjectController.Update(Project) 'update team members count
            End If

        End Sub

        Private Sub BindForm()

            If Project Is Nothing Then
                Response.Redirect(NavigateURL(KickstartSettings.ProjectListTabId))
            End If

            If Request.IsAuthenticated = False Then
                Response.Redirect(NavigateURL(PortalSettings.LoginTabId, "", "ReturnUrl=" & Server.UrlEncode(Request.RawUrl)))
            End If

            If Project.LeadBy <> Null.NullInteger Then
                Response.Redirect(NavigateURL(TabId, "", "ProjectId=" & ProjectId.ToString))
            End If

            lblLeadHeading.Text = Project.Subject
            lblLeadIntro.Text = Utilities.GetSharedResource("LeadIntroText")
            cmdBecomeLead.Text = Utilities.GetSharedResource("cmdBecomeLead")
            cmdCancel.Text = Utilities.GetSharedResource("Cancel")

        End Sub

#End Region

    End Class
End Namespace
