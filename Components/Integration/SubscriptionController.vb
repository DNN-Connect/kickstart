Imports DotNetNuke.Services.Social.Subscriptions
Imports DotNetNuke.Services.Social.Subscriptions.Entities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Users
Imports Connect.Modules.Kickstart.Entities

Namespace Connect.Modules.Kickstart.Integration

    Public Class SubscriptionController

        Public Shared Sub SubscribeToProjectList(objUser As UserInfo, ModuleId As Integer)

            Dim objSubscription As Subscription = CreateProjectListSubscription(ModuleId, objUser.UserID, objUser.PortalID)
            DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.AddSubscription(objSubscription)

        End Sub

        Public Shared Sub UnSubscribeFromProjectList(objUser As UserInfo, ModuleId As Integer)

            Dim objSubscription As Subscription = CreateProjectListSubscription(ModuleId, objUser.UserID, objUser.PortalID)
            DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.DeleteSubscription(objSubscription)

        End Sub

        Public Shared Function IsSubscribedToProjectList(objUser As UserInfo, ModuleId As Integer) As Boolean

            Dim objSubscription As Subscription = CreateProjectListSubscription(ModuleId, objUser.UserID, objUser.PortalID)
            Return DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.IsSubscribed(objSubscription)

        End Function

        Public Shared Sub NotifySubscribersAboutNewProject(PortalId As Integer, objProject As ProjectInfo, url As String)

            Dim deskModuleId As Integer = DesktopModuleController.GetDesktopModuleByFriendlyName("Kickstart").DesktopModuleID
            Dim objType As SubscriptionType = SubscriptionTypeController.Instance.GetSubscriptionType(Function(st) (st.DesktopModuleId = deskModuleId AndAlso st.SubscriptionName = Integration.Subscription_NewProjectTypeName))
            Dim currentSubscriptions As IEnumerable(Of Subscription) = DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.GetContentSubscriptions(PortalId, objType.SubscriptionTypeId, "ModuleId:" & objProject.ModuleId.ToString)

            If currentSubscriptions.Count > 0 Then

                Dim recipients As New List(Of UserInfo)
                For Each objSubscription As Subscription In currentSubscriptions
                    Dim objUser As UserInfo = UserController.GetUserById(PortalId, objSubscription.UserId)
                    recipients.Add(objUser)
                Next

                Dim objMessage As New DotNetNuke.Services.Social.Messaging.Message
                objMessage.Body = String.Format("a new project idea has been posted: {0}. Please check it out: <a href={1}>Link to Project</a>", objProject.Subject, url)
                objMessage.SenderUserID = objProject.CreatedBy
                objMessage.Subject = "New Project Comment"
                objMessage.ReplyAllAllowed = False
                objMessage.PortalID = PortalId

                DotNetNuke.Services.Social.Messaging.MessagingController.Instance.SendMessage(objMessage, Nothing, recipients, Nothing)

            End If            

        End Sub

        Public Shared Sub SubscribeToProjectComments(objUser As UserInfo, Project As ProjectInfo, ModuleId As Integer)

            Dim objSubscription As Subscription = CreateProjectCommentSubscription(Project, ModuleId, objUser.UserID, objUser.PortalID)
            DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.AddSubscription(objSubscription)

        End Sub

        Public Shared Sub UnSubscribeFromProjectComments(objUser As UserInfo, Project As ProjectInfo, ModuleId As Integer)

            Dim objSubscription As Subscription = CreateProjectCommentSubscription(Project, ModuleId, objUser.UserID, objUser.PortalID)
            DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.DeleteSubscription(objSubscription)

        End Sub

        Public Shared Function IsSubscribedToProjectComments(objUser As UserInfo, Project As ProjectInfo, ModuleId As Integer) As Boolean

            Dim objSubscription As Subscription = CreateProjectCommentSubscription(Project, ModuleId, objUser.UserID, objUser.PortalID)
            Return DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.IsSubscribed(objSubscription)

        End Function

        Public Shared Sub NotifySubscribersAboutNewComment(PortalId As Integer, objProject As ProjectInfo, objComment As CommentInfo, url As String)

            Dim deskModuleId As Integer = DesktopModuleController.GetDesktopModuleByFriendlyName("Kickstart").DesktopModuleID
            Dim objType As SubscriptionType = SubscriptionTypeController.Instance.GetSubscriptionType(Function(st) (st.DesktopModuleId = deskModuleId AndAlso st.SubscriptionName = Integration.Subscription_NewCommentTypeName))
            Dim currentSubscriptions As IEnumerable(Of Subscription) = DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.GetContentSubscriptions(PortalId, objType.SubscriptionTypeId, "ProjectId:" & objProject.ProjectId.ToString & ":Comment")

            If currentSubscriptions.Count > 0 Then

                Dim recipients As New List(Of UserInfo)
                For Each objSubscription As Subscription In currentSubscriptions
                    Dim objUser As UserInfo = UserController.GetUserById(PortalId, objSubscription.UserId)
                    recipients.Add(objUser)
                Next

                Dim objMessage As New DotNetNuke.Services.Social.Messaging.Message
                objMessage.Body = String.Format("The project {0} has a new comment. Please check it out: <a href={1}>Link to Project</a>", objProject.Subject, url)
                objMessage.SenderUserID = objProject.LeadBy
                objMessage.Subject = "New Project Comment"
                objMessage.ReplyAllAllowed = False
                objMessage.PortalID = PortalId

                DotNetNuke.Services.Social.Messaging.MessagingController.Instance.SendMessage(objMessage, Nothing, recipients, Nothing)

            End If

        End Sub

        Public Shared Sub SubscribeToProjectReleases(objUser As UserInfo, Project As ProjectInfo, ModuleId As Integer)

            Dim objSubscription As Subscription = CreateProjectReleaseSubscription(Project, ModuleId, objUser.UserID, objUser.PortalID)
            DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.AddSubscription(objSubscription)

        End Sub

        Public Shared Sub UnSubscribeFromProjectReleases(objUser As UserInfo, Project As ProjectInfo, ModuleId As Integer)

            Dim objSubscription As Subscription = CreateProjectReleaseSubscription(Project, ModuleId, objUser.UserID, objUser.PortalID)
            DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.DeleteSubscription(objSubscription)

        End Sub

        Public Shared Function IsSubscribedToProjectReleases(objUser As UserInfo, Project As ProjectInfo, ModuleId As Integer) As Boolean

            Dim objSubscription As Subscription = CreateProjectReleaseSubscription(Project, ModuleId, objUser.UserID, objUser.PortalID)
            Return DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.IsSubscribed(objSubscription)

        End Function

        Public Shared Sub NotifySubscribersAboutNewRelease(PortalId As Integer, objProject As ProjectInfo, url As String)

            Dim deskModuleId As Integer = DesktopModuleController.GetDesktopModuleByFriendlyName("Kickstart").DesktopModuleID
            Dim objType As SubscriptionType = SubscriptionTypeController.Instance.GetSubscriptionType(Function(st) (st.DesktopModuleId = deskModuleId AndAlso st.SubscriptionName = Integration.Subscription_NewReleaseTypeName))
            Dim currentSubscriptions As IEnumerable(Of Subscription) = DotNetNuke.Services.Social.Subscriptions.SubscriptionController.Instance.GetContentSubscriptions(PortalId, objType.SubscriptionTypeId, "ProjectId:" & objProject.ProjectId.ToString & ":Release")

            If currentSubscriptions.Count > 0 Then

                Dim recipients As New List(Of UserInfo)
                For Each objSubscription As Subscription In currentSubscriptions
                    Dim objUser As UserInfo = UserController.GetUserById(PortalId, objSubscription.UserId)
                    recipients.Add(objUser)
                Next

                'todo send message  
                Dim objMessage As New DotNetNuke.Services.Social.Messaging.Message
                objMessage.Body = String.Format("The project {0} has a new release. Please check it out: <a href={1}>Link to Project</a>", objProject.Subject, url)
                objMessage.SenderUserID = objProject.LeadBy
                objMessage.Subject = "New Project Release"
                objMessage.ReplyAllAllowed = False
                objMessage.PortalID = PortalId

                DotNetNuke.Services.Social.Messaging.MessagingController.Instance.SendMessage(objMessage, Nothing, recipients, Nothing)

            End If
            
        End Sub

        Private Shared Function CreateProjectListSubscription(ModuleId As Integer, UserId As Integer, PortalId As Integer) As Subscription

            Dim deskModuleId As Integer = DesktopModuleController.GetDesktopModuleByFriendlyName("Kickstart").DesktopModuleID
            Dim objType As SubscriptionType = SubscriptionTypeController.Instance.GetSubscriptionType(Function(st) (st.DesktopModuleId = deskModuleId AndAlso st.SubscriptionName = Integration.Subscription_NewProjectTypeName))

            Dim objSub As New Subscription
            objSub.Description = Utilities.GetSharedResource("NewProjectSubscriptionDescription")
            objSub.ModuleId = ModuleId
            objSub.ObjectData = ""
            objSub.ObjectKey = "ModuleId:" & ModuleId.ToString
            objSub.PortalId = PortalId
            objSub.SubscriptionTypeId = objType.SubscriptionTypeId
            objSub.UserId = UserId
            objSub.TabId = Null.NullInteger

            Return objSub

        End Function

        Private Shared Function CreateProjectCommentSubscription(Project As ProjectInfo, ModuleId As Integer, UserId As Integer, PortalId As Integer) As Subscription

            Dim deskModuleId As Integer = DesktopModuleController.GetDesktopModuleByFriendlyName("Kickstart").DesktopModuleID
            Dim objType As SubscriptionType = SubscriptionTypeController.Instance.GetSubscriptionType(Function(st) (st.DesktopModuleId = deskModuleId AndAlso st.SubscriptionName = Integration.Subscription_NewCommentTypeName))

            Dim objSub As New Subscription
            objSub.Description = String.Format(Utilities.GetSharedResource("KickstartPrefix"), HtmlUtils.Shorten(Project.Subject, 220, "..."))
            objSub.ModuleId = ModuleId
            objSub.ObjectData = "Comment"
            objSub.ObjectKey = "ProjectId:" & Project.ProjectId.ToString & ":Comment"
            objSub.PortalId = PortalId
            objSub.SubscriptionTypeId = objType.SubscriptionTypeId
            objSub.UserId = UserId
            objSub.TabId = Null.NullInteger

            Return objSub

        End Function

        Private Shared Function CreateProjectReleaseSubscription(Project As ProjectInfo, ModuleId As Integer, UserId As Integer, PortalId As Integer) As Subscription

            Dim deskModuleId As Integer = DesktopModuleController.GetDesktopModuleByFriendlyName("Kickstart").DesktopModuleID
            Dim objType As SubscriptionType = SubscriptionTypeController.Instance.GetSubscriptionType(Function(st) (st.DesktopModuleId = deskModuleId AndAlso st.SubscriptionName = Integration.Subscription_NewReleaseTypeName))

            Dim objSub As New Subscription
            objSub.Description = String.Format(Utilities.GetSharedResource("KickstartPrefix"), HtmlUtils.Shorten(Project.Subject, 220, "..."))
            objSub.ModuleId = ModuleId
            objSub.ObjectData = "Release"
            objSub.ObjectKey = "ProjectId:" & Project.ProjectId.ToString & ":Release"
            objSub.PortalId = PortalId
            objSub.SubscriptionTypeId = objType.SubscriptionTypeId
            objSub.UserId = UserId
            objSub.TabId = Null.NullInteger

            Return objSub

        End Function


#Region " Install Methods "
        ''' <summary>
        ''' This will create a subscription type associated w/ the module and also handle the actions that must be associated with it.
        ''' </summary>
        ''' <remarks>This should only ever run once, during 1.0.0 install (via IUpgradeable)</remarks>
        Friend Shared Sub AddSubscriptionTypes()

            Dim deskModuleId As Integer = DesktopModuleController.GetDesktopModuleByFriendlyName("Kickstart").DesktopModuleID

            Dim objType As New SubscriptionType
            objType.DesktopModuleId = deskModuleId
            objType.FriendlyName = "New Kickstart Project"
            objType.SubscriptionName = Integration.Subscription_NewProjectTypeName

            If SubscriptionTypeController.Instance.GetSubscriptionType(Function(st) (st.DesktopModuleId = deskModuleId AndAlso st.SubscriptionName = objType.SubscriptionName)) Is Nothing Then
                SubscriptionTypeController.Instance.AddSubscriptionType(objType)
            End If

            objType = New SubscriptionType
            objType.DesktopModuleId = deskModuleId
            objType.FriendlyName = "New Kickstart Comment"
            objType.SubscriptionName = Integration.Subscription_NewCommentTypeName

            If SubscriptionTypeController.Instance.GetSubscriptionType(Function(st) (st.DesktopModuleId = deskModuleId AndAlso st.SubscriptionName = objType.SubscriptionName)) Is Nothing Then
                SubscriptionTypeController.Instance.AddSubscriptionType(objType)
            End If

            objType = New SubscriptionType
            objType.DesktopModuleId = deskModuleId
            objType.FriendlyName = "New Kickstart Release"
            objType.SubscriptionName = Integration.Subscription_NewReleaseTypeName

            If SubscriptionTypeController.Instance.GetSubscriptionType(Function(st) (st.DesktopModuleId = deskModuleId AndAlso st.SubscriptionName = objType.SubscriptionName)) Is Nothing Then
                SubscriptionTypeController.Instance.AddSubscriptionType(objType)
            End If

        End Sub

#End Region

    End Class

End Namespace

