<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="Settings.ascx.vb" Inherits="Connect.Modules.Kickstart.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

    <h2 id="dnnSitePanel-DisplaySettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("DisplaySettings")%></a></h2>

	<fieldset>

        <div class="dnnFormItem">
            <dnn:Label ID="lblViewMode" runat="server" /> 
            <asp:DropDownList ID="drpViewMode" runat="server" AutoPostBack="false">
                <asp:ListItem Text="ProjectsList" Value="ProjectsList"></asp:ListItem>
                <asp:ListItem Text="ProjectsDetail" Value="ProjectsDetail"></asp:ListItem>
                <asp:ListItem Text="ProjectMembers" Value="Participation"></asp:ListItem>
                <asp:ListItem Text="Funding" Value="Funding"></asp:ListItem>                
            </asp:DropDownList>            
        </div>

        <div class="dnnFormItem" runat="server" id="rowListTab" visible="true">
            <dnn:label ID="lblListTab" runat="server" />
            <asp:DropDownList ID="drpListTab" runat="server" DataTextField="IndentedTabName" DataValueField="TabId" />
        </div>

        <div class="dnnFormItem" runat="server" id="rowDetailsTab" visible="true">
            <dnn:label ID="lblDetailsTab" runat="server" />
            <asp:DropDownList ID="drpDetailsTab" runat="server" DataTextField="IndentedTabName" DataValueField="TabId" />
        </div>

        <div class="dnnFormItem">
            <dnn:label ID="lblModuleTheme" runat="server" />
            <asp:DropDownList ID="drpModuleTheme" runat="server" />
        </div>

    </fieldset>
