Imports Connect.Modules.Kickstart.Entities
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Entities.Portals

Namespace Connect.Modules.Kickstart.Templates

    Public Class ParticipantsTemplateController

        Public Shared Sub ProcessCommonTemplate(ByRef strHtml As String, ByVal strTemplateFile As String, ByVal objParticipantList As List(Of ParticipantInfo), Settings As KickstartSettings)

            Dim strTemplate As String = TemplateHelper.ReadTemplate(Settings.ThemePath & strTemplateFile)

            Dim literal As New Literal
            Dim delimStr As String = "[]"
            Dim delimiter As Char() = delimStr.ToCharArray()

            Dim templateArray As String()
            templateArray = strTemplate.Split(delimiter)

            For iPtr As Integer = 0 To templateArray.Length - 1 Step 2

                strHtml += templateArray(iPtr).ToString()

                If iPtr < templateArray.Length - 1 Then

                    Select Case templateArray(iPtr + 1)

                        Case ""




                        Case Else

                            If (templateArray(iPtr + 1).ToUpper().StartsWith("RESX:")) Then

                                Dim key As String = templateArray(iPtr + 1).Substring(5, templateArray(iPtr + 1).Length - 5)
                                Dim strText As String = Utilities.GetSharedResource(key)
                                strHtml += strText

                            End If

                    End Select
                End If
            Next

        End Sub

        Public Shared Sub ProcessItemTemplate(ByRef strHtml As String, ByVal strTemplateFile As String, ByVal objParticipant As ParticipantInfo, Settings As KickstartSettings)

            Dim strTemplate As String = TemplateHelper.ReadTemplate(Settings.ThemePath & strTemplateFile)


            Dim usrParticipant As UserInfo = Nothing
            Dim Portalsettings As PortalSettings = PortalController.GetCurrentPortalSettings

            Dim literal As New Literal
            Dim delimStr As String = "[]"
            Dim delimiter As Char() = delimStr.ToCharArray()

            Dim templateArray As String()
            templateArray = strTemplate.Split(delimiter)

            For iPtr As Integer = 0 To templateArray.Length - 1 Step 2

                strHtml += templateArray(iPtr).ToString()

                If iPtr < templateArray.Length - 1 Then

                    Select Case templateArray(iPtr + 1).ToLower

                        Case "participantpictureurl"

                            If usrParticipant Is Nothing Then
                                usrParticipant = UserController.GetUserById(Portalsettings.PortalId, objParticipant.UserId)
                            End If

                            strHtml += String.Format("~/profilepic.ashx?userId={0}&h={1}&w={2}", usrParticipant.UserID.ToString, "32", "32")

                        Case "participantprofileurl"

                            If usrParticipant Is Nothing Then
                                usrParticipant = UserController.GetUserById(Portalsettings.PortalId, objParticipant.UserId)
                            End If
                            strHtml += NavigateURL(Portalsettings.UserTabId, "", "UserId=" & usrParticipant.UserID)

                        Case "participantdisplayname"

                            If usrParticipant Is Nothing Then
                                usrParticipant = UserController.GetUserById(Portalsettings.PortalId, objParticipant.UserId)
                            End If
                            strHtml += usrParticipant.DisplayName

                        Case "participantroles"

                            strHtml += objParticipant.ProjectRoles

                        Case "participantuserid"

                            If usrParticipant Is Nothing Then
                                usrParticipant = UserController.GetUserById(Portalsettings.PortalId, objParticipant.UserId)
                            End If

                            strHtml += usrParticipant.UserID.ToString

                        Case Else

                            If (templateArray(iPtr + 1).ToUpper().StartsWith("RESX:")) Then

                                Dim key As String = templateArray(iPtr + 1).Substring(5, templateArray(iPtr + 1).Length - 5)
                                Dim strText As String = Utilities.GetSharedResource(key)
                                strHtml += strText

                            End If

                    End Select
                End If
            Next

        End Sub

    End Class

End Namespace

