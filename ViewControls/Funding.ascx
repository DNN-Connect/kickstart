<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Funding.ascx.vb" Inherits="Connect.Modules.Kickstart.Funding" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

	<style>

	</style>

<div class="kickstart-projectmeter dnnClear">
    
    <h3><asp:Literal ID="lblFundingHead" runat="server"></asp:Literal></h3>

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
            <asp:HyperLink ID="lnkFund" runat="server" CssClass="dnnSecondaryAction"></asp:HyperLink>
        </li>
    </ul>

</div>