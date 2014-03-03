Imports DotNetNuke.Data
Imports DotNetNuke.Framework
Imports System.Collections.Generic
imports System.ComponentModel

Namespace Connect.Modules.Kickstart.Entities

	<DataObject(True)> _
	Public Class IncentiveController
	
#Region "Private Methods"
        Public Shared Function GetNull(ByVal Field As Object) As Object
            Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
        End Function
#End Region

#Region "Public Methods"

        Public Shared Function GetByAmount(ProjectId As Integer, Amount As Decimal) As IncentiveInfo

            For Each objIncentive As IncentiveInfo In ListByProject(ProjectId)
                If objIncentive.Amount = Amount Then
                    Return objIncentive
                End If
            Next

            Return Nothing

        End Function

        Public Shared Function [Get](ByVal incentiveId As Integer) As IncentiveInfo

            Return CType(CBO.FillObject(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Incentive_Get", incentiveId), GetType(IncentiveInfo)), IncentiveInfo)

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [List]() As List(Of IncentiveInfo)

            Return CBO.FillCollection(Of IncentiveInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Incentive_List"), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [ListByProject](ByVal ProjectId As Integer) As List(Of IncentiveInfo)

            Return CBO.FillCollection(Of IncentiveInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Incentive_ListByProject", ProjectId), IDataReader))

        End Function



        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Add(ByVal objIncentiveInfo As IncentiveInfo) As Integer

            Return DotNetNuke.Data.DataProvider.Instance().ExecuteScalar(Of Integer)("Connect_Kickstart_Incentive_Add", GetNull(objIncentiveInfo.ProjectId), GetNull(objIncentiveInfo.Amount), GetNull(objIncentiveInfo.Incentive))

        End Function

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal objIncentiveInfo As IncentiveInfo)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Incentive_Update", GetNull(objIncentiveInfo.IncentiveId), GetNull(objIncentiveInfo.ProjectId), GetNull(objIncentiveInfo.Amount), GetNull(objIncentiveInfo.Incentive))

        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal IncentiveId As Integer)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Incentive_Delete", IncentiveId)

        End Sub

#End Region

    End Class



End Namespace