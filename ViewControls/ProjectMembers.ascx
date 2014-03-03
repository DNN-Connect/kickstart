<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ProjectMembers.ascx.vb" Inherits="Connect.Modules.Kickstart.ProjectMembers" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

<div class="kickstart-members dnnClear">
        
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
        <FooterTemplate></ul></FooterTemplate>
    </asp:Repeater>

    <asp:Panel ID="pnlParticipate" runat="server">
        <ul class="dnnActions">
            <li>
                <asp:HyperLink ID="lnkParticipate" runat="server" CssClass="dnnPrimaryAction"></asp:HyperLink>
            </li>
        </ul>
    </asp:Panel>


   
</div>