Imports Connect.Modules.Kickstart.Entities

Namespace Connect.Modules.Kickstart
    Public Class CacheUtilities

        Public Shared Function GetConfig(ProjectId As Integer) As ProjectConfigInfo

            If Not DataCache.GetCache("KICKSTART_CONFIG_PROJECT_" & ProjectId.ToString) Is Nothing Then
                Return CType(DataCache.GetCache("KICKSTART_CONFIG_PROJECT_" & ProjectId.ToString), ProjectConfigInfo)
            End If

            Return Nothing

        End Function

        Public Shared Sub AddConfig(config As ProjectConfigInfo, ProjectId As Integer)

            If Not DataCache.GetCache("KICKSTART_CONFIG_PROJECT_" & ProjectId.ToString) Is Nothing Then
                DataCache.RemoveCache("KICKSTART_CONFIG_PROJECT_" & ProjectId.ToString)
            End If

            DataCache.SetCache("KICKSTART_CONFIG_PROJECT_" & ProjectId.ToString, config)

        End Sub

        Public Shared Sub ClearConfig(ProjectId As Integer)

            If Not DataCache.GetCache("KICKSTART_CONFIG_PROJECT_" & ProjectId.ToString) Is Nothing Then
                DataCache.RemoveCache("KICKSTART_CONFIG_PROJECT_" & ProjectId.ToString)
            End If

        End Sub

    End Class
End Namespace

