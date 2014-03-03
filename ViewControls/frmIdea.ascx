<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="frmIdea.ascx.vb" Inherits="Connect.Modules.Kickstart.frmIdea" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="texteditor" Src="~/controls/texteditor.ascx" %>

<div class="kickstart-projectform">

    <div class="dnnForm">
        
        <div class="formItem">
            <h5><asp:Label ID="lblSubject" runat="server" resourcekey="lblSubject"></asp:Label></h5>
            <div class="control"><asp:TextBox ID="txtSubject" runat="server"></asp:TextBox></div>
        </div>

        <div class="formItem">
            <h5><asp:Label ID="lblSummary" runat="server" resourcekey="lblSummary"></asp:Label></h5>
            <div class="control"><asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine"></asp:TextBox></div>
        </div>

        <div class="formItem">
            <h5><asp:Label ID="lblContent" runat="server" resourcekey="lblContent"></asp:Label></h5>
            <div class="control"><dnn:texteditor id="txtContent" runat="server" height="400" width="100%"></dnn:texteditor></div>
        </div>

        <div class="formItem kickstart-note">
            <asp:Label ID="lblCreateNote" runat="server" resourcekey="lblCreateNote"></asp:Label>
        </div>

        <ul class="dnnActions">
            <li><asp:LinkButton ID="cmdCreate" runat="server" resourcekey="cmdCreate" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
            <li><asp:LinkButton ID="cmdCancel" runat="server" resourcekey="cmdCancel" CssClass="dnnSecondaryAction"></asp:LinkButton></li>
        </ul>

    </div>
    
</div>