Imports DotNetNuke.Services.Journal
Imports Connect.Modules.Kickstart.Entities

Namespace Connect.Modules.Kickstart.Integration

    Public Class JournalController

        Public Shared Sub UpdateProjectInJournal(objProject As ProjectInfo, portalId As Integer, tabId As Integer, journalUserId As Integer, url As String, content As String)
            If journalUserId = -1 Then Exit Sub
            Dim objectKey As String = Integration.TypeName_UpdateProject + String.Format("{0}:{1}", objProject.ModuleId, objProject.ProjectId)
            Dim ji As JournalItem = DotNetNuke.Services.Journal.JournalController.Instance.GetJournalItemByKey(portalId, objectKey)

            If Not ji Is Nothing Then
                DotNetNuke.Services.Journal.JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey)
            End If

            ji = New JournalItem

            ji.PortalId = portalId
            ji.ProfileId = journalUserId
            ji.UserId = journalUserId
            ji.ContentItemId = objProject.ProjectId
            ji.Title = objProject.Subject
            ji.ItemData = New ItemData()
            ji.ItemData.Url = url
            ji.Summary = content
            ji.Body = Nothing
            ji.JournalTypeId = Integration.JournalTypeId_UpdateProject
            ji.ObjectKey = objectKey
            ji.SecuritySet = "E,"

            DotNetNuke.Services.Journal.JournalController.Instance.SaveJournalItem(ji, tabId)

        End Sub

        Public Shared Sub AddProjectToJournal(objProject As ProjectInfo, portalId As Integer, tabId As Integer, journalUserId As Integer, url As String)
            If journalUserId = -1 Then Exit Sub
            Dim objectKey As String = Integration.TypeName_AddProject + String.Format("{0}:{1}", objProject.ModuleId, objProject.ProjectId)
            Dim ji As JournalItem = DotNetNuke.Services.Journal.JournalController.Instance.GetJournalItemByKey(portalId, objectKey)

            If Not ji Is Nothing Then
                DotNetNuke.Services.Journal.JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey)
            End If

            ji = New JournalItem

            ji.PortalId = portalId
            ji.ProfileId = journalUserId
            ji.UserId = journalUserId
            ji.ContentItemId = objProject.ProjectId
            ji.Title = objProject.Subject
            ji.ItemData = New ItemData()
            ji.ItemData.Url = url
            ji.Summary = objProject.Summary
            ji.Body = Nothing
            ji.JournalTypeId = Integration.JournalTypeId_AddProject
            ji.ObjectKey = objectKey
            ji.SecuritySet = "E,"

            DotNetNuke.Services.Journal.JournalController.Instance.SaveJournalItem(ji, tabId)

        End Sub

        Public Shared Sub AddCommentToJournal(objProject As ProjectInfo, objComment As Kickstart.Entities.CommentInfo, portalId As Integer, tabId As Integer, journalUserId As Integer, url As String)
            If journalUserId = -1 Then Exit Sub
            Dim objectKey As String = Integration.TypeName_Comment + String.Format("{0}:{1}", objProject.ProjectId, objComment.CommentId)
            Dim ji As JournalItem = DotNetNuke.Services.Journal.JournalController.Instance.GetJournalItemByKey(portalId, objectKey)

            If Not ji Is Nothing Then
                DotNetNuke.Services.Journal.JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey)
            End If

            ji = New JournalItem

            ji.PortalId = portalId
            ji.ProfileId = journalUserId
            ji.UserId = journalUserId
            ji.ContentItemId = objComment.ProjectId
            ji.Title = objProject.Subject
            ji.ItemData = New ItemData()
            ji.ItemData.Url = url
            ji.Summary = HtmlUtils.Shorten(objComment.Content, 300, "..")
            ji.Body = Nothing
            ji.JournalTypeId = Integration.JournalTypeId_CommentOnProject
            ji.ObjectKey = objectKey
            ji.SecuritySet = "E,"

            DotNetNuke.Services.Journal.JournalController.Instance.SaveJournalItem(ji, tabId)

        End Sub

        Public Shared Sub AddFundingToJournal(objProject As ProjectInfo, objFunding As FundingInfo, portalId As Integer, tabId As Integer, journalUserId As Integer, url As String, content As String)
            If journalUserId = -1 Then Exit Sub
            Dim objectKey As String = Integration.TypeName_Fund + String.Format("{0}:{1}", objProject.ProjectId, objFunding.FundingId)
            Dim ji As JournalItem = DotNetNuke.Services.Journal.JournalController.Instance.GetJournalItemByKey(portalId, objectKey)

            If Not ji Is Nothing Then
                DotNetNuke.Services.Journal.JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey)
            End If

            ji = New JournalItem

            ji.PortalId = portalId
            ji.ProfileId = journalUserId
            ji.UserId = journalUserId
            ji.ContentItemId = objProject.ProjectId
            ji.Title = objProject.Subject
            ji.ItemData = New ItemData()
            ji.ItemData.Url = url
            ji.Summary = content
            ji.Body = Nothing
            ji.JournalTypeId = Integration.JournalTypeId_FundProject
            ji.ObjectKey = objectKey
            ji.SecuritySet = "E,"

            DotNetNuke.Services.Journal.JournalController.Instance.SaveJournalItem(ji, tabId)

        End Sub

        Public Shared Sub AddParticipationToJournal(objProject As ProjectInfo, objPaticipation As ParticipantInfo, portalId As Integer, tabId As Integer, journalUserId As Integer, url As String, content As String)
            If journalUserId = -1 Then Exit Sub
            Dim objectKey As String = Integration.TypeName_Participate + String.Format("{0}:{1}", objProject.ProjectId, objPaticipation.ParticipationId)
            Dim ji As JournalItem = DotNetNuke.Services.Journal.JournalController.Instance.GetJournalItemByKey(portalId, objectKey)

            If Not ji Is Nothing Then
                DotNetNuke.Services.Journal.JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey)
            End If

            ji = New JournalItem

            ji.PortalId = portalId
            ji.ProfileId = journalUserId
            ji.UserId = journalUserId
            ji.ContentItemId = objProject.ProjectId
            ji.Title = objProject.Subject
            ji.ItemData = New ItemData()
            ji.ItemData.Url = url
            ji.Summary = content
            ji.Body = Nothing
            ji.JournalTypeId = Integration.JournalTypeId_ParticipateInProject
            ji.ObjectKey = objectKey
            ji.SecuritySet = "E,"

            DotNetNuke.Services.Journal.JournalController.Instance.SaveJournalItem(ji, tabId)

        End Sub

        Public Shared Sub AddLeaderToJournal(objProject As ProjectInfo, objPaticipation As ParticipantInfo, portalId As Integer, tabId As Integer, journalUserId As Integer, url As String, content As String)
            If journalUserId = -1 Then Exit Sub
            Dim objectKey As String = Integration.TypeName_Lead + String.Format("{0}:{1}", objProject.ModuleId, objProject.ProjectId)
            Dim ji As JournalItem = DotNetNuke.Services.Journal.JournalController.Instance.GetJournalItemByKey(portalId, objectKey)

            If Not ji Is Nothing Then
                DotNetNuke.Services.Journal.JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey)
            End If

            ji = New JournalItem

            ji.PortalId = portalId
            ji.ProfileId = journalUserId
            ji.UserId = journalUserId
            ji.ContentItemId = objProject.ProjectId
            ji.Title = objProject.Subject
            ji.ItemData = New ItemData()
            ji.ItemData.Url = url
            ji.Summary = content
            ji.Body = Nothing
            ji.JournalTypeId = Integration.JournalTypeId_LeadProject
            ji.ObjectKey = objectKey
            ji.SecuritySet = "E,"

            DotNetNuke.Services.Journal.JournalController.Instance.SaveJournalItem(ji, tabId)

        End Sub

    End Class

End Namespace

