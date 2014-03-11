Imports System
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports DotNetNuke.Web.Api
Imports DotNetNuke.Services.Localization
Imports System.IO
Imports Connect.Modules.Kickstart.Entities
Imports Connect.Modules.Kickstart.Templates
Imports DotNetNuke.Security

Namespace Connect.Modules.Kickstart.Services

    Public Class KickstartController
        Inherits DnnApiController

        Public Class RequestDTO
            Public Property PageNo As Integer
            Public Property RecordsPerPage As Integer
            Public Property SortCol As String
            Public Property IsVisible As Integer = 1
            Public Property IsDeleted As Integer = 0
            Public Property CreatedBy As Integer = Null.NullInteger
            Public Property LeadBy As Integer = Null.NullInteger
            Public Property ParticipantId As Integer = Null.NullInteger
        End Class

        Public Class ProjectDTO
            Public Property ProjectId As Integer
            Public Property IsVisible As Integer
            Public Property IsLocked As Integer
            Public Property IsDeleted As Integer
        End Class

        Public Class CommentDTO
            Public Property ProjectId As Integer
            Public Property CommentId As Integer = Null.NullInteger
            Public Property Comment As String
            Public Property ParentId As Integer = Null.NullInteger
        End Class


#Region "Private Members"

        Private _Project As ProjectInfo = Nothing
        Private _Comment As CommentInfo = Nothing

#End Region

#Region "Service Methods"

        <AllowAnonymous>
        <HttpPost>
        Public Function GetProjectList(postData As RequestDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)

            Dim projects As New List(Of ProjectInfo)
            projects = ProjectController.PublicList(ActiveModule.ModuleID, postData.SortCol, postData.PageNo, postData.RecordsPerPage, postData.IsVisible, postData.IsDeleted, postData.CreatedBy, postData.LeadBy)

            If postData.ParticipantId > 0 Then
                Dim myProjects As New List(Of ProjectInfo)
                For Each p As ProjectInfo In projects
                    If Utilities.IsTeamMember(postData.ParticipantId, p, False) Then
                        myProjects.Add(p)
                    End If
                Next
                projects = myProjects
            End If

            Dim strHtml As String = ""

            ProjectTemplateController.ProcessCommonTemplate(strHtml, TemplateHelper.Template_ProjectHeader, projects, objKickstartSettings)
            For Each objProject As ProjectInfo In projects
                ProjectTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_ProjectItem, objProject, objKickstartSettings, UserInfo)
            Next
            ProjectTemplateController.ProcessCommonTemplate(strHtml, TemplateHelper.Template_ProjectFooter, projects, objKickstartSettings)

            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

        End Function

        <AllowAnonymous>
        <HttpPost>
        Public Function GetProjectPagingControl(postData As RequestDTO) As HttpResponseMessage

            Dim strHtml As String = ""
            ProjectTemplateController.ProcessPagingTemplate(strHtml, postData.RecordsPerPage, ProjectController.PublicProjectCount(ActiveModule.ModuleID))
            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

        End Function

        <AllowAnonymous>
        <HttpPost>
        Public Function GetProject(postData As ProjectDTO) As HttpResponseMessage

            Try

                SetContext(postData)

                If _Project Is Nothing Then
                    Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
                End If

                Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule, _Project)

                Dim strHtml As String = ""
                ProjectTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_ProjectDetails, _Project, objKickstartSettings, UserInfo)
                Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

            Catch ex As Exception

                Return Request.CreateResponse(HttpStatusCode.OK, "")
                LogException(ex)

            End Try


        End Function

        <KickstartAuthorize(SecurityAccessLevel.Authenticated)>
        <HttpPost>
        Public Function LikeProject(postData As ProjectDTO) As HttpResponseMessage

            SetContext(postData)

            If _Project Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
            End If

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule, _Project)

            Dim vote As New UserVoteInfo(UserInfo.UserID, _Project.ProjectId)
            UserVoteController.Add(vote)

            _Project.Votes += 1
            ProjectController.Update(_Project)

            Dim strHtml As String = ""
            ProjectTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_ProjectDetails, _Project, objKickstartSettings, UserInfo)
            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

        End Function

        <KickstartAuthorize(SecurityAccessLevel.Authenticated)>
        <HttpPost>
        Public Function UnLikeProject(postData As ProjectDTO) As HttpResponseMessage

            SetContext(postData)

            If _Project Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
            End If
            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule, _Project)

            Dim vote As New UserVoteInfo(UserInfo.UserID, _Project.ProjectId)
            UserVoteController.Delete(vote)

            _Project.Votes -= 1
            If _Project.Votes < 0 Then
                _Project.Votes = 0
            End If
            ProjectController.Update(_Project)

            Dim strHtml As String = ""
            ProjectTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_ProjectDetails, _Project, objKickstartSettings, UserInfo)
            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

        End Function

        <KickstartAuthorize(SecurityAccessLevel.AddComment)>
        <HttpPost>
        Public Function AddComment(postData As CommentDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Project Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
            End If

            Dim objComment As New CommentInfo
            objComment.Content = postData.Comment
            objComment.ContentItemId = 0
            objComment.CreatedBy = UserInfo.UserID
            objComment.DateCreated = Date.Now
            objComment.IsTeamResponse = Connect.Modules.Kickstart.Utilities.IsTeamMember(UserInfo.UserID, _Project, False)
            objComment.IsVisible = True
            objComment.ParentId = postData.ParentId
            objComment.ProjectId = postData.ProjectId
            objComment.Votes = 0

            objComment.CommentId = CommentController.Add(objComment)

            Dim ProjectUrl As String = NavigateURL(objKickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & _Project.ProjectId.ToString)
            Integration.NotificationController.SendNewCommentNotification(_Project, objComment, UserInfo, ProjectUrl)
            Integration.SubscriptionController.NotifySubscribersAboutNewComment(objKickstartSettings.Portalsettings.PortalId, _Project, objComment, ProjectUrl)

            Dim strHtml As String = ""
            CommentsTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_CommentItem, objComment, objKickstartSettings, UserInfo)
            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

        End Function

        <KickstartAuthorize(SecurityAccessLevel.EditComment)>
        <HttpPost>
        Public Function EditComment(postData As CommentDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Comment Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_CommentNotFound"))
            End If

            _Comment.Content = postData.Comment
            CommentController.Update(_Comment)

            Dim strHtml As String = ""
            CommentsTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_CommentItem, _Comment, objKickstartSettings, UserInfo)
            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

        End Function

        <KickstartAuthorize(SecurityAccessLevel.DeleteComment)>
        <HttpPost>
        Public Function DeleteComment(postData As CommentDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Comment Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_CommentNotFound"))
            End If

            Dim vote As New UserVoteInfo(UserInfo.UserID, _Project.ProjectId, _Comment.CommentId)
            UserVoteController.Delete(vote)

            CommentController.Delete(postData.CommentId)

            Return Request.CreateResponse(HttpStatusCode.OK, "<div />")

        End Function

        <KickstartAuthorize(SecurityAccessLevel.Authenticated)>
        <HttpPost>
        Public Function LikeComment(postData As CommentDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Comment Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_CommentNotFound"))
            End If

            Dim vote As New UserVoteInfo(UserInfo.UserID, _Project.ProjectId, _Comment.CommentId)
            UserVoteController.Add(vote)

            _Comment.Votes += 1
            CommentController.Update(_Comment)

            Dim strHtml As String = ""
            CommentsTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_CommentItem, _Comment, objKickstartSettings, UserInfo)
            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

        End Function

        <KickstartAuthorize(SecurityAccessLevel.Authenticated)>
        <HttpPost>
        Public Function UnLikeComment(postData As CommentDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Comment Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_CommentNotFound"))
            End If

            Dim vote As New UserVoteInfo(UserInfo.UserID, _Project.ProjectId, _Comment.CommentId)
            UserVoteController.Delete(vote)

            _Comment.Votes -= 1
            If _Comment.Votes < 0 Then
                _Comment.Votes = 0
            End If
            CommentController.Update(_Comment)

            Dim strHtml As String = ""
            CommentsTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_CommentItem, _Comment, objKickstartSettings, UserInfo)
            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

        End Function

        <AllowAnonymous>
        <HttpPost>
        Public Function GetProjectStatus(postData As ProjectDTO) As HttpResponseMessage

            SetContext(postData)

            If _Project Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
            End If
            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule, _Project)

            Dim strHtml As String = ""
            ProjectTemplateController.ProcessItemTemplate(strHtml, TemplateHelper.Template_ProjectStatus, _Project, objKickstartSettings, UserInfo)
            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)

        End Function

        <KickstartAuthorizeAttribute(SecurityAccessLevel.EditModule)>
        <HttpPost>
        Public Function SetProjectVisibilityStatus(postData As ProjectDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Project Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
            End If

            Dim strResponseData As String = ""

            If postData.IsVisible = 1 Then
                _Project.IsVisible = True
                strResponseData = "isvisible"

            Else
                _Project.IsVisible = False
                strResponseData = "ishidden"
            End If

            ProjectController.Update(_Project)

            Dim ProjectUrl As String = NavigateURL(objKickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & _Project.ProjectId.ToString)
            Integration.NotificationController.SendVisibilityChangedNotification(_Project, UserInfo, ProjectUrl)
            Integration.SubscriptionController.NotifySubscribersAboutNewProject(objKickstartSettings.Portalsettings.PortalId, _Project, ProjectUrl)

            Return Request.CreateResponse(HttpStatusCode.OK, strResponseData)

        End Function

        <KickstartAuthorizeAttribute(SecurityAccessLevel.EditModule)>
        <HttpPost>
        Public Function SetProjectLockStatus(postData As ProjectDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Project Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
            End If

            Dim strResponseData As String = ""

            If postData.IsLocked = 1 Then
                _Project.IsLocked = True
                _Project.LockedBy = UserInfo.UserID
                _Project.DateLocked = Date.Now
                strResponseData = "islocked"
            Else
                _Project.IsLocked = False
                _Project.LockedBy = Null.NullInteger
                _Project.DateLocked = Null.NullDate
                strResponseData = "isunlocked"
            End If
            ProjectController.Update(_Project)

            Return Request.CreateResponse(HttpStatusCode.OK, strResponseData)

        End Function

        <KickstartAuthorizeAttribute(SecurityAccessLevel.EditModule)>
        <HttpPost>
        Public Function SetProjectDeletedStatus(postData As ProjectDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Project Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
            End If

            Dim strResponseData As String = ""

            If postData.IsDeleted = 1 Then
                _Project.IsDeleted = True
                _Project.DeletedBy = UserInfo.UserID
                _Project.DateDeleted = Date.Now
                strResponseData = "isdeleted"
            Else
                _Project.IsDeleted = False
                _Project.DeletedBy = Null.NullInteger
                _Project.DateDeleted = Null.NullDate
                strResponseData = "isundeleted"
            End If
            ProjectController.Update(_Project)

            Return Request.CreateResponse(HttpStatusCode.OK, strResponseData)

        End Function

        <KickstartAuthorizeAttribute(SecurityAccessLevel.Authenticated)>
        <HttpPost>
        Public Function ChangeReleaseSubscriptionStatus(postData As ProjectDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Project Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
            End If

            Dim strResponseData As String = ""

            If Integration.SubscriptionController.IsSubscribedToProjectReleases(UserInfo, _Project, _Project.ModuleId) Then
                Integration.SubscriptionController.UnSubscribeFromProjectReleases(UserInfo, _Project, _Project.ModuleId)
                strResponseData = Utilities.GetSharedJSSafeResource("NowUnsubscribedFromReleases")
            Else
                Integration.SubscriptionController.SubscribeToProjectReleases(UserInfo, _Project, _Project.ModuleId)
                strResponseData = Utilities.GetSharedJSSafeResource("NowSubscribedToReleases")
            End If

            Return Request.CreateResponse(HttpStatusCode.OK, strResponseData)

        End Function

        <KickstartAuthorizeAttribute(SecurityAccessLevel.Authenticated)>
        <HttpPost>
        Public Function ChangeCommentSubscriptionStatus(postData As ProjectDTO) As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)
            SetContext(postData)

            If _Project Is Nothing Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest, Utilities.GetSharedJSSafeResource("Error_ProjectNotFound"))
            End If

            Dim strResponseData As String = ""

            If Integration.SubscriptionController.IsSubscribedToProjectComments(UserInfo, _Project, _Project.ModuleId) Then
                Integration.SubscriptionController.UnSubscribeFromProjectComments(UserInfo, _Project, _Project.ModuleId)
                strResponseData = Utilities.GetSharedJSSafeResource("NowUnsubscribedFromComments")
            Else
                Integration.SubscriptionController.SubscribeToProjectComments(UserInfo, _Project, _Project.ModuleId)
                strResponseData = Utilities.GetSharedJSSafeResource("NowSubscribedToComments")
            End If

            Return Request.CreateResponse(HttpStatusCode.OK, strResponseData)

        End Function

        <KickstartAuthorizeAttribute(SecurityAccessLevel.Authenticated)>
        <HttpPost>
        Public Function ChangeProjectListSubscriptionStatus() As HttpResponseMessage

            Dim objKickstartSettings As KickstartSettings = New KickstartSettings(ActiveModule)

            Dim strResponseData As String = ""

            If Integration.SubscriptionController.IsSubscribedToProjectList(UserInfo, ActiveModule.ModuleID) Then
                Integration.SubscriptionController.UnSubscribeFromProjectList(UserInfo, ActiveModule.ModuleID)
                strResponseData = Utilities.GetSharedJSSafeResource("NowUnsubscribedFromProjectList")
            Else
                Integration.SubscriptionController.SubscribeToProjectList(UserInfo, ActiveModule.ModuleID)
                strResponseData = Utilities.GetSharedJSSafeResource("NowSubscribedToProjectList")
            End If

            Return Request.CreateResponse(HttpStatusCode.OK, strResponseData)

        End Function

#End Region

#Region "Private Methods"

        Private Sub SetContext(data As ProjectDTO)
            _Project = ProjectController.Get(data.ProjectId)
        End Sub

        Private Sub SetContext(data As CommentDTO)
            _Project = ProjectController.Get(data.ProjectId)
            If data.CommentId <> Null.NullInteger Then
                _Comment = CommentController.Get(data.CommentId)
            End If
        End Sub

#End Region

    End Class

End Namespace

