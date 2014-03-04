<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="frmParticipate.ascx.vb" Inherits="Connect.Modules.Kickstart.frmParticipate" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

<div class="kickstart-participate">      

    <h2><asp:Literal ID="lblParticipateHeading" runat="server"></asp:Literal></h2>
    <p><asp:Literal ID="lblParticipateIntro" runat="server"></asp:Literal></p>

    <asp:Panel ID="pnlManagers" runat="server" CssClass="dnnClear kickstart-rolepanel managers">

        <div class="kickstart-roledescription">
            <h3><asp:literal id="lblManagersHead" runat="server"></asp:literal></h3>
            <p><asp:Literal ID="lblManagersDescription" runat="server"></asp:Literal></p>
        </div>

        <div class="kickstart-roledata">
            <h4><asp:literal ID="lblManagersCurrent" runat="server"></asp:literal></h4>
            <asp:Panel ID="pnlManagersCurrent" runat="server" CssClass="kickstart-currentmembers"></asp:Panel>
            <p><asp:Label ID="lblManagersNeeded" runat="server" CssClass="kickstart-membersneeded"></asp:Label></p>
            <ul class="dnnActions">
                <li><asp:LinkButton ID="cmdBecomeManager" runat="server" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
                <li><asp:LinkButton ID="cmdStopManaging" runat="server" Visible="false" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
            </ul>
        </div>
       
    </asp:Panel>

    <asp:Panel ID="pnlDevelopers" runat="server" CssClass="dnnClear kickstart-rolepanel developers">

        <div class="kickstart-roledescription">
            <h3><asp:Literal id="lblDevelopersHead" runat="server"></asp:Literal></h3>
            <p><asp:Literal ID="lblDevelopersDescription" runat="server"></asp:Literal></p>
        </div>

        <div class="kickstart-roledata">
            <h4><asp:literal ID="lblDevelopersCurrent" runat="server"></asp:literal></h4>
            <asp:Panel ID="pnlDevelopersCurrent" runat="server" CssClass="kickstart-currentmembers"></asp:Panel>
            <p><asp:Label ID="lblDevelopersNeeded" runat="server" CssClass="kickstart-membersneeded"></asp:Label></p>
            <ul class="dnnActions">
                <li><asp:LinkButton ID="cmdBecomeDeveloper" runat="server" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
                <li><asp:LinkButton ID="cmdStopDeveloping" runat="server" Visible="false" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
            </ul>
        </div>
        
    </asp:Panel>

    <asp:Panel ID="pnlDesigners" runat="server" CssClass="dnnClear kickstart-rolepanel designers">

        <div class="kickstart-roledescription">
            <h3><asp:Literal id="lblDesignersHead" runat="server"></asp:Literal></h3>
            <p><asp:Literal ID="lblDesignersDescription" runat="server"></asp:Literal></p>
        </div>

        <div class="kickstart-roledata">
            <h4><asp:literal ID="lblDesignersCurrent" runat="server"></asp:literal></h4>
            <asp:Panel ID="pnlDesignersCurrent" runat="server" CssClass="kickstart-currentmembers"></asp:Panel>
            <p><asp:Label ID="lblDesignersNeeded" runat="server" CssClass="kickstart-membersneeded"></asp:Label></p>
            <ul class="dnnActions">
                <li><asp:LinkButton ID="cmdBecomeDesigner" runat="server" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
                <li><asp:LinkButton ID="cmdStopDesigning" runat="server" Visible="false" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
            </ul>
        </div>

    </asp:Panel>

    <asp:Panel ID="pnlTranslators" runat="server" CssClass="dnnClear kickstart-rolepanel translators">

        <div class="kickstart-roledescription">
            <h3><asp:Literal id="lblTranslatorsHead" runat="server"></asp:Literal></h3>
            <p><asp:Literal ID="lblTranslatorsDescription" runat="server"></asp:Literal></p>
        </div>

        <div class="kickstart-roledata">
            <h4><asp:literal ID="lblTranslatorsCurrent" runat="server"></asp:literal></h4>
            <asp:Panel ID="pnlTranslatotsCurrent" runat="server" CssClass="kickstart-currentmembers"></asp:Panel>
            <p><asp:Label ID="lblTranlatorsNeeded" runat="server" CssClass="kickstart-membersneeded"></asp:Label> </p>
            <ul class="dnnActions">
                <li><asp:LinkButton ID="cmdBecomeTranslator" runat="server" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
                <li><asp:LinkButton ID="cmdStopTranslating" runat="server" Visible="false" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
            </ul>
        </div>

    </asp:Panel>

    <asp:Panel ID="pnlTesters" runat="server" CssClass="dnnClear kickstart-rolepanel testers">

        <div class="kickstart-roledescription">
            <h3><asp:Literal id="lblTestersHead" runat="server"></asp:Literal></h3>
            <p><asp:Literal ID="lblTestersDescription" runat="server"></asp:Literal></p>
        </div>

        <div class="kickstart-roledata">
            <h4><asp:literal ID="lblTestersCurrent" runat="server"></asp:literal></h4>
            <asp:Panel ID="pnlTestersCurrent" runat="server" CssClass="kickstart-currentmembers"></asp:Panel>
            <p><asp:Label ID="lblTestersNeeded" runat="server" CssClass="kickstart-membersneeded"></asp:Label></p>
            <ul class="dnnActions">
                <li><asp:LinkButton ID="cmdBecomeTester" runat="server" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
                <li><asp:LinkButton ID="cmdStopTesting" runat="server" Visible="false" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
            </ul>
        </div>
       
    </asp:Panel>

    <ul class="dnnActions">
        <li><asp:LinkButton ID="cmdEditProject" runat="server" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
        <li><asp:LinkButton ID="cmdCancel" runat="server" CssClass="dnnSecondaryAction"></asp:LinkButton></li>
    </ul>

   
</div>