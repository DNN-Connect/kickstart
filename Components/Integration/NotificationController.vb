Imports DotNetNuke.Services.Social.Notifications
Imports Connect.Modules.Kickstart.Entities
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Entities.Modules

Namespace Connect.Modules.Kickstart.Integration

    Public Class NotificationController

        Public Shared Sub SendPendingProjectNotification(objProject As ProjectInfo, PortalId As Integer, url As String)

            Dim strSubject As String = String.Format(Utilities.GetSharedResource("NewIdeaNotificationSubject"), objProject.Subject)
            Dim strMessage As String = String.Format(Utilities.GetSharedResource("NewIdeaNotificationBody"), objProject.Summary, url)

            Dim notificationType As NotificationType = NotificationsController.Instance.GetNotificationType(Integration.Notification_NewProjectTypeName)
            Dim notificationKey As New NotificationKey(Integration.Notification_NewProjectTypeName, objProject.ModuleId, objProject.ProjectId, Null.NullInteger, Null.NullInteger, Null.NullInteger)

            Dim objNotification As New Notification
            objNotification.NotificationTypeID = notificationType.NotificationTypeId
            objNotification.Subject = strSubject
            objNotification.Body = strMessage
            objNotification.IncludeDismissAction = True
            objNotification.SenderUserID = objProject.CreatedBy
            objNotification.Context = notificationKey.ToString

            Dim ctrlRoles As New DotNetNuke.Security.Roles.RoleController


            NotificationsController.Instance.SendNotification(objNotification, portalId, Nothing, Utilities.GetModuleEditors(objProject.ModuleId))

        End Sub

        Public Shared Sub SendNewCommentNotification(objProject As ProjectInfo, objComment As CommentInfo, objUser As UserInfo, url As String)

            Dim strMessage As String = String.Format(Utilities.GetSharedResource("NewCommentNotificationBody"), Utilities.GetUserProfileUrl(objUser.UserID), objUser.DisplayName, objProject.Subject, objComment.Content, url)
            Dim strSubject As String = String.Format(Utilities.GetSharedResource("NewCommentNotificationSubject"), objProject.Subject)

            Dim notificationType As NotificationType = NotificationsController.Instance.GetNotificationType(Integration.Notification_NewCommentTypeName)
            Dim notificationKey As New NotificationKey(Integration.Notification_NewProjectTypeName, objProject.ModuleId, objProject.ProjectId, objComment.CommentId, Null.NullInteger, Null.NullInteger)

            Dim objNotification As New Notification
            objNotification.NotificationTypeID = notificationType.NotificationTypeId
            objNotification.Subject = strSubject
            objNotification.Body = strMessage
            objNotification.IncludeDismissAction = True
            objNotification.SenderUserID = objProject.CreatedBy
            objNotification.Context = notificationKey.ToString

            Dim ctrlRoles As New DotNetNuke.Security.Roles.RoleController


            NotificationsController.Instance.SendNotification(objNotification, objUser.PortalID, Nothing, Utilities.GetProjectParticipants(objProject.ProjectId))

        End Sub

        Public Shared Sub SendNewParticipationNotification(objProject As ProjectInfo, objParticipation As ParticipantInfo, objUser As UserInfo, url As String, cancelled As Boolean)

            Dim strMessage As String = String.Format(Utilities.GetSharedResource("NewParticipationNotificationBody"), Utilities.GetUserProfileUrl(objUser.UserID), objUser.DisplayName, objProject.Subject, objParticipation.ProjectRole.ToString, url)
            Dim strSubject As String = String.Format("{0}{1}", Utilities.GetSharedResource("NewParticipationNotificationSubject"), objProject.Subject)

            If cancelled Then
                strMessage = String.Format(Utilities.GetSharedResource("RemovedParticipationNotificationBody"), Utilities.GetUserProfileUrl(objUser.UserID), objUser.DisplayName, objProject.Subject, objParticipation.ProjectRole.ToString, url)
                strSubject = String.Format("{0}{1}", Utilities.GetSharedResource("RemovedParticipationNotificationSubject"), objProject.Subject)
            End If

            Dim notificationType As NotificationType = NotificationsController.Instance.GetNotificationType(Integration.Notification_NewParticipantTypeName)
            Dim notificationKey As New NotificationKey(Integration.Notification_NewProjectTypeName, objProject.ModuleId, objProject.ProjectId, Null.NullInteger, objParticipation.ParticipationId, Null.NullInteger)

            Dim objNotification As New Notification
            objNotification.NotificationTypeID = notificationType.NotificationTypeId
            objNotification.Subject = strSubject
            objNotification.Body = strMessage
            objNotification.IncludeDismissAction = True
            objNotification.SenderUserID = objProject.CreatedBy
            objNotification.Context = notificationKey.ToString

            Dim ctrlRoles As New DotNetNuke.Security.Roles.RoleController


            NotificationsController.Instance.SendNotification(objNotification, objUser.PortalID, Nothing, Utilities.GetProjectParticipants(objProject.ProjectId))

        End Sub

        Public Shared Sub SendNewFundingNotification(objProject As ProjectInfo, objFunding As FundingInfo, objUser As UserInfo, url As String, Update As Boolean, Cancelled As Boolean)


            Dim strMessage As String = String.Format(Utilities.GetSharedResource("NewFundingNotificationBody"), Utilities.GetUserProfileUrl(objUser.UserID), objUser.DisplayName, objProject.Subject, Utilities.FormatFunding(objFunding), url)
            Dim strSubject As String = String.Format("{0}{1}", Utilities.GetSharedResource("NewFundingNotificationSubject"), objProject.Subject)

            If Update Then
                strMessage = String.Format(Utilities.GetSharedResource("UpdatedFundingNotificationBody"), Utilities.GetUserProfileUrl(objUser.UserID), objUser.DisplayName, objProject.Subject, Utilities.FormatFunding(objFunding), url)
                strSubject = String.Format("{0}{1}", Utilities.GetSharedResource("UpdatedFundingNotificationSubject"), objProject.Subject)
            End If

            If Cancelled Then
                strMessage = String.Format(Utilities.GetSharedResource("CancelledFundingNotificationBody"), Utilities.GetUserProfileUrl(objUser.UserID), objUser.DisplayName, objProject.Subject, Utilities.FormatFunding(objFunding), url)
                strSubject = String.Format("{0}{1}", Utilities.GetSharedResource("CancelledFundingNotificationSubject"), objProject.Subject)
            End If

            Dim notificationType As NotificationType = NotificationsController.Instance.GetNotificationType(Integration.Notification_NewFundingTypeName)
            Dim notificationKey As New NotificationKey(Integration.Notification_NewProjectTypeName, objProject.ModuleId, objProject.ProjectId, Null.NullInteger, Null.NullInteger, objFunding.FundingId)

            Dim objNotification As New Notification
            objNotification.NotificationTypeID = notificationType.NotificationTypeId
            objNotification.Subject = strSubject
            objNotification.Body = strMessage
            objNotification.IncludeDismissAction = True
            objNotification.SenderUserID = objProject.CreatedBy
            objNotification.Context = notificationKey.ToString

            Dim ctrlRoles As New DotNetNuke.Security.Roles.RoleController

            NotificationsController.Instance.SendNotification(objNotification, objUser.PortalID, Nothing, Utilities.GetProjectParticipants(objProject.ProjectId))

        End Sub

        Public Shared Sub SendVisibilityChangedNotification(objProject As ProjectInfo, objUser As UserInfo, url As String)

            Dim strMessage As String = String.Format(Utilities.GetSharedResource("ProjectVisibilityChangedBody"), objProject.Subject, Utilities.FormatProjectVisiblity(objProject), url)
            Dim strSubject As String = Utilities.GetSharedResource("ProjectVisibilityChangedSubject")

            Dim notificationType As NotificationType = NotificationsController.Instance.GetNotificationType(Integration.Notification_ProjectVisibilityChanged)
            Dim notificationKey As New NotificationKey(Integration.Notification_ProjectVisibilityChanged, objProject.ModuleId, objProject.ProjectId, Null.NullInteger, Null.NullInteger, Null.NullInteger)

            Dim objNotification As New Notification
            objNotification.NotificationTypeID = notificationType.NotificationTypeId
            objNotification.Subject = strSubject
            objNotification.Body = strMessage
            objNotification.IncludeDismissAction = True
            objNotification.SenderUserID = objUser.UserID
            objNotification.Context = notificationKey.ToString

            Dim ctrlRoles As New DotNetNuke.Security.Roles.RoleController

            NotificationsController.Instance.SendNotification(objNotification, objUser.PortalID, Nothing, Utilities.GetProjectParticipants(objProject.ProjectId))

        End Sub

#Region " Install Methods "
        ''' <summary>
        ''' This will create a notification type associated w/ the module and also handle the actions that must be associated with it.
        ''' </summary>
        ''' <remarks>This should only ever run once, during 1.0.0 install (via IUpgradeable)</remarks>
        Friend Shared Sub AddNotificationTypes()

            Dim actions As List(Of NotificationTypeAction) = New List(Of NotificationTypeAction)
            Dim deskModuleId As Integer = DesktopModuleController.GetDesktopModuleByFriendlyName("Kickstart").DesktopModuleID

            Dim objNotificationType As NotificationType = New NotificationType
            objNotificationType.Name = Integration.Notification_NewCommentTypeName
            objNotificationType.Description = "Kickstart comment added."
            objNotificationType.DesktopModuleId = deskModuleId

            If NotificationsController.Instance.GetNotificationType(objNotificationType.Name) Is Nothing Then
                NotificationsController.Instance.CreateNotificationType(objNotificationType)
            End If

            objNotificationType = New NotificationType
            objNotificationType.Name = Integration.Notification_NewFundingTypeName
            objNotificationType.Description = "Kickstart funding added."
            objNotificationType.DesktopModuleId = deskModuleId

            If NotificationsController.Instance.GetNotificationType(objNotificationType.Name) Is Nothing Then
                NotificationsController.Instance.CreateNotificationType(objNotificationType)
            End If

            objNotificationType = New NotificationType
            objNotificationType.Name = Integration.Notification_ProjectVisibilityChanged
            objNotificationType.Description = "Kickstart visibility changed."
            objNotificationType.DesktopModuleId = deskModuleId

            If NotificationsController.Instance.GetNotificationType(objNotificationType.Name) Is Nothing Then
                NotificationsController.Instance.CreateNotificationType(objNotificationType)
            End If

            objNotificationType = New NotificationType
            objNotificationType.Name = Integration.Notification_NewParticipantTypeName
            objNotificationType.Description = "Kickstart participation added."
            objNotificationType.DesktopModuleId = deskModuleId

            If NotificationsController.Instance.GetNotificationType(objNotificationType.Name) Is Nothing Then
                NotificationsController.Instance.CreateNotificationType(objNotificationType)
            End If

            objNotificationType = New NotificationType
            objNotificationType.Name = Integration.Notification_NewProjectTypeName
            objNotificationType.Description = "Kickstart project added."
            objNotificationType.DesktopModuleId = deskModuleId

            If NotificationsController.Instance.GetNotificationType(objNotificationType.Name) Is Nothing Then

                'actions.Clear()
                'Dim objAction As New NotificationTypeAction
                'objAction.NameResourceKey = "KickstartApproveIdea"
                'objAction.DescriptionResourceKey = "KickstartApproveIdea_Desc"
                'objAction.APICall = "DesktopModules/Connect/Kickstart/API/Kickstart/ApproveProject"
                'objAction.Order = 1
                'actions.Add(objAction)

                'objAction = New NotificationTypeAction
                'objAction.NameResourceKey = "KickstartDeleteIdea"
                'objAction.DescriptionResourceKey = "KickstartDeleteIdea_Desc"
                'objAction.APICall = "DesktopModules/Connect/Kickstart/API/Kickstart/DeleteProject"
                'objAction.ConfirmResourceKey = "KickstartDeleteIdeaConfirm"
                'objAction.Order = 3
                'actions.Add(objAction)

                NotificationsController.Instance.CreateNotificationType(objNotificationType)
                'NotificationsController.Instance.SetNotificationTypeActions(actions, objNotificationType.NotificationTypeId)

            End If

        End Sub

#End Region

    End Class

End Namespace

