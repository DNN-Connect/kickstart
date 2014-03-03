Imports System.Linq
Imports DotNetNuke.Entities.Content.Taxonomy

Namespace Connect.Modules.Kickstart.Integration

    Public Class Integration


        Public Const TypeName_AddProject As String = "Connect_Kickstart_AddProject"
        Public Const TypeName_UpdateProject As String = "Connect_Kickstart_UpdateProject"
        Public Const TypeName_Comment As String = "Connect_Kickstart_CommentOnProject"
        Public Const TypeName_Fund As String = "Connect_Kickstart_FundProject"
        Public Const TypeName_Participate As String = "Connect_Kickstart_ParticipateInProject"
        Public Const TypeName_Lead As String = "Connect_Kickstart_LeadProject"

        Public Const JournalTypeId_AddProject As Integer = 31
        Public Const JournalTypeId_CommentOnProject As Integer = 18
        Public Const JournalTypeId_ParticipateInProject As Integer = 24
        Public Const JournalTypeId_FundProject As Integer = 24
        Public Const JournalTypeId_UpdateProject As Integer = 24
        Public Const JournalTypeId_LeadProject As Integer = 24

        Public Const Notification_ProjectVisibilityChanged As String = "Connect_Kickstart_ProjectVisibilityChanged"
        Public Const Notification_NewProjectTypeName As String = "Connect_Kickstart_NewProject"
        Public Const Notification_NewParticipantTypeName As String = "Connect_Kickstart_NewParticipant"
        Public Const Notification_NewFundingTypeName As String = "Connect_Kickstart_NewFunding"
        Public Const Notification_NewCommentTypeName As String = "Connect_Kickstart_NewComment"

        Public Const Subscription_NewProjectTypeName As String = "Connect_Kickstart_NewProject"
        Public Const Subscription_NewReleaseTypeName As String = "Connect_Kickstart_NewRelease"
        Public Const Subscription_NewCommentTypeName As String = "Connect_Kickstart_NewComment"

        Public Shared Function CreateNewVocabulary(portalId As Integer) As Vocabulary

            Dim name As String = "Blog Categories"
            Dim cntScope As New ScopeTypeController
            Dim cntVocabulary As New VocabularyController
            Dim i As Integer = 1
            Do While cntVocabulary.GetVocabularies.Where(Function(v) v.Name = name).Count > 0
                name = "Kickstart Categories " + i.ToString
            Loop
            Dim objScope As ScopeType = cntScope.GetScopeTypes().Where(Function(s) s.ScopeType = "Portal").SingleOrDefault()
            Dim objVocab As New Vocabulary
            objVocab.Name = name
            objVocab.IsSystem = False
            objVocab.Weight = 0
            objVocab.Description = "Automatically generated for kickstart projects."
            objVocab.ScopeId = portalId
            objVocab.ScopeTypeId = objScope.ScopeTypeId
            objVocab.Type = VocabularyType.Hierarchy
            objVocab.VocabularyId = cntVocabulary.AddVocabulary(objVocab)
            Return objVocab

        End Function

    End Class

End Namespace
