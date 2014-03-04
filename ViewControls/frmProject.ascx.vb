Imports Connect.Modules.Kickstart.Entities
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security.Permissions

Namespace Connect.Modules.Kickstart
    Public Class frmProject
        Inherits KickstartModuleBase


#Region "Event Handlers"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

            ReadQuerystring()


            If Project Is Nothing Then
                Response.Redirect(NavigateURL(KickstartSettings.ProjectListTabId))
            End If

            If Not IsProjectLead() Then
                If TabPermissionController.CanAdminPage Then
                    lblWarning.Text = "You are seeing this control only because you have admin permssions on this module. Usually this can only be seen by project leads."
                    pnlWarning.Visible = True
                Else
                    Response.Redirect(NavigateURL(KickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & ProjectId.ToString))
                End If
            End If

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



            BindLocalization()

            If Not Project Is Nothing Then
                If Project.CreatedBy <> UserInfo.UserID Then
                    'todo: hide me
                End If
            End If


            If Not Page.IsPostBack Then

                BindConfig()
                BindIncentives()
                BindDescription()
                BindStatus()
                BindMembers()

                If Project.IsLocked Then
                    cmdUpdateConfig.Enabled = False
                    cmdAddIncentive.Enabled = False
                    pnlLocked.Visible = True
                    cmdLock.Visible = False
                    cmdUnlock.Visible = KickstartSettings.CanLockProject
                Else
                    cmdLock.Visible = KickstartSettings.CanLockProject
                    cmdUnlock.Visible = False
                End If

                If Project.IsDeleted Then
                    cmdUpdateConfig.Enabled = False
                    cmdAddIncentive.Enabled = False
                    pnlDeleted.Visible = True
                    cmdDelete.Visible = False
                    cmdRestore.Visible = KickstartSettings.CanDeleteProject
                Else
                    cmdDelete.Visible = KickstartSettings.CanDeleteProject
                    cmdRestore.Visible = False
                End If

            End If

        End Sub

        Protected Sub cmdUpdateConfig_Click(sender As Object, e As System.EventArgs) Handles cmdUpdateConfig.Click
            UpdateConfig()
            Response.Redirect(NavigateURL(TabId, "", "ProjectId=" & Project.ProjectId.ToString))
        End Sub

        Public Sub cmdEditIncentive_Click(sender As Object, e As System.EventArgs)
            Dim IncentiveId As Integer = Convert.ToInt32(CType(sender, ImageButton).CommandArgument)
            BindIncentive(IncentiveId)
        End Sub

        Public Sub cmdDeleteIncentive_Click(sender As Object, e As System.EventArgs)
            Dim IncentiveId As Integer = Convert.ToInt32(CType(sender, ImageButton).CommandArgument)
            IncentiveController.Delete(IncentiveId)
            RefreshPage()
        End Sub

        Protected Sub cmdAddIncentive_Click(sender As Object, e As System.EventArgs) Handles cmdAddIncentive.Click
            UpdateIncentive()
            RefreshPage()
        End Sub

        Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

            Response.Redirect(NavigateURL(TabId, "", "ProjectId=" & ProjectId))

        End Sub

        Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click

            ProjectController.Delete(Project, UserId)
            Response.Redirect(NavigateURL(KickstartSettings.ProjectListTabId))

        End Sub

        Private Sub cmdLock_Click(sender As Object, e As EventArgs) Handles cmdLock.Click

            ProjectController.Lock(Project, UserId)
            Response.Redirect(NavigateURL(TabId, "", "ProjectId=" & ProjectId))

        End Sub

        Private Sub cmdRestore_Click(sender As Object, e As EventArgs) Handles cmdRestore.Click

            ProjectController.Restore(Project, UserId)
            Response.Redirect(Request.RawUrl)

        End Sub

        Private Sub cmdUnlock_Click(sender As Object, e As EventArgs) Handles cmdUnlock.Click

            ProjectController.UnLock(Project, UserId)
            Response.Redirect(Request.RawUrl)

        End Sub

        Public Sub cmdDeleteMember_Click(sender As Object, e As System.EventArgs)

            Dim UserId As Integer = Convert.ToInt32(CType(sender, ImageButton).CommandArgument.Split(Char.Parse("|"))(0))
            Dim RoleId As Integer = Convert.ToInt32(CType(sender, ImageButton).CommandArgument.Split(Char.Parse("|"))(1))

            Dim lstCurrent As List(Of ParticipantInfo) = ParticipantController.ListByProject(ProjectId)
            For Each pI As ParticipantInfo In lstCurrent
                If pI.ProjectRole = RoleId And pI.UserId = UserId Then

                    'remove role from participant list
                    ParticipantController.Delete(pI.ParticipationId)

                    'check if still team member in another role maybe
                    If Not Utilities.IsTeamMember(UserId, Project) Then
                        Project.TeamMembers -= 1
                        ProjectController.Update(Project) 'reset team members count
                    End If

                    'notify the rest of the team
                    Integration.NotificationController.SendNewParticipationNotification(Project, pI, UserInfo, NavigateURL(TabId, "", "ProjectId=" & ProjectId.ToString), True)
                    Exit For

                End If
            Next

            RefreshPage()

        End Sub

#End Region

#Region "Templating"


#End Region

#Region "Private Methods"

        Private Sub BindMembers()

            Dim lstCurrent As List(Of ParticipantInfo) = ParticipantController.ListByProject(ProjectId)

            For Each pI As ParticipantInfo In lstCurrent

                Dim oUser As UserInfo = UserController.GetUserById(PortalId, pI.UserId)
                pI.PhotoUrl = "~/profilepic.ashx?userId=" & oUser.UserID.ToString & "&h=30&w=30"
                pI.Displayname = oUser.DisplayName

            Next

            rptTeam.DataSource = lstCurrent
            rptTeam.DataBind()

        End Sub

        Private Sub BindIncentive(IncentiveId As Integer)

            Dim objIncentive As IncentiveInfo = IncentiveController.Get(IncentiveId)
            If Not objIncentive Is Nothing Then
                txtIncentive.Text = objIncentive.Incentive
                ctlFundingAmount.Value = objIncentive.Amount
            End If

        End Sub

        Private Sub BindLocalization()

            lblFundingAmount.Text = Localization.GetString("lblFundingAmount", LocalResourceFile)
            lblHeading.Text = Localization.GetString("lblHeading", LocalResourceFile)
            lblIncentive.Text = Localization.GetString("lblIncentive", LocalResourceFile)
            lblIncentiveIntro.Text = Localization.GetString("lblIncentiveIntro", LocalResourceFile)
            lblFundingIntro.Text = Localization.GetString("lblFundingIntro", LocalResourceFile)
            lblIncentives.Text = Localization.GetString("lblIncentives", LocalResourceFile)

        End Sub

        Private Sub BindConfig()

            Dim config As ProjectConfigInfo = ConfigController.GetConfig(ProjectId)

            If config Is Nothing Then
                config = New ProjectConfigInfo
                config.DesignersNeeded = 0
                config.DevelopersNeeded = 0
                config.FundingNeeded = CDec(0.0)
                config.ManagersNeeded = 1
                config.TestersNeeded = 0
                config.TranslatorsNeeded = 0
                config.FundingCurrency = drpFundingCurrency.SelectedValue
                config.InitializedOnly = True
                ConfigController.UpdateConfig(ProjectId, config)
            End If

            ctlDevelopersNeeded.Value = Convert.ToDecimal(config.DevelopersNeeded)
            ctlDesignersNeeded.Value = Convert.ToDecimal(config.DesignersNeeded)
            ctlFundingNeeded.Value = Convert.ToDecimal(config.FundingNeeded)
            ctlManagersNeeded.Value = Convert.ToDecimal(config.ManagersNeeded)
            ctlTestersNeeded.Value = Convert.ToDecimal(config.TestersNeeded)
            ctlTranslatorsNeeded.Value = Convert.ToDecimal(config.TranslatorsNeeded)

            drpFundingCurrency.SelectedValue = config.FundingCurrency

        End Sub

        Private Sub UpdateConfig()

            Dim config As ProjectConfigInfo = ConfigController.GetConfig(ProjectId)
            If config Is Nothing Then
                config = New ProjectConfigInfo
            End If

            config.DevelopersNeeded = Convert.ToInt32(ctlDevelopersNeeded.Value)
            config.DesignersNeeded = Convert.ToInt32(ctlDesignersNeeded.Value)
            config.TestersNeeded = Convert.ToInt32(ctlTestersNeeded.Value)
            config.TranslatorsNeeded = Convert.ToInt32(ctlTranslatorsNeeded.Value)
            config.ManagersNeeded = Convert.ToInt32(ctlManagersNeeded.Value)
            config.FundingNeeded = Convert.ToDecimal(ctlFundingNeeded.Value)
            config.FundingCurrency = drpFundingCurrency.SelectedValue

            'make sure we store the original idea in the config 
            If config.IdeaTitle = "" Then
                config.IdeaTitle = Project.Subject
            End If
            If config.IdeaSummary = "" Then
                config.IdeaSummary = Project.Summary
            End If
            If config.IdeaDescription = "" Then
                config.IdeaDescription = Project.Content
            End If

            ConfigController.UpdateConfig(ProjectId, config)

            If txtTitle.Text.Length > 0 Then
                Project.Subject = txtTitle.Text
            End If
            If txtSummary.Text.Length > 0 Then
                Project.Summary = txtSummary.Text
            End If
            If txtContent.Text.Length > 0 Then
                Project.Content = txtContent.Text
            End If

            Project.Status = Convert.ToInt32(drpStatus.SelectedValue)
            If Not ctlDateScheduled.SelectedDate Is Nothing Then
                Project.DateScheduled = ctlDateScheduled.DbSelectedDate
            End If
            If drpProjectPlatform.SelectedIndex > 0 Then
                Project.ProjectPlatform = drpProjectPlatform.SelectedValue
            End If
            If txtPlatformRSSUrl.Text.Length > 0 Then
                Project.PlatformRssUrl = txtPlatformRSSUrl.Text.Trim
            End If
            If txtPlatformUrl.Text.Length > 0 Then
                Project.ProjectUrl = txtPlatformUrl.Text
            End If


            Dim blnDeliveryStatusChanged As Boolean = False
            Dim dtLastRelease As Date = Project.DateDelivered

            If chkIsDelivered.Checked Then
                Project.IsDelivered = True
                If Not ctlDateDelivered.SelectedDate Is Nothing Then
                    Project.DateDelivered = ctlDateDelivered.DbSelectedDate
                Else
                    If Project.DateDelivered = Null.NullDate Then 'no date selected yet
                        Project.DateDelivered = Date.Now
                    End If
                End If
            Else
                Project.IsDelivered = False
                Project.DateDelivered = Null.NullDate
            End If

            If dtLastRelease < Project.DateDelivered AndAlso Project.IsDelivered = True Then 'new release, notify subscribers!
                Integration.SubscriptionController.NotifySubscribersAboutNewRelease(PortalId, Project, NavigateURL(TabId, "", "ProjectId=" & ProjectId))
            End If

            ProjectController.Update(Project)

        End Sub

        Private Sub UpdateIncentive()

            Dim Amount As Decimal = ctlFundingAmount.DbValue
            Dim Incentive As String = txtIncentive.Text

            Dim objIncentive As IncentiveInfo = IncentiveController.GetByAmount(ProjectId, Amount)

            If Not objIncentive Is Nothing Then

                If String.IsNullOrEmpty(Incentive) Then
                    IncentiveController.Delete(objIncentive.IncentiveId)
                ElseIf Amount = CDec(0.0) Then
                    IncentiveController.Delete(objIncentive.IncentiveId)
                Else
                    objIncentive.Amount = Amount
                    objIncentive.Incentive = Incentive
                    IncentiveController.Update(objIncentive)
                End If
            Else

                If Not String.IsNullOrEmpty(Incentive) AndAlso Not (Amount = CDec(0.0)) Then

                    objIncentive = New IncentiveInfo
                    objIncentive.ProjectId = ProjectId
                    objIncentive.Amount = Amount
                    objIncentive.Incentive = Incentive
                    IncentiveController.Add(objIncentive)

                End If

            End If

        End Sub

        Private Sub BindIncentives()

            Dim incentives As New List(Of IncentiveInfo)
            incentives = IncentiveController.ListByProject(ProjectId)

            rptIncentives.DataSource = incentives
            rptIncentives.DataBind()

        End Sub

        Private Sub BindDescription()
            txtTitle.Text = Project.Subject
            txtSummary.Text = Project.Summary
            txtContent.Text = Project.Content
        End Sub

        Private Sub BindStatus()

            drpStatus.Items.Clear()
            For Each Status As String In [Enum].GetNames(GetType(Enums.ProjectStatus))
                drpStatus.Items.Add(New ListItem(Status, [Enum].Parse(GetType(Enums.ProjectStatus), Status)))
            Next

            drpStatus.SelectedValue = Project.Status.ToString

            chkIsDelivered.Checked = Project.IsDelivered
            If Project.DateDelivered > Null.NullDate AndAlso Project.IsDelivered Then
                ctlDateDelivered.SelectedDate = Project.DateDelivered
            End If
            If Project.DateScheduled > Null.NullDate Then
                ctlDateScheduled.SelectedDate = Project.DateScheduled
            End If
            txtPlatformRSSUrl.Text = Project.PlatformRssUrl
            txtPlatformUrl.Text = Project.ProjectUrl
            Try
                drpProjectPlatform.Items.FindByValue(Project.ProjectPlatform).Selected = True
            Catch
            End Try

        End Sub

#End Region

#Region "Public Methods"

        Public Function GetCurrency() As String
            Return Utilities.FormatCurrency(Config.FundingCurrency)
        End Function

#End Region

    End Class
End Namespace
