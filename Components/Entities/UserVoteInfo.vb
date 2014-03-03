Namespace Connect.Modules.Kickstart.Entities

	Public Class UserVoteInfo
	
			Implements DotNetNuke.Entities.Modules.IHydratable
			
#Region "Private Members"
		Private _voteId as Int32
		Private _commentId as Int32
		Private _projectId as Int32
		Private _userId as Int32
		Private _votes as Int32
#End Region
		
#Region "Constructors"

        Public Sub New()
        End Sub

        Public Sub New(userid As Integer, projectid As Integer, commentid As Integer)
            _userId = userid
            _projectId = projectid
            _commentId = commentid
            _votes = 1
        End Sub

        Public Sub New(userid As Integer, projectid As Integer)
            _userId = userid
            _projectId = projectid
            _commentId = Null.NullInteger
            _votes = 1
        End Sub

#End Region
		
#Region "Public Properties"
		Public Property VoteId() as Int32
			Get
				Return _voteId
			End Get
			Set(ByVal Value as Int32)
				_voteId = Value
			End Set
		End Property
		
        
        
		Public Property CommentId() as Int32 
			Get
				Return _commentId
			End Get
			Set(ByVal Value as Int32)
				_commentId = Value
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

        
        
		Public Property Votes() as Int32 
			Get
				Return _votes
			End Get
			Set(ByVal Value as Int32)
				_votes = Value
			End Set
		End Property
		
	Public Sub Fill(ByVal dr As System.Data.IDataReader) Implements DotNetNuke.Entities.Modules.IHydratable.Fill
    
        
    	try					
    		CommentId = Convert.ToInt32(dr("CommentId"))
    	catch
    	end try
    
        
    	try					
    		ProjectId = Convert.ToInt32(dr("ProjectId"))
    	catch
    	end try
    
        
    	try					
    		UserId = Convert.ToInt32(dr("UserId"))
    	catch
    	end try
    
        
    	try					
    		Votes = Convert.ToInt32(dr("Votes"))
    	catch
    	end try
    	try
    		VoteId = Convert.ToInt32(dr("VoteId"))
    	catch
    	end try
	End Sub

	Public Property KeyID() As Integer Implements DotNetNuke.Entities.Modules.IHydratable.KeyID
		Get
			Return _voteId
		End Get
		Set(ByVal value As Integer)
			_voteId = Value
		End Set
	End Property             
#End Region

	End Class

	
End Namespace