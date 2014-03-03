Namespace Connect.Modules.Kickstart.Entities

	Public Class FundingInfo
	
			Implements DotNetNuke.Entities.Modules.IHydratable
			
#Region "Private Members"
		Private _fundingId as Int32
		Private _projectId as Int32
		Private _userId as Int32
		Private _funding as decimal
		Private _anonymous as Boolean
		Private _fundingReceived as Boolean
#End Region
		
#Region "Constructors"
       
 Public Sub New()
        End Sub

#End Region
		
#Region "Public Properties"
		Public Property FundingId() as Int32
			Get
				Return _fundingId
			End Get
			Set(ByVal Value as Int32)
				_fundingId = Value
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

        
        
		Public Property UserId() as Int32 
			Get
				Return _userId
			End Get
			Set(ByVal Value as Int32)
				_userId = Value
			End Set
		End Property

        
        
		Public Property Funding() as decimal 
			Get
				Return _funding
			End Get
			Set(ByVal Value as decimal)
				_funding = Value
			End Set
		End Property

        
        
		Public Property Anonymous() as Boolean 
			Get
				Return _anonymous
			End Get
			Set(ByVal Value as Boolean)
				_anonymous = Value
			End Set
		End Property

        
        
		Public Property FundingReceived() as Boolean 
			Get
				Return _fundingReceived
			End Get
			Set(ByVal Value as Boolean)
				_fundingReceived = Value
			End Set
		End Property
		
	Public Sub Fill(ByVal dr As System.Data.IDataReader) Implements DotNetNuke.Entities.Modules.IHydratable.Fill
    
        
    	try					
    		ProjectId = Convert.ToInt32(dr("ProjectId"))
    	catch
    	end try
    
        
    	try					
    		UserId = Convert.ToInt32(dr("UserId"))
    	catch
    	end try
    
        
    	try					
    		Funding = Convert.Todecimal(dr("Funding"))
    	catch
    	end try
    
        
    	try					
    		Anonymous = Convert.ToBoolean(dr("Anonymous"))
    	catch
    	end try
    
        
    	try					
    		FundingReceived = Convert.ToBoolean(dr("FundingReceived"))
    	catch
    	end try
    	try
    		FundingId = Convert.ToInt32(dr("FundingId"))
    	catch
    	end try
	End Sub

	Public Property KeyID() As Integer Implements DotNetNuke.Entities.Modules.IHydratable.KeyID
		Get
			Return _fundingId
		End Get
		Set(ByVal value As Integer)
			_fundingId = Value
		End Set
	End Property             
#End Region

	End Class

	
End Namespace