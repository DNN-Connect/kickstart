Imports Connect.Modules.Kickstart.Entities
Imports Connect.Modules.Kickstart.Entities.ParticipantInfo
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security.Permissions

Namespace Connect.Modules.Kickstart
    Public Class frmParticipate
        Inherits KickstartModuleBase


#Region "Event Handlers"

        Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

            DotNetNuke.Framework.AJAX.RegisterScriptManager()

            ReadQuerystring()

            If Project Is Nothing Then
                Response.Redirect(NavigateURL(KickstartSettings.ProjectListTabId))
            End If

            If Request.IsAuthenticated = False Then
                Response.Redirect(NavigateURL(KickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & ProjectId.ToString))
            End If

        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            LocalizeForm()

            If Not Page.IsPostBack Then
                BindData()
            End If

        End Sub

        Private Sub cmdBecomeDesigner_Click(sender As Object, e As EventArgs) Handles cmdBecomeDesigner.Click
            Participate(ParticipantRole.Designer)
            Response.Redirect(Request.RawUrl)
        End Sub

        Private Sub cmdBecomeDeveloper_Click(sender As Object, e As EventArgs) Handles cmdBecomeDeveloper.Click
            Participate(ParticipantRole.Developer)
            Response.Redirect(Request.RawUrl)
        End Sub

        Private Sub cmdBecomeManager_Click(sender As Object, e As EventArgs) Handles cmdBecomeManager.Click
            Participate(ParticipantRole.Manager)
            Response.Redirect(Request.RawUrl)
        End Sub

        Private Sub cmdBecomeTranslator_Click(sender As Object, e As EventArgs) Handles cmdBecomeTranslator.Click
            Participate(ParticipantRole.Translator)
            Response.Redirect(Request.RawUrl)
        End Sub

        Private Sub cmdBecomeTester_Click(sender As Object, e As EventArgs) Handles cmdBecomeTester.Click
            Participate(ParticipantRole.Tester)
            Response.Redirect(Request.RawUrl)
        End Sub

        Private Sub cmdStopDesigning_Click(sender As Object, e As EventArgs) Handles cmdStopDesigning.Click
            StopParticipating(ParticipantRole.Designer)
            Response.Redirect(Request.RawUrl)
        End Sub

        Private Sub cmdStopDeveloping_Click(sender As Object, e As EventArgs) Handles cmdStopDeveloping.Click
            StopParticipating(ParticipantRole.Developer)
            Response.Redirect(Request.RawUrl)
        End Sub

        Private Sub cmdStopManaging_Click(sender As Object, e As EventArgs) Handles cmdStopManaging.Click
            StopParticipating(ParticipantRole.Manager)
            Response.Redirect(Request.RawUrl)
        End Sub

        Private Sub cmdStopTesting_Click(sender As Object, e As EventArgs) Handles cmdStopTesting.Click
            StopParticipating(ParticipantRole.Tester)
            Response.Redirect(Request.RawUrl)
        End Sub

        Private Sub cmdStopTranslating_Click(sender As Object, e As EventArgs) Handles cmdStopTranslating.Click
            StopParticipating(ParticipantRole.Translator)
            Response.Redirect(Request.RawUrl)
        End Sub

#End Region

#Region "Templating"

        'todo: add templating to this control

#End Region

#Region "Private Methods"

        Private Sub Participate(Role As ParticipantRole)

            Dim pI As New ParticipantInfo
            pI.ProjectRole = Role
            pI.UserId = UserId
            pI.ProjectId = ProjectId

            'add role to particiapant list
            ParticipantController.Add(pI)

            'check if not already member in another role
            If Not Utilities.IsTeamMember(UserId, ProjectId) Then
                Project.TeamMembers += 1
                ProjectController.Update(Project) 'update team members count
            End If

            'notify the rest of the team
            Integration.NotificationController.SendNewParticipationNotification(Project, pI, UserInfo, NavigateURL(TabId, "", "ProjectId=" & ProjectId.ToString), False)

        End Sub

        Private Sub StopParticipating(Role As ParticipantRole)

            Dim current As List(Of ParticipantInfo) = ParticipantController.GetUserParticipation(UserId, ProjectId)
            For Each pI As ParticipantInfo In current
                If pI.ProjectRole = Role And pI.UserId = UserId Then

                    'remove role from participant list
                    ParticipantController.Delete(pI.ParticipationId)

                    'check if still team member in another role maybe
                    If Not Utilities.IsTeamMember(UserId, ProjectId) Then
                        Project.TeamMembers -= 1
                        ProjectController.Update(Project) 'reset team members count
                    End If

                    'notify the rest of the team
                    Integration.NotificationController.SendNewParticipationNotification(Project, pI, UserInfo, NavigateURL(TabId, "", "ProjectId=" & ProjectId.ToString), True)
                    Exit Sub

                End If
            Next

        End Sub

        Private Sub AddParticipant(ByRef pnl As Panel, ByVal UserId As Integer)

            Dim user As UserInfo = UserController.GetUserById(PortalId, UserId)
            If Not user Is Nothing Then

                Dim strHtml As String = "<div class=""kickstart-currentmember dnnClear"">"
                strHtml += "<img src=""~/profilepic.ashx?userId=" & UserId.ToString & "&h=30&w=30"" />"
                strHtml += "<p><a href=""" & NavigateURL(PortalSettings.UserTabId, "", "UserId=" & user.UserID.ToString) & """>" & user.DisplayName & "</a></p>"
                strHtml += "</div>"
                pnl.Controls.Add(New LiteralControl(strHtml))

            End If

        End Sub

        Private Sub BindData()

            Dim intDesignersNeeded As Integer = Config.DesignersNeeded
            Dim intDevelopersNeeded As Integer = Config.DevelopersNeeded
            Dim intManagersNeeded As Integer = Config.ManagersNeeded
            Dim intTranslatordNeeded As Integer = Config.TranslatorsNeeded
            Dim intTestersNeeded As Integer = Config.TestersNeeded

            Dim intCurrentManagers As Integer = 0

            Dim currentParticipation As List(Of ParticipantInfo) = ParticipantController.ListByProject(ProjectId)
            For Each pI As ParticipantInfo In currentParticipation
                If pI.ProjectRole = ParticipantRole.Designer Then
                    intDesignersNeeded -= 1
                    AddParticipant(pnlDesignersCurrent, pI.UserId)
                End If
                If pI.ProjectRole = ParticipantRole.Developer Then
                    intDevelopersNeeded -= 1
                    AddParticipant(pnlDevelopersCurrent, pI.UserId)
                End If
                If pI.ProjectRole = ParticipantRole.Manager Then
                    intManagersNeeded -= 1
                    AddParticipant(pnlManagersCurrent, pI.UserId)
                    intCurrentManagers += 1
                End If
                If pI.ProjectRole = ParticipantRole.Tester Then
                    intTestersNeeded -= 1
                    AddParticipant(pnlTestersCurrent, pI.UserId)
                End If
                If pI.ProjectRole = ParticipantRole.Translator Then
                    intTranslatordNeeded -= 1
                    AddParticipant(pnlTranslatotsCurrent, pI.UserId)
                End If
            Next

            If intDesignersNeeded < 0 Then intDesignersNeeded = 0
            If intDevelopersNeeded < 0 Then intDevelopersNeeded = 0
            If intManagersNeeded < 0 Then intManagersNeeded = 0
            If intTestersNeeded < 0 Then intTestersNeeded = 0
            If intTranslatordNeeded < 0 Then intTranslatordNeeded = 0

            lblDesignersNeeded.Text += intDesignersNeeded.ToString
            cmdBecomeDesigner.Visible = intDesignersNeeded > 0

            lblDevelopersNeeded.Text += intDevelopersNeeded.ToString
            cmdBecomeDeveloper.Visible = intDevelopersNeeded > 0

            lblManagersNeeded.Text += intManagersNeeded.ToString
            cmdBecomeManager.Visible = intManagersNeeded > 0

            lblTranlatorsNeeded.Text += intTranslatordNeeded.ToString
            cmdBecomeTranslator.Visible = intTranslatordNeeded > 0

            lblTestersNeeded.Text += intTestersNeeded.ToString
            cmdBecomeTester.Visible = intTestersNeeded > 0


            Dim myParticipation As List(Of ParticipantInfo) = ParticipantController.ListByUsers(UserId)
            For Each pI As ParticipantInfo In myParticipation
                If pI.ProjectRole = ParticipantRole.Designer Then
                    cmdBecomeDesigner.Visible = False
                    cmdStopDesigning.Visible = True
                End If
                If pI.ProjectRole = ParticipantRole.Developer Then
                    cmdBecomeDeveloper.Visible = False
                    cmdStopDeveloping.Visible = True
                End If
                If pI.ProjectRole = ParticipantRole.Manager Then
                    cmdBecomeManager.Visible = False
                    cmdStopManaging.Visible = (intCurrentManagers > 1) 'do not allow the last manager to leave the project
                End If
                If pI.ProjectRole = ParticipantRole.Tester Then
                    cmdBecomeTester.Visible = False
                    cmdStopTesting.Visible = True
                End If
                If pI.ProjectRole = ParticipantRole.Translator Then
                    cmdBecomeTranslator.Visible = False
                    cmdStopTranslating.Visible = True
                End If
            Next

        End Sub

        Private Sub LocalizeForm()

            lblParticipateHeading.Text = Localization.GetString("lblParticipateHeading", LocalResourceFile)
            lblParticipateIntro.Text = Localization.GetString("lblParticipateIntro", LocalResourceFile)

            lblDesignersCurrent.Text = Localization.GetString("lblDesignersCurrent", LocalResourceFile)
            lblDesignersDescription.Text = Localization.GetString("lblDesignersDescription", LocalResourceFile)
            lblDesignersHead.Text = Localization.GetString("lblDesignersHead", LocalResourceFile)
            lblDesignersNeeded.Text = Localization.GetString("OpenPositions", LocalResourceFile)
            cmdBecomeDesigner.Text = Localization.GetString("cmdBecomeDesigner", LocalResourceFile)
            cmdStopDesigning.Text = Localization.GetString("cmdLeave", LocalResourceFile)

            lblDevelopersCurrent.Text = Localization.GetString("lblDevelopersCurrent", LocalResourceFile)
            lblDevelopersDescription.Text = Localization.GetString("lblDevelopersDescription", LocalResourceFile)
            lblDevelopersHead.Text = Localization.GetString("lblDevelopersHead", LocalResourceFile)
            lblDevelopersNeeded.Text = Localization.GetString("OpenPositions", LocalResourceFile)
            cmdBecomeDeveloper.Text = Localization.GetString("cmdBecomeDeveloper", LocalResourceFile)
            cmdStopDeveloping.Text = Localization.GetString("cmdLeave", LocalResourceFile)

            lblManagersCurrent.Text = Localization.GetString("lblManagersCurrent", LocalResourceFile)
            lblManagersDescription.Text = Localization.GetString("lblManagersDescription", LocalResourceFile)
            lblManagersHead.Text = Localization.GetString("lblManagersHead", LocalResourceFile)
            lblManagersNeeded.Text = Localization.GetString("OpenPositions", LocalResourceFile)
            cmdBecomeManager.Text = Localization.GetString("cmdBecomeManager", LocalResourceFile)
            cmdStopManaging.Text = Localization.GetString("cmdLeave", LocalResourceFile)

            lblTestersCurrent.Text = Localization.GetString("lblTestersCurrent", LocalResourceFile)
            lblTestersDescription.Text = Localization.GetString("lblTestersDescription", LocalResourceFile)
            lblTestersHead.Text = Localization.GetString("lblTestersHead", LocalResourceFile)
            lblTestersNeeded.Text = Localization.GetString("OpenPositions", LocalResourceFile)
            cmdBecomeTester.Text = Localization.GetString("cmdBecomeTester", LocalResourceFile)
            cmdStopTesting.Text = Localization.GetString("cmdLeave", LocalResourceFile)

            lblTranslatorsCurrent.Text = Localization.GetString("lblTranslatorsCurrent", LocalResourceFile)
            lblTranslatorsDescription.Text = Localization.GetString("lblTranslatorsDescription", LocalResourceFile)
            lblTranslatorsHead.Text = Localization.GetString("lblTranslatorsHead", LocalResourceFile)
            lblTranlatorsNeeded.Text = Localization.GetString("OpenPositions", LocalResourceFile)
            cmdBecomeTranslator.Text = Localization.GetString("cmdBecomeTranslator", LocalResourceFile)
            cmdStopTranslating.Text = Localization.GetString("cmdLeave", LocalResourceFile)

        End Sub

#End Region

    End Class
End Namespace
