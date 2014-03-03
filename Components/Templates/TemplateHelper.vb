Imports System.IO

Public Class TemplateHelper

    Public Const Template_ProjectHeader As String = "Project_ListHeader.html"
    Public Const Template_ProjectItem As String = "Project_ListItem.html"
    Public Const Template_ProjectFooter As String = "Project_ListFooter.html"
    Public Const Template_ProjectDetails As String = "Project_Details.html"
    Public Const Template_ProjectStatus As String = "Project_Status.html"

    Public Const Template_CommentHeader As String = "Comment_ListHeader.html"
    Public Const Template_CommentItem As String = "Comment_ListItem.html"
    Public Const Template_CommentFooter As String = "Comment_ListFooter.html"

    Public Const Template_ParticipantsHeader As String = "Participants_ListHeader.html"
    Public Const Template_ParticipantsItem As String = "Participants_ListItem.html"
    Public Const Template_ParticipantsFooter As String = "Participants_ListFooter.html"

    Public Shared Function ReadTemplate(strTemplatePath As String) As String

        If File.Exists(strTemplatePath) Then
            Dim sr As New StreamReader(strTemplatePath)
            Dim strContent As String = sr.ReadToEnd
            sr.Close()
            sr.Dispose()
            Return strContent
        End If

        Return ""

    End Function


End Class
