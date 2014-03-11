Imports DotNetNuke.Data
Imports DotNetNuke.Framework
Imports System.Collections.Generic
imports System.ComponentModel

Namespace Connect.Modules.Kickstart.Entities

	<DataObject(True)> _
	Public Class ProjectController
	
#Region "Private Methods"
        Public Shared Function GetNull(ByVal Field As Object) As Object
            Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
        End Function
#End Region

#Region "Public Methods"

        Public Shared Function [Get](ByVal projectId As Integer) As ProjectInfo

            Return CType(CBO.FillObject(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Project_Get", projectId), GetType(ProjectInfo)), ProjectInfo)

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [List]() As List(Of ProjectInfo)

            Return CBO.FillCollection(Of ProjectInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Project_List"), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [PublicProjectCount](ByVal ModuleId As Integer) As Integer

            Return DotNetNuke.Data.DataProvider.Instance().ExecuteScalar(Of Integer)("Connect_Kickstart_Project_GetPublicProjectCount", ModuleId)

        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function [PublicList](ByVal ModuleId As Integer, ByVal SortColumn As String, ByVal PageNo As Integer, RecordsPerPage As Integer, IsVisible As Integer, IsDeleted As Integer, CreatedBy As Integer, LeadBy As Integer) As List(Of ProjectInfo)

            Return CBO.FillCollection(Of ProjectInfo)(CType(DotNetNuke.Data.DataProvider.Instance().ExecuteReader("Connect_Kickstart_Project_GetPageByModules", SortColumn, PageNo, RecordsPerPage, ModuleId, GetNull(IsVisible), GetNull(IsDeleted), GetNull(CreatedBy), GetNull(LeadBy)), IDataReader))

        End Function

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Add(ByVal objProjectInfo As ProjectInfo) As Integer

            Return DotNetNuke.Data.DataProvider.Instance().ExecuteScalar(Of Integer)("Connect_Kickstart_Project_Add", objProjectInfo.ModuleId, objProjectInfo.ContentItemId, objProjectInfo.Status, objProjectInfo.Subject, objProjectInfo.Summary, objProjectInfo.Content, GetNull(objProjectInfo.ProjectUrl), GetNull(objProjectInfo.ProjectPlatform), GetNull(objProjectInfo.PlatformRssUrl), GetNull(objProjectInfo.DateScheduled), GetNull(objProjectInfo.DateDelivered), objProjectInfo.DateCreated, GetNull(objProjectInfo.DateLocked), GetNull(objProjectInfo.DateDeleted), objProjectInfo.CreatedBy, GetNull(objProjectInfo.LockedBy), GetNull(objProjectInfo.DeletedBy), GetNull(objProjectInfo.LeadBy), objProjectInfo.IsVisible, objProjectInfo.IsLocked, objProjectInfo.IsDeleted, objProjectInfo.IsDelivered, objProjectInfo.Views, objProjectInfo.Comments, objProjectInfo.Votes, objProjectInfo.TeamMembers)

        End Function
	
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal objProjectInfo As ProjectInfo)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Project_Update", objProjectInfo.ProjectId, objProjectInfo.ModuleId, objProjectInfo.ContentItemId, objProjectInfo.Status, objProjectInfo.Subject, objProjectInfo.Summary, objProjectInfo.Content, GetNull(objProjectInfo.ProjectUrl), GetNull(objProjectInfo.ProjectPlatform), GetNull(objProjectInfo.PlatformRssUrl), GetNull(objProjectInfo.DateScheduled), GetNull(objProjectInfo.DateDelivered), objProjectInfo.DateCreated, GetNull(objProjectInfo.DateLocked), GetNull(objProjectInfo.DateDeleted), objProjectInfo.CreatedBy, GetNull(objProjectInfo.LockedBy), GetNull(objProjectInfo.DeletedBy), GetNull(objProjectInfo.LeadBy), objProjectInfo.IsVisible, objProjectInfo.IsLocked, objProjectInfo.IsDeleted, objProjectInfo.IsDelivered, objProjectInfo.Views, objProjectInfo.Comments, objProjectInfo.Votes, objProjectInfo.TeamMembers)
        End Sub

        Public Shared Sub Lock(objProject As ProjectInfo, UserId As Integer)
            objProject.IsLocked = True
            objProject.LockedBy = UserId
            objProject.DateLocked = Date.Now
            Update(objProject)
        End Sub

        Public Shared Sub UnLock(objProject As ProjectInfo, UserId As Integer)
            objProject.IsLocked = False
            objProject.LockedBy = Null.NullInteger
            objProject.DateLocked = Null.NullDate
            Update(objProject)
        End Sub

        Public Shared Sub Delete(objProject As ProjectInfo, UserId As Integer)
            objProject.IsDeleted = True
            objProject.DeletedBy = UserId
            objProject.DateDeleted = Date.Now
            Update(objProject)
        End Sub

        Public Shared Sub Restore(objProject As ProjectInfo, UserId As Integer)
            objProject.IsDeleted = False
            objProject.DeletedBy = Null.NullInteger
            objProject.DateDeleted = Null.NullDate
            Update(objProject)
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub HardDelete(ByVal ProjectId As Integer)

            DotNetNuke.Data.DataProvider.Instance().ExecuteNonQuery("Connect_Kickstart_Project_Delete", ProjectId)

        End Sub
    
#End Region

	End Class



End Namespace