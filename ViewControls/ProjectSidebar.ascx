<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ProjectSidebar.ascx.vb" Inherits="Connect.Modules.Kickstart.ProjectSidebar" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

<div class="kickstart-sidebar">

    <asp:Panel ID="pnlStatus" runat="server" CssClass="kickstart-sidebarpanel pnlStatus dnnClear">
        <p class="status">
            <asp:Image ID="imgStatus" runat="server" />
            <br />
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
        </p>
    </asp:Panel>

    <asp:Panel ID="pnlFunding" runat="server" CssClass="kickstart-sidebarpanel pnlFunding dnnClear">

        <h4><asp:Literal ID="lblFundingHead" runat="server"></asp:Literal></h4>

        <div class="funding-needed">
            <span class="funding-needed"><asp:Literal ID="lblFundingNeeded" runat="server"></asp:Literal></span>
        </div>
        <div class="funding-reached">
            <span class="funding-reached"><asp:Literal ID="lblFundingReached" runat="server"></asp:Literal></span>
        </div>
        <div class="meter orange nostripes">
	        <asp:Literal ID="lblMeterSpan" runat="server"></asp:Literal>
        </div>
        <ul class="dnnActions">
            <li>
                <asp:HyperLink ID="lnkFund" runat="server" CssClass="dnnPrimaryAction kickstart-fund"></asp:HyperLink>
            </li>
        </ul>


    </asp:Panel>

    <asp:Panel ID="pnlParticipation" runat="server" CssClass="kickstart-sidebarpanel pnlMembers dnnClear">
   
        <asp:Panel ID="pnlMembers" runat="server">

            <h4><asp:Literal ID="lblOwnerHead" runat="server"></asp:Literal></h4>            
            
            <div class="kickstart-participant-item dnnClear">
                <div class="kickstart-participantitem-avatar">
                    <asp:Image ID="imgLead" runat="server" />
                </div>
                <div class="kickstart-participantitem-data">
                    <div class="kickstart-participantitem-name"><asp:Literal id="lblLeadName" runat="server"></asp:Literal></div>
                    <div class="kickstart-participantitem-role"><asp:Hyperlink id="lnkLeadProfile" runat="server" Text="Full Profile"></asp:Hyperlink></div>
                </div>
            </div>

            <asp:Panel ID="pnlTeam" runat="server">

                <h4><asp:Literal ID="lblTeam" runat="server"></asp:Literal></h4>

                <asp:Repeater ID="rptTeam" runat="server">
                    <HeaderTemplate><div class="kickstart-participant-items dnnClear"></HeaderTemplate>
                    <ItemTemplate>
                        <div class="kickstart-participant-item dnnClear">
                            <div class="kickstart-participantitem-avatar">
                                <asp:Image ID="imgParticipant" runat="server" />
                            </div>
                            <div class="kickstart-participantitem-data">
                                <div class="kickstart-participantitem-name"><asp:HyperLink ID="lnkProfile" runat="server"></asp:HyperLink></div>
                                <div class="kickstart-participantitem-role"><asp:Literal ID="lblRole" runat="server"></asp:Literal></div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate></div></FooterTemplate>
                </asp:Repeater>

            </asp:Panel>
           
        </asp:Panel>

        <asp:Panel ID="pnlNoLead" runat="server">

            <p><asp:Literal ID="lblNoLead" runat="server"></asp:Literal></p>

        </asp:Panel>

        <asp:Panel ID="pnlParticipate" runat="server">
            <ul class="dnnActions">
                <li>
                    <asp:HyperLink ID="lnkParticipate" runat="server" CssClass="dnnPrimaryAction"></asp:HyperLink>
                </li>
            </ul>
        </asp:Panel>
   
    </asp:Panel>

    <asp:Panel ID="pnlRelease" runat="server" CssClass="kickstart-sidebarpanel pnlRelease dnnClear">

        <h4><asp:Literal ID="lblReleasesHead" runat="server"></asp:Literal></h4>
        <p><asp:Literal ID="lblCurrentReleaseVersionLabel" runat="server"></asp:Literal> <span class="kickstart-currentrelease-version"><asp:Literal ID="lblCurrentReleaseVersion" runat="server"></asp:Literal></span></p>
        <p><asp:Literal ID="lblCurrentReleaseDateLabel" runat="server"></asp:Literal> <span class="kickstart-currentrelease-date"><asp:Literal ID="lblCurrentReleaseDate" runat="server"></asp:Literal></span></p>
        <ul class="dnnActions">
            <li>
                <asp:HyperLink ID="lnkDownload" runat="server" CssClass="dnnPrimaryAction kickstart-download"></asp:HyperLink>
            </li>
        </ul>

    </asp:Panel>

</div>