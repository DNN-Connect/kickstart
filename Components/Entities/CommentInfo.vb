Namespace Connect.Modules.Kickstart.Entities

    Public Class CommentInfo

        Implements DotNetNuke.Entities.Modules.IHydratable

#Region "Private Members"
        Private _commentId As Int32
        Private _projectId As Int32
        Private _contentItemId As Int32
        Private _parentId As Int32 = Null.NullInteger
        Private _content As String
        Private _isVisible As Boolean
        Private _createdBy As Int32
        Private _dateCreated As DateTime
        Private _votes As Int32
        Private _comments As Int32
        Private _isTeamResponse As Boolean
#End Region

#Region "Constructors"

        Public Sub New()
        End Sub

#End Region

#Region "Public Properties"
        Public Property CommentId() As Int32
            Get
                Return _commentId
            End Get
            Set(ByVal Value As Int32)
                _commentId = Value
            End Set
        End Property



        Public Property ProjectId() As Int32
            Get
                Return _projectId
            End Get
            Set(ByVal Value As Int32)
                _projectId = Value
            End Set
        End Property



        Public Property ContentItemId() As Int32
            Get
                Return _contentItemId
            End Get
            Set(ByVal Value As Int32)
                _contentItemId = Value
            End Set
        End Property



        Public Property ParentId() As Int32
            Get
                Return _parentId
            End Get
            Set(ByVal Value As Int32)
                _parentId = Value
            End Set
        End Property



        Public Property Content() As String
            Get
                Return _content
            End Get
            Set(ByVal Value As String)
                _content = Value
            End Set
        End Property



        Public Property IsVisible() As Boolean
            Get
                Return _isVisible
            End Get
            Set(ByVal Value As Boolean)
                _isVisible = Value
            End Set
        End Property



        Public Property CreatedBy() As Int32
            Get
                Return _createdBy
            End Get
            Set(ByVal Value As Int32)
                _createdBy = Value
            End Set
        End Property



        Public Property DateCreated() As DateTime
            Get
                Return _dateCreated
            End Get
            Set(ByVal Value As DateTime)
                _dateCreated = Value
            End Set
        End Property



        Public Property Votes() As Int32
            Get
                Return _votes
            End Get
            Set(ByVal Value As Int32)
                _votes = Value
            End Set
        End Property



        Public Property Comments() As Int32
            Get
                Return _comments
            End Get
            Set(ByVal Value As Int32)
                _comments = Value
            End Set
        End Property



        Public Property IsTeamResponse() As Boolean
            Get
                Return _isTeamResponse
            End Get
            Set(ByVal Value As Boolean)
                _isTeamResponse = Value
            End Set
        End Property

        Public Sub Fill(ByVal dr As System.Data.IDataReader) Implements DotNetNuke.Entities.Modules.IHydratable.Fill


            Try
                ProjectId = Convert.ToInt32(dr("ProjectId"))
            Catch
            End Try


            Try
                ContentItemId = Convert.ToInt32(dr("ContentItemId"))
            Catch
            End Try


            Try
                ParentId = Convert.ToInt32(dr("ParentId"))
            Catch
            End Try


            Try
                Content = Convert.ToString(dr("Content"))
            Catch
            End Try


            Try
                IsVisible = Convert.ToBoolean(dr("IsVisible"))
            Catch
            End Try


            Try
                CreatedBy = Convert.ToInt32(dr("CreatedBy"))
            Catch
            End Try


            Try
                DateCreated = Convert.ToDateTime(dr("DateCreated"))
            Catch
            End Try


            Try
                Votes = Convert.ToInt32(dr("Votes"))
            Catch
            End Try


            Try
                Comments = Convert.ToInt32(dr("Comments"))
            Catch
            End Try


            Try
                IsTeamResponse = Convert.ToBoolean(dr("IsTeamResponse"))
            Catch
            End Try
            Try
                CommentId = Convert.ToInt32(dr("CommentId"))
            Catch
            End Try
        End Sub

        Public Property KeyID() As Integer Implements DotNetNuke.Entities.Modules.IHydratable.KeyID
            Get
                Return _commentId
            End Get
            Set(ByVal value As Integer)
                _commentId = Value
            End Set
        End Property
#End Region

    End Class


End Namespace