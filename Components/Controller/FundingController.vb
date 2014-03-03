Imports DotNetNuke.Data
Imports DotNetNuke.Framework
Imports System.Collections.Generic
imports System.ComponentModel

Namespace Connect.Modules.Kickstart.Entities

	<DataObject(True)> _
	Public Class FundingController
	
#Region "Private Methods"
        Public Shared Function GetNull(ByVal Field As Object) As Object
            Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
        End Function
#End Region

#Region "Public Methods"

        Public Shared Function [Get](ByVal fundingId As Integer) As FundingInfo

            Return CType(CBO.FillObject(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Funding_Get", fundingId), GetType(FundingInfo)), FundingInfo)

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [List]() As List(Of FundingInfo)

            Return CBO.FillCollection(Of FundingInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Funding_List"), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [ListByProject](ByVal ProjectId As Integer) As List(Of FundingInfo)

            Return CBO.FillCollection(Of FundingInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Funding_ListByProject", ProjectId), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [ListByUsers](ByVal UserId As Integer) As List(Of FundingInfo)

            Return CBO.FillCollection(Of FundingInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Funding_ListByUsers", UserId), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Add(ByVal objFundingInfo As FundingInfo) As Integer

            Return DotNetNuke.Data.DataProvider.Instance().ExecuteScalar(Of Integer)("Connect_Kickstart_Funding_Add", objFundingInfo.ProjectId, objFundingInfo.UserId, objFundingInfo.Funding, objFundingInfo.Anonymous, objFundingInfo.FundingReceived)

        End Function
	
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal objFundingInfo As FundingInfo)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Funding_Update", objFundingInfo.FundingId, objFundingInfo.ProjectId, objFundingInfo.UserId, objFundingInfo.Funding, objFundingInfo.Anonymous, objFundingInfo.FundingReceived)

        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal FundingId As Integer)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Funding_Delete", FundingId)

        End Sub
    
#End Region

#Region "Public Helpers"

        Public Shared Function GetFunds(ProjectId As Integer) As Decimal

            Dim Amount As Decimal = CDec(0.0)

            For Each objFund As FundingInfo In ListByProject(ProjectId)
                Amount += objFund.Funding
            Next

            Return Amount

        End Function

#End Region

	End Class



End Namespace