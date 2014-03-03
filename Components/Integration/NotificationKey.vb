
Namespace Connect.Modules.Kickstart.Integration
    Public Class NotificationKey

        Public ID As String = ""
        Public ModuleId As Integer = Null.NullInteger
        Public ProjectId As Integer = Null.NullInteger
        Public CommentId As Integer = Null.NullInteger
        Public ParticipationId As Integer = Null.NullInteger
        Public FundingId As Integer = Null.NullInteger

        Public Sub New(key As String)
            Dim keyParts() As String = key.Split(":"c)
            If keyParts.Length < 6 Then Exit Sub
            ID = keyParts(0)
            ModuleId = Integer.Parse(keyParts(1))
            ProjectId = Integer.Parse(keyParts(2))
            CommentId = Integer.Parse(keyParts(3))
            ParticipationId = Integer.Parse(keyParts(4))
            FundingId = Integer.Parse(keyParts(5))
        End Sub

        Public Sub New(id As String, moduleId As Integer, projectId As Integer, commentId As Integer, participationId As Integer, fundingId As Integer)
            Me.ID = id
            Me.ModuleId = moduleId
            Me.ProjectId = projectId
            Me.CommentId = commentId
            Me.ParticipationId = participationId
            Me.FundingId = fundingId
        End Sub

        Public Shadows Function ToString() As String
            Return String.Format("{0}:{1}:{2}:{3}:{4}:{5}", ID, ModuleId, ProjectId, CommentId, ParticipationId, FundingId)
        End Function

    End Class
End Namespace
