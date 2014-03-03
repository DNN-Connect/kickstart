Imports Connect.Modules.Kickstart.Entities
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Entities.Portals

Namespace Connect.Modules.Kickstart.Templates

    Public Class CommentsTemplateController

        Public Shared Sub ProcessCommonTemplate(ByRef strHtml As String, ByVal strTemplateFile As String, ByVal objCommentList As List(Of CommentInfo), Settings As KickstartSettings, Project As ProjectInfo)

            Dim strTemplate As String = TemplateHelper.ReadTemplate(Settings.ThemePath & strTemplateFile)
            Dim User As UserInfo = UserController.GetCurrentUserInfo

            Dim literal As New Literal
            Dim delimStr As String = "[]"
            Dim delimiter As Char() = delimStr.ToCharArray()

            Dim templateArray As String()
            templateArray = strTemplate.Split(delimiter)

            For iPtr As Integer = 0 To templateArray.Length - 1 Step 2

                strHtml += templateArray(iPtr).ToString()

                If iPtr < templateArray.Length - 1 Then

                    Select Case templateArray(iPtr + 1).ToLower

                        Case "ifcansubscribe"

                            If Settings.CanSubscribe = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcansubscribe") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifissubscribed"

                            If Integration.SubscriptionController.IsSubscribedToProjectComments(User, Project, Project.ModuleId) = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifissubscribed") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifnotsubscribed"

                            If Integration.SubscriptionController.IsSubscribedToProjectComments(User, Project, Project.ModuleId) = True Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifnotsubscribed") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "loginurl"

                            strHtml += NavigateURL(Settings.Portalsettings.LoginTabId, "", "ReturnUrl=" & HttpUtility.UrlEncode(NavigateURL(Settings.ProjectDetailsTabId, "", "ProjectId=" & Project.ProjectId.ToString)))

                        Case "ifcancomment"

                            If Settings.CanComment = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcancomment") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifisauthenticated"

                            If HttpContext.Current.Request.IsAuthenticated = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifisauthenticated") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifisanonymous"

                            If HttpContext.Current.Request.IsAuthenticated = True Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifisanonymous") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

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

        Public Shared Sub ProcessItemTemplate(ByRef strHtml As String, ByVal strTemplateFile As String, ByVal objComment As CommentInfo, Settings As KickstartSettings, User As UserInfo)

            Dim strTemplate As String = TemplateHelper.ReadTemplate(Settings.ThemePath & strTemplateFile)
            Dim usrCreatedBy As UserInfo = Nothing

            Dim blnCanVote As Boolean = False

            If Settings.CanVote Then
                If UserVoteController.ListUserVotes(User.UserID, objComment.ProjectId, objComment.CommentId).Count = 0 Then
                    blnCanVote = True
                End If
            End If



            Dim literal As New Literal
            Dim delimStr As String = "[]"
            Dim delimiter As Char() = delimStr.ToCharArray()

            Dim templateArray As String()
            templateArray = strTemplate.Split(delimiter)

            For iPtr As Integer = 0 To templateArray.Length - 1 Step 2

                strHtml += templateArray(iPtr).ToString()

                If iPtr < templateArray.Length - 1 Then


                    Select Case templateArray(iPtr + 1).ToLower



                        Case "datecreated"

                            Dim strDate As String = String.Format(Utilities.GetSharedResource("DateFormat"), objComment.DateCreated.ToShortDateString)

                            If objComment.DateCreated.Date = Date.Now.Date Then
                                strDate = Utilities.GetSharedResource("Today")
                            End If

                            If objComment.DateCreated.Date.AddDays(1) = Date.Now.Date Then
                                strDate = Utilities.GetSharedResource("Yesterday")
                            End If


                            strHtml += strDate

                        Case "timeago"

                            Dim strTime As String = String.Format(Utilities.GetSharedResource("TimeFormat"), objComment.DateCreated.ToShortTimeString)

                            If objComment.DateCreated.Date = Date.Now.Date Then
                                'today
                                Dim diff As Integer = DateDiff(DateInterval.Minute, objComment.DateCreated, Date.Now)
                                If diff < 60 Then
                                    'within last hour
                                    strTime = String.Format(Utilities.GetSharedResource("MinutesAgo"), diff.ToString)

                                    If diff = 1 Then
                                        strTime = Utilities.GetSharedResource("JustNow")
                                    End If

                                End If
                            End If

                            strHtml += strTime

                        Case "createdbyuserid"

                            If usrCreatedBy Is Nothing Then
                                usrCreatedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objComment.CreatedBy)
                            End If

                            strHtml += usrCreatedBy.UserID.ToString

                        Case "createdbydisplayname"

                            If usrCreatedBy Is Nothing Then
                                usrCreatedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objComment.CreatedBy)
                            End If

                            strHtml += usrCreatedBy.DisplayName

                        Case "createdbyprofileurl"

                            If usrCreatedBy Is Nothing Then
                                usrCreatedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objComment.CreatedBy)
                            End If

                            strHtml += NavigateURL(Settings.Portalsettings.UserTabId, "", "UserId=" & usrCreatedBy.UserID)


                        Case "createdbypictureurl"

                            If usrCreatedBy Is Nothing Then
                                usrCreatedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objComment.CreatedBy)
                            End If

                            strHtml += String.Format("~/profilepic.ashx?userId={0}&h={1}&w={2}", usrCreatedBy.UserID.ToString, "32", "32")

                        Case "content"

                            strHtml += objComment.Content

                        Case "commentid"

                            strHtml += objComment.CommentId.ToString

                        Case "comments"

                            Dim lstChildComments As New List(Of CommentInfo)
                            lstChildComments = CommentController.ListByProject(objComment.ProjectId, objComment.CommentId)
                            For Each objChildComment As CommentInfo In lstChildComments
                                CommentsTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_CommentItem, objChildComment, Settings, User)
                            Next

                        Case "ifcanvote"

                            If blnCanVote = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcanvote") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcannotvote"

                            If blnCanVote Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcannotvote") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcancomment"

                            If Settings.CanComment = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcancomment") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifisauthenticated"

                            If HttpContext.Current.Request.IsAuthenticated = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifisauthenticated") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifisanonymous"

                            If HttpContext.Current.Request.IsAuthenticated = True Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifisanonymous") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcandelete"

                            If Settings.CanDeleteComment = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcandelete") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcanedit"

                            If Settings.CanEditComment = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcanedit") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "votestext"

                            If objComment.Votes = 0 Then
                                strHtml += Utilities.GetSharedResource("CommentNoVotesString")
                            End If
                            If objComment.Votes = 1 Then
                                strHtml += String.Format(Utilities.GetSharedResource("CommentSingleVoteString"), objComment.Votes.ToString)
                            End If
                            If objComment.Votes > 1 Then
                                strHtml += String.Format(Utilities.GetSharedResource("CommentVotesString"), objComment.Votes.ToString)
                            End If

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

