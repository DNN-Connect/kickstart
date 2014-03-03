<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="frmProject.ascx.vb" Inherits="Connect.Modules.Kickstart.frmProject" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="texteditor" Src="~/controls/texteditor.ascx" %>

<asp:Panel ID="pnlWarning" runat="server" Visible="false">
    <asp:Literal ID="lblWarning" runat="server"></asp:Literal>
</asp:Panel>

<div class="dnnForm dnnClear kickstart-config" id="kickstartConfigform">

        <h3><asp:Literal ID="lblHeading" runat="server"></asp:Literal></h3>
        
        <h2 id="dnnSitePanel-DisplaySettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%= LocalizeString("lblResourcesQuestion")%></a></h2>

        <fieldset class="kickstart-config-resources">
            <div class="dnnFormItem">
                <dnn:label id="lblManagersNeeded" controlname="ctlManagersNeeded" runat="server" />
                <dnn:DnnNumericTextBox id="ctlManagersNeeded" runat="server" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" MinValue="1" Width="80px"></dnn:DnnNumericTextBox>                
            </div>
            <div class="dnnFormItem">
                <dnn:label id="lblDevelopersNeeded" controlname="ctlDevelopersNeeded" runat="server" />
                <dnn:DnnNumericTextBox id="ctlDevelopersNeeded" runat="server" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" MinValue="0" Width="80px"></dnn:DnnNumericTextBox>                
            </div>
            <div class="dnnFormItem">
                <dnn:label id="lblDesignersNeeded" controlname="ctlDesignersNeeded" runat="server" />
                <dnn:DnnNumericTextBox id="ctlDesignersNeeded" runat="server" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" MinValue="0" Width="80px"></dnn:DnnNumericTextBox>                
            </div>
            <div class="dnnFormItem">
                <dnn:label id="lblTranslatorsNeeded" controlname="ctlTranslatorsNeeded" runat="server" />
                <dnn:DnnNumericTextBox id="ctlTranslatorsNeeded" runat="server" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" MinValue="0" Width="80px"></dnn:DnnNumericTextBox>                
            </div>
            <div class="dnnFormItem">
                <dnn:label id="lblTestersNeeded" controlname="ctlTestersNeeded" runat="server" />
                <dnn:DnnNumericTextBox id="ctlTestersNeeded" runat="server" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" MinValue="0" Width="80px"></dnn:DnnNumericTextBox>                
            </div>
        
        </fieldset>
    
        <h2 id="dnnSitePanel-DisplaySettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%= LocalizeString("lblFundingQuestion")%></a></h2>

        <fieldset class="kickstart-config-funding">
            
            <p><asp:Literal ID="lblFundingIntro" runat="server"></asp:Literal></p>

            <div class="dnnFormItem">
                <table>
                    <tr>
                        <td class="currencyinput"><asp:DropDownList ID="drpFundingCurrency" runat="server" Width="60"><asp:ListItem Text="$" Value="USD"></asp:ListItem><asp:ListItem Text="€" Value="EUR"></asp:ListItem></asp:DropDownList></td>
                        <td class="fundinginput"><dnn:DnnNumericTextBox ID="ctlFundingNeeded" runat="server" ShowSpinButtons="false" NumberFormat-DecimalDigits="0" Width="160px" MinValue="0"></dnn:DnnNumericTextBox></td>
                    </tr>
                </table>    
            </div>

            <asp:Panel ID="pnlIncentives" runat="server" CssClass="dnnClear kickstart-incentives">

                <p class="strong"><asp:Literal ID="lblIncentives" runat="server"></asp:Literal></p>
                <p><asp:Literal ID="lblIncentiveIntro" runat="server"></asp:Literal></p>

                <div class="dnnLeft">
                    <div class="dnnFormItem funding-amount">
                        <dnn:label id="lblFundingAmount" controlname="ctlFundingAmount" runat="server" />
                        <dnn:DnnNumericTextBox id="ctlFundingAmount" runat="server" ShowSpinButtons="false" NumberFormat-DecimalDigits="0"></dnn:DnnNumericTextBox>
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label id="lblIncentive" controlname="txtIncentive" runat="server" />
                        <dnn:DnnTextBox ID="txtIncentive" runat="server" TextMode="MultiLine" Height="50px"></dnn:DnnTextBox>
                    </div>


                </div>
                <div class="dnnLeft">
                    <asp:Repeater ID="rptIncentives" runat="server">
                        <HeaderTemplate><table cellpadding="0" cellspacing="0"></HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:left;padding-right:5px;padding-top:5px;"><%# GetCurrency()%></td>
                                <td style="text-align: right;padding-top:5px;"> <%# Connect.Modules.Kickstart.Utilities.FormatAmount(DataBinder.Eval(Container.DataItem, "Amount"))%>:</td>
                                <td style="padding-left: 10px;padding-top:5px;"><%# DataBinder.Eval(Container.DataItem, "Incentive")%></td>
                                <td style="padding-left: 20px;padding-top:5px;"><asp:ImageButton ID="cmdEditIncentive" runat="server" OnClick="cmdEditIncentive_Click" ImageUrl="~/Desktopmodules/Connect/Kickstart/images/edit.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IncentiveId")%>' />&nbsp;<asp:ImageButton ID="cmdDeleteIncentive" runat="server" OnClick="cmdDeleteIncentive_Click" ImageUrl="~/Desktopmodules/Connect/Kickstart/images/delete.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IncentiveId")%>' /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></table></FooterTemplate>
                    </asp:Repeater>

                    <ul class="dnnActions dnnClear">
                        <li>
                            <asp:LinkButton ID="cmdAddIncentive" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdAddIncentive" />
                        </li>
                    </ul>

                </div>
            </asp:Panel>

        </fieldset>

        <h2 id="dnnSitePanel-DisplaySettings" class="dnnFormSectionHead"><a href="" class="dnnSectionCollapsed"><%= LocalizeString("lblMetaDescription")%></a></h2>

        <fieldset>

            <div class="dnnFormItem">
                <dnn:label id="lblTitle" controlname="txtTitle" runat="server" />
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </div>
            <div class="dnnFormItem">
                <dnn:label id="lblSummary" controlname="txtSummary" runat="server" />
                <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" Height="100"></asp:TextBox>
            </div>
            <div class="dnnFormItem">
                <dnn:label id="lblDescription" controlname="txtDescription" runat="server" />
                <dnn:texteditor id="txtContent" runat="server" height="400" width="100%"></dnn:texteditor>
            </div>

        </fieldset>

        <h2 id="dnnSitePanel-DisplaySettings" class="dnnFormSectionHead"><a href="" class="dnnSectionCollapsed"><%= LocalizeString("lblPublishingDetails")%></a></h2>

        <fieldset class="kickstart-config-publishing">

            <div class="dnnFormItem">
                <dnn:label id="lblStatus" controlname="drpStatus" runat="server" />
                <asp:DropDownList ID="drpStatus" runat="server"></asp:DropDownList>
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lblDateScheduled" controlname="ctlDateScheduled" runat="server" />
                <dnn:DnnDatePicker id="ctlDateScheduled" runat="server" ></dnn:DnnDatePicker>
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lblProjectPlatform" controlname="drpProjectPlatform" runat="server" />
                <asp:DropDownList ID="drpProjectPlatform" runat="server">
                    <asp:ListItem Text="No platform yet" Value="noplatform"></asp:ListItem>
                    <asp:ListItem Text="Github" Value="github"></asp:ListItem>
                    <asp:ListItem Text="Codeplex" Value="codeplex"></asp:ListItem>
                    <asp:ListItem Text="SourceForge" Value="sourceforge"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="other"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lblPlatformRSSUrl" controlname="txtPlatformRSSUrl" runat="server" />
                <asp:TextBox ID="txtPlatformRSSUrl" runat="server"></asp:TextBox>
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lblPlatformUrl" controlname="txtPlatformUrl" runat="server" />
                <asp:TextBox ID="txtPlatformUrl" runat="server"></asp:TextBox>
            </div>

            <div class="dnnFormItem checkbox">
                <dnn:label id="lblIsDelivered" controlname="chkIsDelivered" runat="server" />
                <asp:CheckBox ID="chkIsDelivered" runat="server" />
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lblDateDelivered" controlname="ctlDateDelivered" runat="server" />
                <dnn:DnnDatePicker id="ctlDateDelivered" runat="server" ></dnn:DnnDatePicker>
            </div>

        </fieldset>

        <h2 id="dnnSitePanel-DisplaySettings" class="dnnFormSectionHead"><a href="" class="dnnSectionCollapsed"><%= LocalizeString("lblTeamMembers")%></a></h2>

        <fieldset class="kickstart-config-team">

            <asp:Repeater ID="rptTeam" runat="server">
                <HeaderTemplate><table cellpadding="0" cellspacing="0"></HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align:left;padding-right:5px;padding-top:5px;"><img src='<%# DataBinder.Eval(Container.DataItem, "PhotoUrl")%>' /></td>
                        <td style="text-align:left;padding-right:5px;padding-top:5px;"><%# DataBinder.Eval(Container.DataItem, "Displayname")%></td>
                        <td style="text-align:left;padding-right:5px;padding-top:5px;"><%# DataBinder.Eval(Container.DataItem, "ProjectRole")%></td>
                        <td style="padding-left: 20px;padding-top:5px;"><asp:ImageButton ID="cmdDeleteMember" runat="server" OnClick="cmdDeleteMember_Click" ImageUrl="~/Desktopmodules/Connect/Kickstart/images/delete.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserId") & "|" & DataBinder.Eval(Container.DataItem, "ProjectRole")%>' /></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>

        </fieldset>
</div>

<asp:Panel ID="pnlLocked" runat="server" Visible="false" CssClass="kickstart-locked-note">
    <asp:Label ID="lblLockedNote" runat="server" resourcekey="lblLockedNote"></asp:Label>
</asp:Panel>
<asp:Panel ID="pnlDeleted" runat="server" Visible="false" CssClass="kickstart-deleted-note">
    <asp:Label ID="lblDeletedNote" runat="server" resourcekey="lblDeletedNote"></asp:Label>
</asp:Panel>

<ul class="dnnActions dnnClear kickstart-actions">
    <li>
        <asp:LinkButton ID="cmdUpdateConfig" runat="server" CssClass="dnnPrimaryAction" resourcekey="cmdUpdateConfig" />
        <asp:LinkButton ID="cmdCancel" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdCancel" />
        <asp:LinkButton ID="cmdDelete" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdDelete" />
        <asp:LinkButton ID="cmdRestore" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdRestore" />
        <asp:LinkButton ID="cmdLock" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdLock" />
        <asp:LinkButton ID="cmdUnlock" runat="server" CssClass="dnnSecondaryAction" resourcekey="cmdUnlock" />
    </li>
</ul>
    

<script type="text/javascript">

    function setupLayout() {
        $('#kickstartConfigform').dnnPanels();
    }
    (function ($, Sys) {
        $(document).ready(function () {
            setupLayout();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setupLayout();
            });
        });
    }($, window.Sys));
</script>