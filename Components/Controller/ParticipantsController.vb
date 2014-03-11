Imports DotNetNuke.Data
Imports DotNetNuke.Framework
Imports System.Collections.Generic
Imports System.ComponentModel

Namespace Connect.Modules.Kickstart.Entities

    <DataObject(True)> _
    Public Class ParticipantController

#Region "Private Methods"
        Public Shared Function GetNull(ByVal Field As Object) As Object
            Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
        End Function
#End Region

#Region "Public Methods"

        Public Shared Function GetUserParticipation(UserId As Integer, ProjectId As Integer) As List(Of ParticipantInfo)

            Dim lst As New List(Of ParticipantInfo)

            For Each objParticipant As ParticipantInfo In ListByProject(ProjectId)
                If objParticipant.UserId = UserId Then
                    lst.Add(objParticipant)
                End If
            Next

            Return lst

        End Function

        Public Shared Function [Get](ByVal fundingId As Integer) As ParticipantInfo

            Return CType(CBO.FillObject(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Participant_Get", fundingId), GetType(ParticipantInfo)), ParticipantInfo)

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [List]() As List(Of ParticipantInfo)

            Return CBO.FillCollection(Of ParticipantInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Participant_List"), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [ListByProject](ByVal ProjectId As Integer) As List(Of ParticipantInfo)

            Return CBO.FillCollection(Of ParticipantInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Participant_ListByProject", ProjectId), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [ListByUsers](ByVal UserId As Integer) As List(Of ParticipantInfo)

            Return CBO.FillCollection(Of ParticipantInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Participant_ListByUsers", UserId), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Add(ByVal objParticipantInfo As ParticipantInfo) As Integer

            Return DotNetNuke.Data.DataProvider.Instance().ExecuteScalar(Of Integer)("Connect_Kickstart_Participant_Add", GetNull(objParticipantInfo.ProjectId), GetNull(objParticipantInfo.UserId), GetNull(objParticipantInfo.ProjectRole))

        End Function

        'Public Shared Sub UpdateParticipation(ProjectRoles As List(Of Integer), ProjectId As Integer, UserId As Integer)

        '    Dim lstCurrent As New List(Of ParticipantInfo)
        '    lstCurrent = ListByProject(ProjectId)
        '    For Each objP As ParticipantInfo In lstCurrent
        '        If objP.UserId = UserId Then
        '            Delete(objP.ParticipationId)
        '        End If
        '    Next

        '    For Each RoleId As Integer In ProjectRoles
        '        Dim objNP As New ParticipantInfo
        '        objNP.ProjectId = ProjectId
        '        objNP.ProjectRole = RoleId
        '        objNP.UserId = UserId
        '        Add(objNP)
        '    Next

        'End Sub

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal objParticipantInfo As ParticipantInfo)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Participant_Update", GetNull(objParticipantInfo.ParticipationId), GetNull(objParticipantInfo.ProjectId), GetNull(objParticipantInfo.UserId), GetNull(objParticipantInfo.ProjectRole))

        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal ParticipationId As Integer)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Participant_Delete", ParticipationId)

        End Sub

#End Region

    End Class

End Namespace