Namespace Connect.Modules.Kickstart.Entities

	Public Class IncentiveInfo
	
			Implements DotNetNuke.Entities.Modules.IHydratable
			
#Region "Private Members"
		Private _incentiveId as Int32
		Private _projectId as Int32
		Private _amount as decimal
		Private _incentive as String
#End Region
		
#Region "Constructors"
       
 Public Sub New()
        End Sub

#End Region
		
#Region "Public Properties"
		Public Property IncentiveId() as Int32
			Get
				Return _incentiveId
			End Get
			Set(ByVal Value as Int32)
				_incentiveId = Value
			End Set
		End Property
		
        
        
		Public Property ProjectId() as Int32 
			Get
				Return _projectId
			End Get
			Set(ByVal Value as Int32)
				_projectId = Value
			End Set
		End Property

        
        
		Public Property Amount() as decimal 
			Get
				Return _amount
			End Get
			Set(ByVal Value as decimal)
				_amount = Value
			End Set
		End Property

        
        
		Public Property Incentive() as String 
			Get
				Return _incentive
			End Get
			Set(ByVal Value as String)
				_incentive = Value
			End Set
		End Property
		
	Public Sub Fill(ByVal dr As System.Data.IDataReader) Implements DotNetNuke.Entities.Modules.IHydratable.Fill
    
        
    	try					
    		ProjectId = Convert.ToInt32(dr("ProjectId"))
    	catch
    	end try
    
        
    	try					
    		Amount = Convert.Todecimal(dr("Amount"))
    	catch
    	end try
    
        
    	try					
    		Incentive = Convert.ToString(dr("Incentive"))
    	catch
    	end try
    	try
    		IncentiveId = Convert.ToInt32(dr("IncentiveId"))
    	catch
    	end try
	End Sub

	Public Property KeyID() As Integer Implements DotNetNuke.Entities.Modules.IHydratable.KeyID
		Get
			Return _incentiveId
		End Get
		Set(ByVal value As Integer)
			_incentiveId = Value
		End Set
	End Property             
#End Region

	End Class

	
End Namespace