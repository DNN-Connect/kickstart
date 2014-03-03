Imports DotNetNuke.Data
Imports DotNetNuke.Framework
Imports System.Collections.Generic
imports System.ComponentModel

Namespace Connect.Modules.Kickstart.Entities

	<DataObject(True)> _
	Public Class ProjectConfigController
	
#Region "Private Methods"
	Private Function GetNull(ByVal Field As Object) As Object
			Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
	End Function
#End Region

#Region "Public Methods"

		Public Function [Get](ByVal settingId As Integer) As ProjectConfigInfo
			
			Return CType(CBO.FillObject(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_ProjectConfig_Get", settingId), GetType(ProjectConfigInfo)), ProjectConfigInfo)

		End Function

		<DataObjectMethod(DataObjectMethodType.Select, True)> _
		Public Function [List]() as List(of ProjectConfigInfo) 
		
			Return CBO.FillCollection(of ProjectConfigInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_ProjectConfig_List"), IDataReader))
		
		End Function

		<DataObjectMethod(DataObjectMethodType.Select, True)> _
		Public Function [ListByProject](ByVal ProjectId as Integer) as List(of ProjectConfigInfo) 
		
			Return CBO.FillCollection(of ProjectConfigInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_ProjectConfig_ListByProject", ProjectId), IDataReader))
		
		End Function
        
        
	
		<DataObjectMethod(DataObjectMethodType.Insert, True)> _
		Public Function Add(ByVal objProjectConfigInfo As ProjectConfigInfo) as Integer
			
			Return DotNetNuke.Data.DataProvider.Instance().ExecuteScalar(of Integer)("Connect_Kickstart_ProjectConfig_Add", GetNull(objProjectConfigInfo.ProjectId), GetNull(objProjectConfigInfo.Value), GetNull(objProjectConfigInfo.Setting))

		End Function
	
		<DataObjectMethod(DataObjectMethodType.Update, True)> _
		Public Sub Update(ByVal objProjectConfigInfo As ProjectConfigInfo)
			
			DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Update", GetNull(objProjectConfigInfo.SettingId), GetNull(objProjectConfigInfo.ProjectId), GetNull(objProjectConfigInfo.Value), GetNull(objProjectConfigInfo.Setting))
			
		End Sub
		
		<DataObjectMethod(DataObjectMethodType.Delete, True)> _
		Public Sub Delete(ByVal SettingId As Integer)
			
			DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_ProjectConfig_Delete", SettingId)
		
		End Sub
    
#End Region

	End Class



End Namespace