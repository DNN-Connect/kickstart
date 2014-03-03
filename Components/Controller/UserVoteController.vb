Imports DotNetNuke.Data
Imports DotNetNuke.Framework
Imports System.Collections.Generic
imports System.ComponentModel

Namespace Connect.Modules.Kickstart.Entities

	<DataObject(True)> _
	Public Class UserVoteController
	
#Region "Private Methods"
        Private Shared Function GetNull(ByVal Field As Object) As Object
            Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
        End Function
#End Region

#Region "Public Methods"

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [ListUserVotes](ByVal UserId As Integer, ByVal ProjectId As Integer, ByVal CommentId As Integer) As List(Of UserVoteInfo)

            Return CBO.FillCollection(Of UserVoteInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_UserVote_ListUserVotes", GetNull(UserId), GetNull(ProjectId), GetNull(CommentId)), IDataReader))

        End Function
        

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Add(ByVal objUserVoteInfo As UserVoteInfo) As Integer

            Return DotNetNuke.Data.DataProvider.Instance().ExecuteScalar(Of Integer)("Connect_Kickstart_UserVote_Add", GetNull(objUserVoteInfo.CommentId), GetNull(objUserVoteInfo.ProjectId), objUserVoteInfo.UserId, objUserVoteInfo.Votes)

        End Function
	
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal objUserVoteInfo As UserVoteInfo)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_UserVote_Delete", objUserVoteInfo.UserId, GetNull(objUserVoteInfo.CommentId), GetNull(objUserVoteInfo.ProjectId))

        End Sub
    
#End Region

	End Class



End Namespace