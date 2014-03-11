Imports DotNetNuke.Entities.Users
Imports Connect.Modules.Kickstart.Entities
Imports Connect.Modules.Kickstart.Entities.ParticipantInfo

Namespace Connect.Modules.Kickstart
    Public Class ProjectSidebar
        Inherits KickstartModuleBase


#Region "Event Handlers"

        Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

            DotNetNuke.Framework.AJAX.RegisterScriptManager()

            ReadQuerystring()
            If Project Is Nothing Then
                HideModule()
            End If

        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            BindStatus()
            BindParticipation()
            BindReleases()
            BindFunding()

        End Sub

        Private Sub rptTeam_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptTeam.ItemDataBound
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

                Dim pI As ParticipantInfo = CType(e.Item.DataItem, ParticipantInfo)

                Dim img As Image = CType(e.Item.FindControl("imgParticipant"), Image)
                If Not img Is Nothing Then
                    img.ImageUrl = pI.PhotoUrl
                End If

                Dim lnk As HyperLink = CType(e.Item.FindControl("lnkProfile"), HyperLink)
                If Not lnk Is Nothing Then
                    lnk.NavigateUrl = NavigateURL(PortalSettings.UserTabId, "", "UserId=" & pI.UserId.ToString)
                    lnk.Text = pI.Displayname
                End If

                Dim lblRole As Literal = CType(e.Item.FindControl("lblRole"), Literal)
                If Not lblRole Is Nothing Then
                    lblRole.Text = pI.ProjectRoles
                End If

            End If
        End Sub

#End Region

#Region "Templating"


#End Region

#Region "Private Methods"

        Private Sub BindStatus()

            Dim strPath As String = "~/Desktopmodules/Connect/Kickstart/images/"

            If Project.LeadBy = Null.NullInteger Then
                imgStatus.ImageUrl = strPath & "icn-status-waiting.png"
                lblStatus.Text = Utilities.GetSharedResource("status-waiting")
            Else

                If Project.IsDelivered Then
                    imgStatus.ImageUrl = strPath & "icn-status-delivered.png"
                    lblStatus.Text = Utilities.GetSharedResource("status-delivered")
                Else
                    imgStatus.ImageUrl = strPath & "icn-status-inprogress.png"
                    lblStatus.Text = Utilities.GetSharedResource("status-inprogress")
                End If

            End If


        End Sub

        Private Sub BindParticipation()

            If Project.LeadBy = Null.NullInteger Then
                pnlNoLead.Visible = True
                pnlMembers.Visible = False
                lblNoLead.Text = Utilities.GetSharedResource("NoLeadYetLong")
                lnkParticipate.Text = Utilities.GetSharedResource("BecomeLead")
                lnkParticipate.NavigateUrl = Utilities.NavigateUrl(KickstartSettings, ProjectId, Kickstart.ActionMode.BecomeLead)
                Exit Sub
            End If

            Dim objLead As UserInfo = UserController.GetUserById(PortalId, Project.LeadBy)
            If Not objLead Is Nothing Then

                lblOwnerHead.Text = Utilities.GetSharedResource("lblOwnerHead")
                imgLead.ImageUrl = "~/ProfilePic.ashx?UserId=" & objLead.UserID.ToString & "&h=32&w=32"
                lblLeadName.Text = objLead.DisplayName
                lnkLeadProfile.NavigateUrl = NavigateURL(PortalSettings.UserTabId, "", "UserId=" & objLead.UserID.ToString)
                lnkLeadProfile.Text = Utilities.GetSharedResource("FullProfileUrl")

                BindMembers()

                If Utilities.IsTeamMember(UserId, Project, False) Then
                    lnkParticipate.Text = Utilities.GetSharedResource("ManageParticipation")
                    lnkParticipate.NavigateUrl = Utilities.NavigateUrl(KickstartSettings, ProjectId, Kickstart.ActionMode.Participate)
                Else
                    If Config.HasOpenPositions Then
                        lnkParticipate.Text = Utilities.GetSharedResource("JoinTeam")
                        lnkParticipate.NavigateUrl = Utilities.NavigateUrl(KickstartSettings, ProjectId, Kickstart.ActionMode.Participate)
                    Else
                        lnkParticipate.Visible = False
                    End If
                End If

            End If

        End Sub

        Private Sub BindMembers()

            lblTeam.Text = Utilities.GetSharedResource("lblTeam")

            Dim lstCurrent As List(Of ParticipantInfo) = Utilities.GetParticipators(Project, PortalId)
            Dim lstMembers As New List(Of ParticipantInfo)
            For Each pI As ParticipantInfo In lstCurrent

                'If pI.UserId <> Project.LeadBy Then

                'End If
                lstMembers.Add(pI)
            Next

            If lstMembers.Count > 0 Then
                rptTeam.DataSource = lstMembers
                rptTeam.DataBind()
            Else
                pnlTeam.Visible = False
            End If

        End Sub

        Private Sub BindReleases()

            If Project.IsDelivered = False Then
                pnlRelease.Visible = False
                Exit Sub
            End If

            lblReleasesHead.Text = Utilities.GetSharedResource("lblReleasesHead")

            If Not String.IsNullOrEmpty(Config.CurrentVersion) Then
                lblCurrentReleaseVersionLabel.Text = Utilities.GetSharedResource("CurrentVersion")
                lblCurrentReleaseVersion.Text = Config.CurrentVersion
            End If

            If Not Project.DateDelivered = Null.NullDate Then
                lblCurrentReleaseDateLabel.Text = Utilities.GetSharedResource("CurrentVersionDate")
                lblCurrentReleaseDate.Text = Project.DateDelivered.ToShortDateString
            End If

            If Not String.IsNullOrEmpty(Config.DownloadUrl) Then
                lnkDownload.Text = Utilities.GetSharedResource("Download")
                lnkDownload.NavigateUrl = Config.DownloadUrl
            End If

        End Sub

        Private Sub BindFunding()

            If Config Is Nothing OrElse Config.NeedsFunding = False OrElse Project.LeadBy = Null.NullInteger Then
                pnlFunding.Visible = False
                Exit Sub
            End If

            lblFundingHead.Text = Utilities.GetSharedResource("lblFundingHead")

            If IsFunding() Then
                lnkFund.Text = Utilities.GetSharedResource("lnkManageFunding")
            Else
                lnkFund.Text = Utilities.GetSharedResource("lnkFund")
            End If

            Dim CurrentFunding As Decimal = FundingController.GetFunds(ProjectId)
            Dim NeededFunding As Decimal = Config.FundingNeeded
            Dim spanHtmlPercent As String = "5%"

            Select Case CurrentFunding / NeededFunding
                Case Is >= CDec(1.0)
                    spanHtmlPercent = "100%"
                Case Is >= CDec(0.90000000000000002)
                    spanHtmlPercent = "90%"
                Case Is >= CDec(0.80000000000000004)
                    spanHtmlPercent = "80%"
                Case Is >= CDec(0.69999999999999996)
                    spanHtmlPercent = "70%"
                Case Is >= CDec(0.59999999999999998)
                    spanHtmlPercent = "60%"
                Case Is >= CDec(0.5)
                    spanHtmlPercent = "50%"
                Case Is >= CDec(0.40000000000000002)
                    spanHtmlPercent = "40%"
                Case Is >= CDec(0.29999999999999999)
                    spanHtmlPercent = "30%"
                Case Is >= CDec(0.20000000000000001)
                    spanHtmlPercent = "20%"
                Case Is >= CDec(0.10000000000000001)
                    spanHtmlPercent = "10%"
            End Select

            lblMeterSpan.Text = "<span style=""width: " & spanHtmlPercent & """></span>"

            lblFundingNeeded.Text = String.Format(Utilities.GetSharedResource("lblFundingNeeded"), Utilities.FormatCurrency(Config.FundingCurrency), Utilities.FormatAmount(NeededFunding))
            lblFundingReached.Text = String.Format(Utilities.GetSharedResource("lblFundingReached"), Utilities.FormatCurrency(Config.FundingCurrency), Utilities.FormatAmount(CurrentFunding))


            Dim FundingUrl As String = Utilities.NavigateUrl(KickstartSettings, ProjectId, Kickstart.ActionMode.Fund)

            If Request.IsAuthenticated Then
                lnkFund.NavigateUrl = FundingUrl
            Else
                lnkFund.NavigateUrl = NavigateURL(PortalSettings.LoginTabId, "", "ReturnUrl=" & Server.UrlEncode(FundingUrl))
            End If


        End Sub

#End Region

    End Class
End Namespace
