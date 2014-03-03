Namespace Connect.Modules.Kickstart.Entities

	Public Class ProjectConfigInfo
	
        Private _managersneeded As Integer
        Private _developersneeded As Integer
        Private _designersneeded As Integer
        Private _translatorsneeded As Integer
        Private _testersneeded As Integer
        Private _fundingneeded As Decimal
        Private _fundingcurrency As String
        Private _initializedonly As Boolean
        Private _ideatitle As String
        Private _ideasummary As String
        Private _ideadescription As String

        Public Sub New()
            _initializedonly = True
            _managersneeded = 1
            _developersneeded = 0
            _designersneeded = 0
            _translatorsneeded = 0
            _testersneeded = 0
            _fundingneeded = CDec(0.0)
            _fundingcurrency = "USD"
            _ideatitle = ""
            _ideasummary = ""
            _ideadescription = ""
        End Sub

        Public Property InitializedOnly As Boolean
            Get
                Return _initializedonly
            End Get
            Set(Value As Boolean)
                _initializedonly = Value
            End Set
        End Property

        Public Property IdeaTitle As String
            Get
                Return _ideatitle
            End Get
            Set(Value As String)
                _ideatitle = Value
            End Set
        End Property

        Public Property IdeaSummary As String
            Get
                Return _ideasummary
            End Get
            Set(Value As String)
                _ideasummary = Value
            End Set
        End Property

        Public Property IdeaDescription As String
            Get
                Return _ideadescription
            End Get
            Set(Value As String)
                _ideadescription = Value
            End Set
        End Property

        Public Property FundingCurrency As String
            Get
                Return _fundingcurrency
            End Get
            Set(Value As String)
                _fundingcurrency = Value
            End Set
        End Property

        Public Property ManagersNeeded As Integer
            Get
                Return _managersneeded
            End Get
            Set(Value As Integer)
                _managersneeded = Value
            End Set
        End Property

        Public Property DevelopersNeeded As Integer
            Get
                Return _developersneeded
            End Get
            Set(Value As Integer)
                _developersneeded = Value
            End Set
        End Property

        Public Property DesignersNeeded As Integer
            Get
                Return _designersneeded
            End Get
            Set(Value As Integer)
                _designersneeded = Value
            End Set
        End Property

        Public Property TranslatorsNeeded As Integer
            Get
                Return _translatorsneeded
            End Get
            Set(Value As Integer)
                _translatorsneeded = Value
            End Set
        End Property

        Public Property TestersNeeded As Integer
            Get
                Return _testersneeded
            End Get
            Set(Value As Integer)
                _testersneeded = Value
            End Set
        End Property

        Public Property FundingNeeded As Decimal
            Get
                Return _fundingneeded
            End Get
            Set(Value As Decimal)
                _fundingneeded = Value
            End Set
        End Property

        Public ReadOnly Property NeedsFunding As Boolean
            Get
                Return FundingNeeded > 0.0
            End Get
        End Property

        Public ReadOnly Property NeedsDevelopers As Boolean
            Get
                Return DevelopersNeeded > 0
            End Get
        End Property

        Public ReadOnly Property NeedsManagers As Boolean
            Get
                Return ManagersNeeded > 0
            End Get
        End Property

        Public ReadOnly Property NeedsDesigners As Boolean
            Get
                Return DesignersNeeded > 0
            End Get
        End Property

        Public ReadOnly Property NeedsTranslators As Boolean
            Get
                Return TranslatorsNeeded > 0
            End Get
        End Property

        Public ReadOnly Property NeedsTesters As Boolean
            Get
                Return TestersNeeded > 0
            End Get
        End Property

	End Class

	
End Namespace