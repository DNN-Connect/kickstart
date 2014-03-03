Namespace Connect.Modules.Kickstart.Entities

	Public Class ParticipantInfo
	
			Implements DotNetNuke.Entities.Modules.IHydratable


#Region "Enums"

        Public Enum ParticipantRole
            Developer = 1
            Designer = 2
            Tester = 3
            Translator = 4
            Manager = 5
            Funding = 6
        End Enum

#End Region

#Region "Private Members"
        Private _ParticipationId As Int32
		Private _projectId as Int32
		Private _userId as Int32
        Private _projectRole As ParticipantRole
        Private _displayname As String
        Private _photourl As String
#End Region
		
#Region "Constructors"
       
 Public Sub New()
        End Sub

#End Region
		
#Region "Public Properties"
        Public Property ParticipationId() As Int32
            Get
                Return _ParticipationId
            End Get
            Set(ByVal Value As Int32)
                _ParticipationId = Value
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

        Public Property ProjectRole() As ParticipantRole
            Get
                Return _projectRole
            End Get
            Set(ByVal Value As ParticipantRole)
                _projectRole = Value
            End Set
        End Property
		
	Public Sub Fill(ByVal dr As System.Data.IDataReader) Implements DotNetNuke.Entities.Modules.IHydratable.Fill

            Try
                ProjectId = Convert.ToInt32(dr("ProjectId"))
            Catch
            End Try
    
        
    	try					
    		UserId = Convert.ToInt32(dr("UserId"))
    	catch
    	end try
    
        
    	try					
    		ProjectRole = Convert.ToInt32(dr("ProjectRole"))
    	catch
    	end try
    	try
                ParticipationId = Convert.ToInt32(dr("ParticipationId"))
    	catch
    	end try
	End Sub

	Public Property KeyID() As Integer Implements DotNetNuke.Entities.Modules.IHydratable.KeyID
		Get
                Return _ParticipationId
		End Get
		Set(ByVal value As Integer)
                _ParticipationId = value
		End Set
	End Property             
#End Region

#Region "Hydrated Properties"

        Public Property PhotoUrl As String
            Get
                Return _photourl
            End Get
            Set(Value As String)
                _photourl = Value
            End Set
        End Property

        Public Property Displayname As String
            Get
                Return _displayname
            End Get
            Set(Value As String)
                _displayname = Value
            End Set
        End Property

#End Region
	End Class

	
End Namespace