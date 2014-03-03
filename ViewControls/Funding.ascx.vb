Imports Connect.Modules.Kickstart.Entities

Namespace Connect.Modules.Kickstart
    Public Class Funding
        Inherits KickstartModuleBase


#Region "Event Handlers"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

            ReadQuerystring()

            If Project Is Nothing Then
                HideModule()
            End If

        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            BindLocalization()

            If Not Page.IsPostBack Then
                If Not Project Is Nothing Then

                    BindFunds()

                End If
            End If

        End Sub

#End Region

#Region "Templating"


#End Region

#Region "Private Methods"

        Private Sub BindLocalization()

            lblFundingHead.Text = Localization.GetString("lblFundingHead", LocalResourceFile)


            If IsFunding() Then
                lnkFund.Text = Localization.GetString("lnkManageFunding", LocalResourceFile)
            Else
                lnkFund.Text = Localization.GetString("lnkFund", LocalResourceFile)
            End If

        End Sub

        Private Sub BindFunds()

            If Not Config Is Nothing Then

                Dim CurrentFunding As Decimal = FundingController.GetFunds(ProjectId)
                Dim NeededFunding As Decimal = Config.FundingNeeded

                Dim spanHtmlPercent As String = "5%"

                If NeededFunding > CDec(0.0) Then

                    Select Case CurrentFunding / NeededFunding
                        Case Is >= CDec(1.0)
                            spanHtmlPercent = "100%"
                        Case Is >= CDec(0.9)
                            spanHtmlPercent = "90%"
                        Case Is >= CDec(0.8)
                            spanHtmlPercent = "80%"
                        Case Is >= CDec(0.7)
                            spanHtmlPercent = "70%"
                        Case Is >= CDec(0.6)
                            spanHtmlPercent = "60%"
                        Case Is >= CDec(0.5)
                            spanHtmlPercent = "50%"
                        Case Is >= CDec(0.4)
                            spanHtmlPercent = "40%"
                        Case Is >= CDec(0.3)
                            spanHtmlPercent = "30%"
                        Case Is >= CDec(0.2)
                            spanHtmlPercent = "20%"
                        Case Is >= CDec(0.1)
                            spanHtmlPercent = "10%"
                    End Select

                    lblMeterSpan.Text = "<span style=""width: " & spanHtmlPercent & """></span>"

                    lblFundingNeeded.Text = String.Format(Localization.GetString("lblFundingNeeded", LocalResourceFile), Utilities.FormatCurrency(Config.FundingCurrency), Utilities.FormatAmount(NeededFunding))
                    lblFundingReached.Text = String.Format(Localization.GetString("lblFundingReached", LocalResourceFile), Utilities.FormatCurrency(Config.FundingCurrency), Utilities.FormatAmount(CurrentFunding))

                    Dim FundingUrl As String = NavigateURL(KickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & ProjectId.ToString, "Action=Fund")
                    If Request.IsAuthenticated Then
                        lnkFund.NavigateUrl = FundingUrl
                    Else
                        lnkFund.NavigateUrl = NavigateURL(PortalSettings.LoginTabId, "", "ReturnUrl=" & Server.UrlEncode(FundingUrl))
                    End If

                Else

                    HideModule()

                End If

            Else

                HideModule()

            End If


        End Sub

#End Region

    End Class
End Namespace
