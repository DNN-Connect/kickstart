<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="frmFund.ascx.vb" Inherits="Connect.Modules.Kickstart.frmFund" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

<div class="kickstart-funding">      

    <h3><asp:Literal ID="lblFundingHeader" runat="server"></asp:Literal></h3>
    <p><asp:Literal ID="lblFundingIntro" runat="server"></asp:Literal></p>





    <div class="dnnForm dnnClear">

        <div class="dnnFormItem selectfunding">
            <dnn:Label id="lblSelectIncentive" runat="server"></dnn:Label>
            <asp:DropDownList ID="drpFundingAmount" runat="server" AutoPostBack="true"></asp:DropDownList>
        </div>

        <div class="dnnFormItem">
            <dnn:Label id="lblSelectedIncentive" runat="server"></dnn:Label>
            <div class="selectedIncentive">
                <span class="selectedIncentive"><asp:Literal ID="lblFundingIncentive" runat="server"></asp:Literal></span>
            </div>
        </div>

        <div class="dnnFormItem customfunding">
            <dnn:Label id="lblCustomFunding" runat="server"></dnn:Label>
            <dnn:DNNNumericTextBox ID="ctlCustomFunding" runat="server" Width="160px" ShowSpinButtons="false" MinValue="1" NumberFormat-DecimalDigits="0"></dnn:DNNNumericTextBox>
            <span class="currency"><asp:Literal ID="lblCurrency" runat="server"></asp:Literal></span>
        </div>

        <div class="dnnFormItem anonymousfunding">
            <dnn:Label id="lblAnonymousFunding" runat="server"></dnn:Label>
            <asp:CheckBox ID="chkAnonymous" runat="server" />
        </div>

    </div>

    <ul class="dnnActions">
        <li>
            <asp:LinkButton ID="cmdAddFunding" runat="server" CssClass="dnnPrimaryAction"></asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="cmdDeleteFunding" runat="server" CssClass="dnnSecondaryAction"></asp:LinkButton>
        </li>
    </ul>
       
</div>