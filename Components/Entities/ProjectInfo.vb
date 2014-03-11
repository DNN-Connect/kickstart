Namespace Connect.Modules.Kickstart.Entities

    Public Class ProjectInfo

        Implements DotNetNuke.Entities.Modules.IHydratable

#Region "Private Members"
        Private _projectId As Int32 = Null.NullInteger
        Private _moduleId As Int32 = Null.NullInteger
        Private _contentItemId As Int32 = 0
        Private _status As Int32 = 1
        Private _subject As String = Null.NullString
        Private _summary As String = Null.NullString
        Private _content As String = Null.NullString
        Private _projectUrl As String = Null.NullString
        Private _projectPlatform As String = Null.NullString
        Private _platformRssUrl As String = Null.NullString
        Private _dateScheduled As DateTime = Null.NullDate
        Private _dateDelivered As DateTime = Null.NullDate
        Private _dateCreated As DateTime = Null.NullDate
        Private _dateLocked As DateTime = Null.NullDate
        Private _dateDeleted As DateTime = Null.NullDate
        Private _createdBy As Int32 = Null.NullInteger
        Private _lockedBy As Int32 = Null.NullInteger
        Private _deletedBy As Int32 = Null.NullInteger
        Private _leadBy As Int32 = Null.NullInteger
        Private _isVisible As Boolean = False
        Private _isLocked As Boolean = False
        Private _isDeleted As Boolean = False
        Private _isDelivered As Boolean = False
        Private _views As Int32 = 0
        Private _comments As Int32 = 0
        Private _votes As Int32 = 0
        Private _teamMembers As Int32 = 0
#End Region

#Region "Constructors"

        Public Sub New()
        End Sub

#End Region

#Region "Public Properties"
        Public Property ProjectId() As Int32
            Get
                Return _projectId
            End Get
            Set(ByVal Value As Int32)
                _projectId = Value
            End Set
        End Property



        Public Property ModuleId() As Int32
            Get
                Return _moduleId
            End Get
            Set(ByVal Value As Int32)
                _moduleId = Value
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



        Public Property Status() As Int32
            Get
                Return _status
            End Get
            Set(ByVal Value As Int32)
                _status = Value
            End Set
        End Property



        Public Property Subject() As String
            Get
                Return _subject
            End Get
            Set(ByVal Value As String)
                _subject = Value
            End Set
        End Property



        Public Property Summary() As String
            Get
                Return _summary
            End Get
            Set(ByVal Value As String)
                _summary = Value
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



        Public Property ProjectUrl() As String
            Get
                Return _projectUrl
            End Get
            Set(ByVal Value As String)
                _projectUrl = Value
            End Set
        End Property



        Public Property ProjectPlatform() As String
            Get
                Return _projectPlatform
            End Get
            Set(ByVal Value As String)
                _projectPlatform = Value
            End Set
        End Property



        Public Property PlatformRssUrl() As String
            Get
                Return _platformRssUrl
            End Get
            Set(ByVal Value As String)
                _platformRssUrl = Value
            End Set
        End Property



        Public Property DateScheduled() As DateTime
            Get
                Return _dateScheduled
            End Get
            Set(ByVal Value As DateTime)
                _dateScheduled = Value
            End Set
        End Property



        Public Property DateDelivered() As DateTime
            Get
                Return _dateDelivered
            End Get
            Set(ByVal Value As DateTime)
                _dateDelivered = Value
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



        Public Property DateLocked() As DateTime
            Get
                Return _dateLocked
            End Get
            Set(ByVal Value As DateTime)
                _dateLocked = Value
            End Set
        End Property



        Public Property DateDeleted() As DateTime
            Get
                Return _dateDeleted
            End Get
            Set(ByVal Value As DateTime)
                _dateDeleted = Value
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



        Public Property LockedBy() As Int32
            Get
                Return _lockedBy
            End Get
            Set(ByVal Value As Int32)
                _lockedBy = Value
            End Set
        End Property



        Public Property DeletedBy() As Int32
            Get
                Return _deletedBy
            End Get
            Set(ByVal Value As Int32)
                _deletedBy = Value
            End Set
        End Property



        Public Property LeadBy() As Int32
            Get
                Return _leadBy
            End Get
            Set(ByVal Value As Int32)
                _leadBy = Value
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



        Public Property IsLocked() As Boolean
            Get
                Return _isLocked
            End Get
            Set(ByVal Value As Boolean)
                _isLocked = Value
            End Set
        End Property



        Public Property IsDeleted() As Boolean
            Get
                Return _isDeleted
            End Get
            Set(ByVal Value As Boolean)
                _isDeleted = Value
            End Set
        End Property



        Public Property IsDelivered() As Boolean
            Get
                Return _isDelivered
            End Get
            Set(ByVal Value As Boolean)
                _isDelivered = Value
            End Set
        End Property



        Public Property Views() As Int32
            Get
                Return _views
            End Get
            Set(ByVal Value As Int32)
                _views = Value
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

        Public Property Votes() As Int32
            Get
                Return _votes
            End Get
            Set(ByVal Value As Int32)
                _votes = Value
            End Set
        End Property

        Public Property TeamMembers() As Int32
            Get
                Return _teamMembers
            End Get
            Set(ByVal Value As Int32)
                _teamMembers = Value
            End Set
        End Property

        Public Sub Fill(ByVal dr As System.Data.IDataReader) Implements DotNetNuke.Entities.Modules.IHydratable.Fill


            Try
                ModuleId = Convert.ToInt32(dr("ModuleId"))
            Catch
            End Try


            Try
                ContentItemId = Convert.ToInt32(dr("ContentItemId"))
            Catch
            End Try


            Try
                Status = Convert.ToInt32(dr("Status"))
            Catch
            End Try


            Try
                Subject = Convert.ToString(dr("Subject"))
            Catch
            End Try


            Try
                Summary = Convert.ToString(dr("Summary"))
            Catch
            End Try


            Try
                Content = Convert.ToString(dr("Content"))
            Catch
            End Try


            Try
                ProjectUrl = Convert.ToString(dr("ProjectUrl"))
            Catch
            End Try


            Try
                ProjectPlatform = Convert.ToString(dr("ProjectPlatform"))
            Catch
            End Try


            Try
                PlatformRssUrl = Convert.ToString(dr("PlatformRssUrl"))
            Catch
            End Try


            Try
                DateScheduled = Convert.ToDateTime(dr("DateScheduled"))
            Catch
            End Try


            Try
                DateDelivered = Convert.ToDateTime(dr("DateDelivered"))
            Catch
            End Try


            Try
                DateCreated = Convert.ToDateTime(dr("DateCreated"))
            Catch
            End Try


            Try
                DateLocked = Convert.ToDateTime(dr("DateLocked"))
            Catch
            End Try


            Try
                DateDeleted = Convert.ToDateTime(dr("DateDeleted"))
            Catch
            End Try


            Try
                CreatedBy = Convert.ToInt32(dr("CreatedBy"))
            Catch
            End Try


            Try
                LockedBy = Convert.ToInt32(dr("LockedBy"))
            Catch
            End Try


            Try
                DeletedBy = Convert.ToInt32(dr("DeletedBy"))
            Catch
            End Try


            Try
                LeadBy = Convert.ToInt32(dr("LeadBy"))
            Catch
            End Try


            Try
                IsVisible = Convert.ToBoolean(dr("IsVisible"))
            Catch
            End Try


            Try
                IsLocked = Convert.ToBoolean(dr("IsLocked"))
            Catch
            End Try


            Try
                IsDeleted = Convert.ToBoolean(dr("IsDeleted"))
            Catch
            End Try


            Try
                IsDelivered = Convert.ToBoolean(dr("IsDelivered"))
            Catch
            End Try


            Try
                Views = Convert.ToInt32(dr("Views"))
            Catch
            End Try


            Try
                Comments = Convert.ToInt32(dr("Comments"))
            Catch
            End Try


            Try
                Votes = Convert.ToInt32(dr("Votes"))
            Catch
            End Try


            Try
                TeamMembers = Convert.ToInt32(dr("TeamMembers"))
            Catch
            End Try
            Try
                ProjectId = Convert.ToInt32(dr("ProjectId"))
            Catch
            End Try
        End Sub

        Public Property KeyID() As Integer Implements DotNetNuke.Entities.Modules.IHydratable.KeyID
            Get
                Return _projectId
            End Get
            Set(ByVal value As Integer)
                _projectId = Value
            End Set
        End Property
#End Region

    End Class


End Namespace