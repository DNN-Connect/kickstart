Imports DotNetNuke.Data
Imports DotNetNuke.Framework
Imports System.Collections.Generic
Imports System.ComponentModel

Namespace Connect.Modules.Kickstart.Entities

    <DataObject(True)> _
    Public Class CommentController

#Region "Private Methods"
        Private Shared Function GetNull(ByVal Field As Object) As Object
            Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
        End Function
#End Region

#Region "Public Methods"

        Public Shared Function [Get](ByVal commentId As Integer) As CommentInfo

            Return CType(CBO.FillObject(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Comment_Get", commentId), GetType(CommentInfo)), CommentInfo)

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [List]() As List(Of CommentInfo)

            Return CBO.FillCollection(Of CommentInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Comment_List"), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [ListByProject](ByVal ProjectId As Integer, ParentId As Integer) As List(Of CommentInfo)

            Return CBO.FillCollection(Of CommentInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Comment_ListByProject", ProjectId, GetNull(ParentId)), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [ListByUsers](ByVal CreatedBy As Integer) As List(Of CommentInfo)

            Return CBO.FillCollection(Of CommentInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Comment_ListByUsers", CreatedBy), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Add(ByVal objCommentInfo As CommentInfo) As Integer

            Return DotNetNuke.Data.DataProvider.Instance().ExecuteScalar(Of Integer)("Connect_Kickstart_Comment_Add", objCommentInfo.ProjectId, objCommentInfo.ContentItemId, GetNull(objCommentInfo.ParentId), objCommentInfo.Content, objCommentInfo.IsVisible, objCommentInfo.CreatedBy, objCommentInfo.DateCreated, objCommentInfo.Votes, objCommentInfo.Comments, objCommentInfo.IsTeamResponse)

        End Function

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal objCommentInfo As CommentInfo)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Comment_Update", objCommentInfo.CommentId, objCommentInfo.ProjectId, objCommentInfo.ContentItemId, GetNull(objCommentInfo.ParentId), objCommentInfo.Content, objCommentInfo.IsVisible, objCommentInfo.CreatedBy, objCommentInfo.DateCreated, objCommentInfo.Votes, objCommentInfo.Comments, objCommentInfo.IsTeamResponse)

        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal CommentId As Integer)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Comment_Delete", CommentId)

        End Sub

#End Region

    End Class



End Namespace