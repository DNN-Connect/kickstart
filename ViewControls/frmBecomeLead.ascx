<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="frmBecomeLead.ascx.vb" Inherits="Connect.Modules.Kickstart.frmBecomeLead" %>


<div class="kickstart-project-becomelead">

    <h3><asp:Literal ID="lblLeadHeading" runat="server"></asp:Literal></h3>
    <p><asp:Literal ID="lblLeadIntro" runat="server"></asp:Literal></p>

    <ul class="dnnActions">
        <li>
            <asp:LinkButton ID="cmdBecomeLead" runat="server" CssClass="dnnPrimaryAction"></asp:LinkButton>
            <asp:LinkButton ID="cmdCancel" runat="server" CssClass="dnnSecondaryAction"></asp:LinkButton>
        </li>
    </ul>

</div>
