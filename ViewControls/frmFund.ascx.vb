Imports Connect.Modules.Kickstart.Entities
Imports Connect.Modules.Kickstart.Entities.ParticipantInfo

Namespace Connect.Modules.Kickstart
    Public Class frmFund
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

            BindLocalization()

            If Not Page.IsPostBack Then

                BindFunding()
                BindFundingOptions()

            End If

        End Sub

        Protected Sub cmdAddFunding_Click(sender As Object, e As System.EventArgs) Handles cmdAddFunding.Click

            If Request.IsAuthenticated = False Then
                Response.Redirect(NavigateURL(PortalSettings.RegisterTabId, "", "ReturnUrl=" & Server.UrlEncode(ProjectUrl(ProjectId))))
            End If

            UpdateFunding()
            RefreshPage()

        End Sub

        Protected Sub cmdDeleteFunding_Click(sender As Object, e As System.EventArgs) Handles cmdDeleteFunding.Click

            If Request.IsAuthenticated = False Then
                Response.Redirect(NavigateURL(PortalSettings.RegisterTabId, "", "ReturnUrl=" & Server.UrlEncode(ProjectUrl(ProjectId))))
            End If

            RemoveFunding()
            RefreshPage()
        End Sub

        Protected Sub drpFundingAmount_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpFundingAmount.SelectedIndexChanged
            BindIncentiveDescription()
        End Sub

#End Region

#Region "Templating"


#End Region

#Region "Private Methods"

        Private Sub BindIncentiveDescription()

            Dim incentives As New List(Of IncentiveInfo)
            incentives = IncentiveController.ListByProject(ProjectId)

            For Each i As IncentiveInfo In incentives
                If i.IncentiveId.ToString = drpFundingAmount.SelectedValue Then
                    lblFundingIncentive.Text = i.Incentive
                End If
            Next

        End Sub

        Private Sub BindLocalization()

            lblFundingHeader.Text = Localization.GetString("lblFundingHeader", LocalResourceFile)
            lblFundingIntro.Text = Localization.GetString("lblFundingIntro", LocalResourceFile)
            lblCustomFunding.Text = Localization.GetString("lblCustomFunding", LocalResourceFile)
            cmdAddFunding.Text = Localization.GetString("cmdAddFunding", LocalResourceFile)
            cmdDeleteFunding.Text = Localization.GetString("cmdDeleteFunding", LocalResourceFile)
            lblCurrency.Text = Utilities.FormatCurrency(ConfigController.GetConfig(ProjectId).FundingCurrency)

        End Sub

        Private Sub BindFundingOptions()

            Dim incentives As New List(Of IncentiveInfo)
            incentives = IncentiveController.ListByProject(ProjectId)

            drpFundingAmount.Items.Clear()

            For Each i As IncentiveInfo In incentives
                Dim item As New ListItem
                item.Text = Utilities.FormatCurrency(ConfigController.GetConfig(ProjectId).FundingCurrency) & " " & Utilities.FormatAmount(i.Amount)
                item.Value = i.IncentiveId.ToString
                drpFundingAmount.Items.Add(item)
            Next

            If incentives.Count > 0 Then
                lblFundingIncentive.Text = incentives(0).Incentive
            End If


        End Sub

        Private Sub UpdateFunding()

            Dim objFunding As FundingInfo = Nothing

            If IsFunding() Then
                objFunding = MyFunding()
            Else
                objFunding = New FundingInfo
            End If

            objFunding.FundingReceived = False
            objFunding.Anonymous = chkAnonymous.Checked
            objFunding.ProjectId = ProjectId
            objFunding.UserId = UserId
            If ctlCustomFunding.DbValue > CDec(0.0) Then
                objFunding.Funding = ctlCustomFunding.DbValue
            Else
                objFunding.Funding = IncentiveController.Get(Convert.ToInt32(drpFundingAmount.SelectedValue)).Amount
            End If

            If IsFunding() Then

                FundingController.Update(objFunding)
                Integration.NotificationController.SendNewFundingNotification(Project, objFunding, UserInfo, NavigateURL(KickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & ProjectId.ToString), True, False)

            Else

                If objFunding.Anonymous = False Then
                    Integration.JournalController.AddFundingToJournal(Project, objFunding, PortalId, TabId, UserId, NavigateURL(KickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & ProjectId.ToString), String.Format(Utilities.GetSharedResource("AddedFunding"), UserInfo.DisplayName))
                End If

                FundingController.Add(objFunding)
                Integration.NotificationController.SendNewFundingNotification(Project, objFunding, UserInfo, NavigateURL(KickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & ProjectId.ToString), False, False)

            End If

        End Sub

        Private Sub BindFunding()

            If IsFunding() Then
                cmdDeleteFunding.Visible = True
                cmdAddFunding.Text = Localization.GetString("cmdUpdateFunding", LocalResourceFile)
                lblFundingIntro.Text = String.Format(Localization.GetString("lblMyFunding", LocalResourceFile), MyFundingAsString)
                If MyFunding.Anonymous Then
                    lblFundingIntro.Text += " " & Localization.GetString("anonymously", LocalResourceFile)
                    chkAnonymous.Checked = True
                End If
            Else
                cmdDeleteFunding.Visible = False
            End If

        End Sub

        Private Sub RemoveFunding()

            Dim originalFunding As FundingInfo = MyFunding()
            If Not originalFunding Is Nothing Then
                FundingController.Delete(originalFunding.FundingId)
                Integration.NotificationController.SendNewFundingNotification(Project, originalFunding, UserInfo, NavigateURL(KickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & ProjectId.ToString), False, True)
            End If
        End Sub

#End Region

    End Class
End Namespace
