Imports DotNetNuke.Entities.Users
Imports Connect.Modules.Kickstart.Entities
Imports Connect.Modules.Kickstart.Entities.ParticipantInfo

Namespace Connect.Modules.Kickstart
    Public Class ProjectMembers
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

            Dim objLead As UserInfo = UserController.GetUserById(PortalId, Project.LeadBy)
            If Not objLead Is Nothing Then

                lblOwnerHead.Text = Localization.GetString("lblOwnerHead", LocalResourceFile)
                imgLead.ImageUrl = "~/ProfilePic.ashx?UserId=" & objLead.UserID.ToString & "&h=32&w=32"
                lblLeadName.Text = objLead.DisplayName
                lnkLeadProfile.NavigateUrl = NavigateURL(PortalSettings.UserTabId, "", "UserId=" & objLead.UserID.ToString)
                lnkLeadProfile.Text = Localization.GetString("FullProfileUrl", LocalResourceFile)

                BindMembers()

                If Utilities.IsTeamMember(UserId, ProjectId) Then
                    lnkParticipate.Text = Localization.GetString("JoinTeam", LocalResourceFile)
                Else
                    lnkParticipate.Text = Localization.GetString("ManageParticipation", LocalResourceFile)
                End If
                lnkParticipate.NavigateUrl = NavigateURL(KickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & ProjectId.ToString, "Action=Participate")

            End If


        End Sub

#End Region

#Region "Templating"


#End Region

#Region "Private Methods"

        Private Sub BindMembers()

            lblTeam.Text = Localization.GetString("lblTeam", LocalResourceFile)

            Dim lstCurrent As List(Of ParticipantInfo) = ParticipantController.ListByProject(ProjectId)

            For Each pI As ParticipantInfo In lstCurrent

                Dim oUser As UserInfo = UserController.GetUserById(PortalId, pI.UserId)
                pI.PhotoUrl = "~/ProfilePic.ashx?UserId=" & oUser.UserID.ToString & "&h=32&w=32"
                pI.Displayname = oUser.DisplayName

            Next

            rptTeam.DataSource = lstCurrent
            rptTeam.DataBind()

        End Sub
        

#End Region

        Private Sub rptTeam_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptTeam.ItemDataBound
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

                Dim pI As ParticipantInfo = CType(e.Item.DataItem, ParticipantInfo)

                Dim img As Image = CType(e.Item.FindControl("imgParticipant"), Image)
                If Not img Is Nothing Then
                    img.ImageUrl = pI.PhotoUrl
                End If

                Dim lnk As HyperLink = CType(e.Item.FindControl("lnkParticipant"), HyperLink)
                If Not lnk Is Nothing Then
                    lnk.NavigateUrl = NavigateURL(PortalSettings.UserTabId, "", "UserId=" & pI.UserId.ToString)
                    lnk.Text = pI.Displayname
                End If

                Dim lblRole As Literal = CType(e.Item.FindControl("lblRole"), Literal)
                If Not lblRole Is Nothing Then
                    lblRole.Text = pI.ProjectRole.ToString
                End If

            End If
        End Sub
    End Class
End Namespace
