
Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions


Namespace Connect.Modules.Kickstart
    Public Class Settings
        Inherits SettingsBase

#Region "Base Method Implementations"

        Public Overrides Sub LoadSettings()
            Try
                If (Page.IsPostBack = False) Then

                    BindThemes()
                    BindTabs()

                    Try 'since the theme folder might have been deleted by an unknown bastard we need to be careful here
                        If (Settings.Contains("Kickstart_ModuleTheme")) Then drpModuleTheme.SelectedValue = Settings("Kickstart_ModuleTheme").ToString()
                    Catch
                    End Try

                    If (Settings.Contains("Kickstart_ViewMode")) Then drpViewMode.SelectedValue = Settings("Kickstart_ViewMode").ToString()
                    If (Settings.Contains("Kickstart_ListTabId")) Then drpListTab.SelectedValue = Settings("Kickstart_ListTabId").ToString()
                    If (Settings.Contains("Kickstart_DetailsTabId")) Then drpDetailsTab.SelectedValue = Settings("Kickstart_DetailsTabId").ToString()


                End If
            Catch exc As Exception           'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

        Public Overrides Sub UpdateSettings()
            Try

                Dim objModules As New DotNetNuke.Entities.Modules.ModuleController

                objModules.UpdateModuleSetting(ModuleId, "Kickstart_ModuleTheme", drpModuleTheme.SelectedValue)
                objModules.UpdateModuleSetting(ModuleId, "Kickstart_ViewMode", drpViewMode.SelectedValue)
                objModules.UpdateModuleSetting(ModuleId, "Kickstart_ListTabId", drpListTab.SelectedValue)
                objModules.UpdateModuleSetting(ModuleId, "Kickstart_DetailsTabId", drpDetailsTab.SelectedValue)

            Catch exc As Exception           'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

#End Region

#Region "Private Methods"

        Private Sub BindThemes()

            drpModuleTheme.Items.Clear()
            Dim basepath As String = Server.MapPath(Me.TemplateSourceDirectory & "/templates/")

            For Each folder As String In System.IO.Directory.GetDirectories(basepath)
                Dim foldername As String = folder.Substring(folder.LastIndexOf("\") + 1)

                drpModuleTheme.Items.Add(New ListItem(foldername, foldername))

            Next

        End Sub

        Private Sub BindTabs()

            Dim tabs As System.Collections.Generic.List(Of DotNetNuke.Entities.Tabs.TabInfo) = TabController.GetPortalTabs(PortalId, Null.NullInteger, True, True, False, False)

            drpListTab.DataSource = tabs
            drpListTab.DataBind()
            drpDetailsTab.DataSource = tabs
            drpDetailsTab.DataBind()

        End Sub

#End Region

#Region "Event Handlers"


#End Region

    End Class
End Namespace
