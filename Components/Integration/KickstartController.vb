Imports DotNetNuke.Entities.Modules

Namespace Connect.Modules.Kickstart.Integration
    Public Class KickstartController
        Implements IUpgradeable

        Public Function UpgradeModule(Version As String) As String Implements IUpgradeable.UpgradeModule



            Select Case Version
                Case "01.00.00"

                    NotificationController.AddNotificationTypes()
                    SubscriptionController.AddSubscriptionTypes()

            End Select

            Return ""

        End Function

    End Class
End Namespace

