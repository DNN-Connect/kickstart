Imports Connect.Modules.Kickstart.Entities
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Entities.Portals

Namespace Connect.Modules.Kickstart.Templates

    Public Class ProjectTemplateController

        Public Shared Sub ProcessCommonTemplate(ByRef strHtml As String, ByVal strTemplateFile As String, ByVal objProjectList As List(Of ProjectInfo), Settings As KickstartSettings)

            Dim strTemplate As String = TemplateHelper.ReadTemplate(Settings.ThemePath & strTemplateFile)

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

                            If Integration.SubscriptionController.IsSubscribedToProjectList(UserController.GetCurrentUserInfo, Settings.ModuleConfiguration.ModuleID) = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifissubscribed") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifnotsubscribed"

                            If Integration.SubscriptionController.IsSubscribedToProjectList(UserController.GetCurrentUserInfo, Settings.ModuleConfiguration.ModuleID) = True Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifnotsubscribed") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcansubmit"

                            If Settings.CanAddProject = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcansubmit") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "createideaurl"

                            strHtml += NavigateURL(Portalsettings.ActiveTab.TabID, "", "Action=Create")

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

        Public Shared Sub ProcessPagingTemplate(ByRef strHtml As String, PageSize As Integer, TotalCount As Integer)

            Dim pages As Integer = TotalCount / PageSize
            strHtml += "<ul>"
            For i As Integer = 1 To pages
                strHtml += "<li><a href=""#"" rel=""" & i.ToString & """ class=""pagelink"">" & i.ToString & "</a></li>"
            Next
            strHtml += "</ul>"

        End Sub

        Public Shared Sub ProcessItemTemplate(ByRef strHtml As String, ByVal strTemplateFile As String, ByVal objProject As ProjectInfo, ByVal Settings As KickstartSettings, User As UserInfo)

            Dim strTemplate As String = TemplateHelper.ReadTemplate(Settings.ThemePath & strTemplateFile)
            Dim usrCreatedBy As UserInfo = Nothing
            Dim usrLockedBy As UserInfo = Nothing
            Dim usrDeletedBy As UserInfo = Nothing
            Dim usrLead As UserInfo = Nothing

            Dim blnCanVote As Boolean = False

            If Settings.CanVote Then
                If UserVoteController.ListUserVotes(User.UserID, objProject.ProjectId, Null.NullInteger).Count = 0 Then
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

                        Case "ifcanparticipate"

                            If Settings.CanParticipate = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcanparticipate") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifisparticipating"

                            If Utilities.IsTeamMember(User.UserID, objProject) = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifisparticipating") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifnotparticipating"

                            If Utilities.IsTeamMember(User.UserID, objProject) = True Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifnotparticipating") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "manageparticipationurl"

                            If objProject.LeadBy = Null.NullInteger Then
                                strHtml += NavigateURL(Settings.Portalsettings.ActiveTab.TabID, "", "ProjectId=" & objProject.ProjectId.ToString, "Action=BecomeLead")
                            Else
                                strHtml += NavigateURL(Settings.Portalsettings.ActiveTab.TabID, "", "ProjectId=" & objProject.ProjectId.ToString, "Action=Participate")
                            End If



                        Case "iseditor"

                            If Not Utilities.CanEdit(objProject) Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/iseditor") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcanapprove"

                            If Settings.CanApproveProject = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcanapprove") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcanedit"

                            If Settings.CanEditProject = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcanedit") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcansubscribe"

                            If Settings.CanSubscribe = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcansubscribe") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcanlock"

                            If Settings.CanLockProject = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcanlock") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcandelete"

                            If Settings.CanLockProject = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcandelete") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifcandothings"

                            If Settings.CanLockProject = False Or Settings.CanApproveProject = False Or Settings.CanDeleteProject = False Or Settings.CanEditProject = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifcandothings") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If


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

                        Case "ifisauthenticated"

                            If HttpContext.Current.Request.IsAuthenticated = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifisauthenticated") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "statuscontainercss"

                            strHtml += "kickstart-project-status"

                        Case "status"

                            ProcessItemTemplate(strHtml, TemplateHelper.Template_ProjectStatus, objProject, Settings, User)

                        Case "moduleid"

                            strHtml += objProject.ModuleId.ToString

                        Case "contentitemid"

                            strHtml += objProject.ContentItemId

                        Case "statusid"

                            strHtml += objProject.Status.ToString

                        Case "status"

                            strHtml += Utilities.GetSharedResource(CType(objProject.Status, Enums.ProjectStatus).ToString)

                        Case "subject"

                            strHtml += objProject.Subject

                        Case "summary"

                            strHtml += HttpUtility.HtmlDecode(objProject.Summary)

                        Case "content"

                            strHtml += HttpUtility.HtmlDecode(objProject.Content)

                        Case "projecturl"

                            strHtml += objProject.ProjectUrl

                        Case "hasprojecturl"

                            If String.IsNullOrEmpty(objProject.ProjectUrl) Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/hasprojecturl") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "projectplatform"

                            strHtml += objProject.ProjectPlatform

                        Case "hasprojectplatform"

                            If String.IsNullOrEmpty(objProject.ProjectPlatform) Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/hasprojectplatform") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "hasnoprojectplatform"

                            If String.IsNullOrEmpty(objProject.ProjectPlatform) = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/hasnoprojectplatform") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "platformrssurl"

                            strHtml += objProject.PlatformRssUrl

                        Case "hasplatformrssurl"

                            If String.IsNullOrEmpty(objProject.PlatformRssUrl) Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/hasplatformrssurl") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "hasexternals"

                            If String.IsNullOrEmpty(objProject.PlatformRssUrl) And String.IsNullOrEmpty(objProject.ProjectPlatform) And String.IsNullOrEmpty(objProject.ProjectUrl) Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/hasexternals") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifissubscribed"

                            If Integration.SubscriptionController.IsSubscribedToProjectReleases(User, objProject, objProject.ModuleId) = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifissubscribed") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifnotsubscribed"

                            If Integration.SubscriptionController.IsSubscribedToProjectReleases(User, objProject, objProject.ModuleId) = True Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifnotsubscribed") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifissubscribed"

                            If Integration.SubscriptionController.IsSubscribedToProjectComments(User, objProject, objProject.ModuleId) = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifissubscribed") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifnotsubscribed"

                            If Integration.SubscriptionController.IsSubscribedToProjectComments(User, objProject, objProject.ModuleId) = True Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifnotsubscribed") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "datescheduled"

                            strHtml += objProject.DateScheduled.ToShortDateString

                        Case "datelocked"

                            strHtml += objProject.DateLocked.ToShortDateString

                        Case "datedelivered"

                            strHtml += objProject.DateDelivered.ToShortDateString

                        Case "datedeleted"

                            strHtml += objProject.DateDeleted.ToShortDateString

                        Case "datecreated"

                            strHtml += objProject.DateCreated.ToShortDateString

                        Case "datescheduled"

                            strHtml += objProject.DateScheduled.ToShortDateString

                        Case "createdbyuserid"

                            If usrCreatedBy Is Nothing Then
                                usrCreatedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.CreatedBy)
                            End If

                            strHtml += usrCreatedBy.UserID.ToString

                        Case "createdbydisplayname"

                            If usrCreatedBy Is Nothing Then
                                usrCreatedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.CreatedBy)
                            End If

                            strHtml += usrCreatedBy.DisplayName

                        Case "createdbyprofileurl"

                            If usrCreatedBy Is Nothing Then
                                usrCreatedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.CreatedBy)
                            End If

                            strHtml += NavigateURL(Settings.Portalsettings.UserTabId, "", "UserId=" & usrCreatedBy.UserID)

                        Case "createdbypictureurl"

                            If usrCreatedBy Is Nothing Then
                                usrCreatedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.CreatedBy)
                            End If

                            strHtml += String.Format("~/profilepic.ashx?userId={0}&h={1}&w={2}", usrCreatedBy.UserID.ToString, "32", "32")

                        Case "lockedbyuserid"

                            If usrLockedBy Is Nothing Then
                                usrLockedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.LockedBy)
                            End If

                            strHtml += usrLockedBy.UserID.ToString

                        Case "lockedbydisplayname"

                            If usrLockedBy Is Nothing Then
                                usrLockedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.LockedBy)
                            End If

                            strHtml += usrLockedBy.DisplayName

                        Case "visibilityclass"

                            If objProject.IsVisible Then
                                strHtml += "isvisible"
                            Else
                                strHtml += "ishidden"
                            End If

                        Case "lockingclass"

                            If objProject.IsLocked Then
                                strHtml += "islocked"
                            Else
                                strHtml += "isunlocked"
                            End If


                        Case "deletingclass"

                            If objProject.IsDeleted Then
                                strHtml += "isdeleted"
                            Else
                                strHtml += "isundeleted"
                            End If

                        Case "lockedbyprofileurl"

                            If usrLockedBy Is Nothing Then
                                usrLockedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.LockedBy)
                            End If

                            strHtml += NavigateURL(Settings.Portalsettings.UserTabId, "", "UserId=" & usrLockedBy.UserID)

                        Case "lockedbypictureurl"

                            If usrLockedBy Is Nothing Then
                                usrLockedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.LockedBy)
                            End If

                            strHtml += String.Format("~/profilepic.ashx?userId={0}&h={1}&w={2}", usrLockedBy.UserID.ToString, "32", "32")

                        Case "deletedbyuserid"

                            If usrDeletedBy Is Nothing Then
                                usrDeletedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.DeletedBy)
                            End If

                            strHtml += usrDeletedBy.UserID.ToString

                        Case "deletedbydisplayname"

                            If usrDeletedBy Is Nothing Then
                                usrDeletedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.DeletedBy)
                            End If

                            strHtml += usrDeletedBy.DisplayName

                        Case "deletedbyprofileurl"

                            If usrDeletedBy Is Nothing Then
                                usrDeletedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.DeletedBy)
                            End If

                            strHtml += NavigateURL(Settings.Portalsettings.UserTabId, "", "UserId=" & usrDeletedBy.UserID)

                        Case "deletedbypictureurl"

                            If usrDeletedBy Is Nothing Then
                                usrDeletedBy = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.DeletedBy)
                            End If

                            strHtml += String.Format("~/profilepic.ashx?userId={0}&h={1}&w={2}", usrDeletedBy.UserID.ToString, "32", "32")

                        Case "leadbyuserid"

                            If usrLead Is Nothing Then
                                usrLead = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.LeadBy)
                            End If

                            strHtml += usrLead.UserID.ToString

                        Case "leadbydisplayname"

                            If usrLead Is Nothing Then
                                usrLead = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.LeadBy)
                            End If

                            strHtml += usrLead.DisplayName

                        Case "leadbyprofileurl"

                            If usrLead Is Nothing Then
                                usrLead = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.LeadBy)
                            End If

                            strHtml += NavigateURL(Settings.Portalsettings.UserTabId, "", "UserId=" & usrLead.UserID)

                        Case "leadbypictureurl"

                            If usrLead Is Nothing Then
                                usrLead = UserController.GetUserById(Settings.Portalsettings.PortalId, objProject.LeadBy)
                            End If

                            strHtml += String.Format("~/profilepic.ashx?userId={0}&h={1}&w={2}", usrLead.UserID.ToString, "32", "32")

                        Case "becomealeadurl"

                            strHtml += NavigateURL(Settings.Portalsettings.ActiveTab.TabID, "", "ProjectId=" & objProject.ProjectId.ToString, "Action=BecomeLead")

                        Case "isvisible"

                            Dim strKey As String = ""
                            If objProject.IsVisible Then
                                strKey = "Published"
                            Else
                                strKey = "UnPublished"
                            End If

                            strHtml += Utilities.GetSharedResource(strKey)

                        Case "islocked"

                            Dim strKey As String = ""
                            If objProject.IsLocked Then
                                strKey = "Locked"
                            Else
                                strKey = "UnLocked"
                            End If

                            strHtml += Utilities.GetSharedResource(strKey)

                        Case "isdeleted"

                            Dim strKey As String = ""
                            If objProject.IsDeleted Then
                                strKey = "Deleted"
                            Else
                                strKey = "NotDeleted"
                            End If

                            strHtml += Utilities.GetSharedResource(strKey)

                        Case "isdelivered"

                            Dim strKey As String = ""
                            If objProject.IsDeleted Then
                                strKey = "Delivered"
                            Else
                                strKey = "NotDelivered"
                            End If

                            strHtml += Utilities.GetSharedResource(strKey)

                        Case "viewscount"

                            strHtml += objProject.Views.ToString

                        Case "commentscount"

                            strHtml += objProject.Comments.ToString

                        Case "votescount"

                            strHtml += objProject.Votes.ToString

                        Case "teammemberscount"

                            strHtml += objProject.TeamMembers.ToString

                        Case "haslead"

                            If objProject.LeadBy = Null.NullInteger Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/haslead") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "hasnolead"

                            If objProject.LeadBy <> Null.NullInteger Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/hasnolead") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifislocked"

                            If objProject.IsLocked = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifislocked") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifisdeleted"

                            If objProject.IsDeleted = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifisdeleted") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifisvisible"

                            If objProject.IsVisible = False Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifisvisible") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "ifishidden"

                            If objProject.IsVisible = True Then
                                While (iPtr < templateArray.Length - 1)
                                    If (templateArray(iPtr + 1).ToLower = "/ifishidden") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If


                        Case "detailurl"

                            strHtml += NavigateURL(Settings.ProjectDetailsTabId, "", "ProjectId=" & objProject.ProjectId.ToString)

                        Case "comments"

                            Dim lstComments As List(Of CommentInfo) = CommentController.ListByProject(objProject.ProjectId, Null.NullInteger)
                            CommentsTemplateController.ProcessCommonTemplate(strHtml, TemplateHelper.Template_CommentHeader, lstComments, Settings, objProject)
                            For Each objComment As CommentInfo In lstComments
                                CommentsTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_CommentItem, objComment, Settings, User)
                            Next
                            CommentsTemplateController.ProcessCommonTemplate(strHtml, TemplateHelper.Template_CommentFooter, lstComments, Settings, objProject)

                        Case "participants"

                            Dim lstParticipants As List(Of ParticipantInfo) = ParticipantController.ListByProject(objProject.ProjectId)
                            ParticipantsTemplateController.ProcessCommonTemplate(strHtml, TemplateHelper.Template_ParticipantsHeader, lstParticipants, Settings)
                            For Each objParticipant As ParticipantInfo In lstParticipants
                                ParticipantsTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_ParticipantsItem, objParticipant, Settings)
                            Next
                            ParticipantsTemplateController.ProcessCommonTemplate(strHtml, TemplateHelper.Template_ParticipantsFooter, lstParticipants, Settings)

                        Case "approveprojecturl"

                            If objProject.IsVisible Then
                                strHtml += "javascript:UnApproveProject(" & objProject.ModuleId & "," & objProject.ProjectId & ");"
                            Else
                                strHtml += "javascript:ApproveProject(" & objProject.ModuleId & "," & objProject.ProjectId & ");"
                            End If


                        Case "editprojecturl"

                            If objProject.LeadBy = Null.NullInteger AndAlso (objProject.CreatedBy = User.UserID Or Settings.CanApproveProject) Then
                                strHtml += NavigateURL(Settings.Portalsettings.ActiveTab.TabID, "", "Action=EditIdea", "ProjectId=" & objProject.ProjectId.ToString)
                            End If

                            If objProject.LeadBy <> Null.NullInteger Then
                                strHtml += NavigateURL(Settings.Portalsettings.ActiveTab.TabID, "", "Action=EditProject", "ProjectId=" & objProject.ProjectId.ToString)
                            End If

                        Case "lockprojecturl"

                            If objProject.IsLocked Then
                                strHtml += "javascript:UnLockProject(" & objProject.ModuleId & "," & objProject.ProjectId & ");"
                            Else
                                strHtml += "javascript:LockProject(" & objProject.ModuleId & "," & objProject.ProjectId & ");"
                            End If

                        Case "deleteprojecturl"

                            strHtml += "javascript:DeleteProject(" & objProject.ModuleId & "," & objProject.ProjectId & ");"

                        Case Else

                            If (templateArray(iPtr + 1).ToUpper().StartsWith("RESX:")) Then

                                Dim key As String = templateArray(iPtr + 1).Substring(5, templateArray(iPtr + 1).Length - 5)

                                Select Case key
                                    Case "cmdLockProject"
                                        If objProject.IsLocked Then
                                            strHtml += Utilities.GetSharedResource("cmdUnLockProject")
                                        Else
                                            strHtml += Utilities.GetSharedResource(key)
                                        End If

                                    Case "cmdApproveProject"
                                        If objProject.IsVisible Then
                                            strHtml += Utilities.GetSharedResource("cmdUnApproveProject")
                                        Else
                                            strHtml += Utilities.GetSharedResource("cmdApproveProject")
                                        End If

                                    Case "cmdDeleteProject"
                                        If objProject.IsDeleted Then
                                            strHtml += Utilities.GetSharedResource("cmdUnDeleteProject")
                                        Else
                                            strHtml += Utilities.GetSharedResource("cmdDeleteProject")
                                        End If

                                    Case Else

                                        strHtml += Utilities.GetSharedResource(key)

                                End Select

                            End If

                    End Select
                End If
            Next

        End Sub

    End Class

End Namespace

