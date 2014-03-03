Imports Microsoft.VisualBasic
Imports DotNetNuke
Imports DotNetNuke.Common.Utilities
Imports Connect.Modules.Kickstart.Entities


Namespace Connect.Modules.Kickstart

    Public Class ConfigController

        Public Shared Sub UpdateConfig(ProjectId As Integer, objConfig As ProjectConfigInfo)

            Dim dicConfig As New Dictionary(Of String, String)
            Dim dr As IDataReader = CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_ProjectConfig_ListByProject", ProjectId), IDataReader)
            While dr.Read
                dicConfig.Add(Convert.ToString(dr("Value")), Convert.ToString(dr("Setting")))
            End While
            dr.Close()
            dr.Dispose()

            If Not dicConfig.ContainsKey("IdeaTitle") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "IdeaTitle", objConfig.IdeaTitle)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "IdeaTitle", objConfig.IdeaTitle)
            End If

            If Not dicConfig.ContainsKey("IdeaSummary") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "IdeaSummary", objConfig.IdeaSummary)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "IdeaSummary", objConfig.IdeaSummary)
            End If

            If Not dicConfig.ContainsKey("IdeaDescription") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "IdeaDescription", objConfig.IdeaDescription)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "IdeaDescription", objConfig.IdeaDescription)
            End If

            If Not dicConfig.ContainsKey("DesignersNeeded") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "DesignersNeeded", objConfig.DesignersNeeded.ToString)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "DesignersNeeded", objConfig.DesignersNeeded.ToString)
            End If

            If Not dicConfig.ContainsKey("DevelopersNeeded") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "DevelopersNeeded", objConfig.DevelopersNeeded.ToString)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "DevelopersNeeded", objConfig.DevelopersNeeded.ToString)
            End If

            If Not dicConfig.ContainsKey("FundingNeeded") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "FundingNeeded", objConfig.FundingNeeded.ToString)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "FundingNeeded", objConfig.FundingNeeded.ToString)
            End If

            If Not dicConfig.ContainsKey("ManagersNeeded") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "ManagersNeeded", objConfig.ManagersNeeded.ToString)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "ManagersNeeded", objConfig.ManagersNeeded.ToString)
            End If

            If Not dicConfig.ContainsKey("TestersNeeded") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "TestersNeeded", objConfig.TestersNeeded.ToString)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "TestersNeeded", objConfig.TestersNeeded.ToString)
            End If

            If Not dicConfig.ContainsKey("TranslatorsNeeded") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "TranslatorsNeeded", objConfig.TranslatorsNeeded.ToString)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "TranslatorsNeeded", objConfig.TranslatorsNeeded.ToString)
            End If

            If Not dicConfig.ContainsKey("FundingCurrency") Then
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Add", ProjectId, "FundingCurrency", objConfig.FundingCurrency.ToString)
            Else
                DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", ProjectId, "FundingCurrency", objConfig.FundingCurrency.ToString)
            End If

        End Sub

        Public Shared Function GetConfig(ProjectId As Integer) As ProjectConfigInfo

            Dim dicConfig As New Dictionary(Of String, String)
            Dim dr As IDataReader = CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_ProjectConfig_ListByProject", ProjectId), IDataReader)
            While dr.Read
                dicConfig.Add(Convert.ToString(dr("Value")), Convert.ToString(dr("Setting")))
            End While
            dr.Close()
            dr.Dispose()


            Dim config As New ProjectConfigInfo

            If dicConfig.ContainsKey("IdeaTitle") Then
                If dicConfig("IdeaTitle") <> "" Then
                    config.IdeaTitle = Convert.ToString(dicConfig("IdeaTitle"))
                    config.InitializedOnly = False
                End If
            End If

            If dicConfig.ContainsKey("IdeaSummary") Then
                If dicConfig("IdeaSummary") <> "" Then
                    config.IdeaSummary = Convert.ToString(dicConfig("IdeaSummary"))
                    config.InitializedOnly = False
                End If
            End If

            If dicConfig.ContainsKey("IdeaDescription") Then
                If dicConfig("IdeaDescription") <> "" Then
                    config.IdeaDescription = Convert.ToString(dicConfig("IdeaDescription"))
                    config.InitializedOnly = False
                End If
            End If

            If dicConfig.ContainsKey("DesignersNeeded") Then
                If dicConfig("DesignersNeeded") <> "0" Then
                    config.DesignersNeeded = Convert.ToInt32(dicConfig("DesignersNeeded"))
                    config.InitializedOnly = False
                End If
            End If


            If dicConfig.ContainsKey("DevelopersNeeded") Then
                If dicConfig("DevelopersNeeded") <> "0" Then
                    config.DevelopersNeeded = Convert.ToInt32(dicConfig("DevelopersNeeded"))
                    config.InitializedOnly = False
                End If
            End If


            If dicConfig.ContainsKey("FundingNeeded") Then
                If dicConfig("FundingNeeded") <> "0" Then
                    config.FundingNeeded = Convert.ToDecimal(dicConfig("FundingNeeded"))
                    config.InitializedOnly = False
                End If
            End If


            If dicConfig.ContainsKey("ManagersNeeded") Then
                If dicConfig("ManagersNeeded") <> "0" Then
                    config.ManagersNeeded = Convert.ToInt32(dicConfig("ManagersNeeded"))
                    config.InitializedOnly = False
                End If
            End If


            If dicConfig.ContainsKey("TestersNeeded") Then
                If dicConfig("TestersNeeded") <> "0" Then
                    config.TestersNeeded = Convert.ToInt32(dicConfig("TestersNeeded"))
                    config.InitializedOnly = False
                End If
            End If


            If dicConfig.ContainsKey("TranslatorsNeeded") Then
                If dicConfig("TranslatorsNeeded") <> "0" Then
                    config.TranslatorsNeeded = Convert.ToInt32(dicConfig("TranslatorsNeeded"))
                    config.InitializedOnly = False
                End If
            End If

            If dicConfig.ContainsKey("FundingCurrency") Then
                If dicConfig("FundingCurrency") <> "0" Then
                    config.FundingCurrency = Convert.ToString(dicConfig("FundingCurrency"))
                    config.InitializedOnly = False
                End If
            End If


            Return config

        End Function

    End Class

End Namespace


