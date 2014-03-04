Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security.Permissions
Imports Connect.Modules.Kickstart.Entities
Imports Connect.Modules.Kickstart.Entities.ParticipantInfo


Namespace Connect.Modules.Kickstart

    Public Class KickstartModuleBase
        Inherits PortalModuleBase


        Private _projectid As Integer = Null.NullInteger
        Private _project As ProjectInfo
        Private _config As ProjectConfigInfo = Nothing

        Public ReadOnly Property KickstartSettings As KickstartSettings
            Get
                Dim objKickstartSettings As KickstartSettings = New KickstartSettings(Me.ModuleConfiguration)
                Return objKickstartSettings
            End Get
        End Property
        Public ReadOnly Property Config As ProjectConfigInfo
            Get
                Return _config
            End Get
        End Property

        Public ReadOnly Property ProjectId As Integer
            Get
                Return _projectid
            End Get
        End Property

        Public ReadOnly Property Project As ProjectInfo
            Get
                Return _project
            End Get
        End Property

        Public ReadOnly Property ActionMode As String
            Get

                If Not Request.QueryString("Action") Is Nothing Then
                    Return Request.QueryString("Action")
                End If

                Return ""

            End Get
        End Property

        Public ReadOnly Property ViewMode As String
            Get

                If Settings.Contains("Kickstart_ViewMode") Then
                    Return Settings("Kickstart_ViewMode").ToString
                End If

                Return ""

            End Get
        End Property

        Public ReadOnly Property ModuleTheme As String
            Get

                If Settings.Contains("Kickstart_ModuleTheme") Then
                    Return Settings("Kickstart_ModuleTheme").ToString
                End If

                Return "Default"

            End Get
        End Property

        Public Sub ReadQuerystring()

            If Not Request.QueryString("ProjectId") Is Nothing Then
                Try
                    _projectid = Integer.Parse(Request.QueryString("ProjectId"))
                    _project = ProjectController.Get(_projectid)


                    If _projectid <> Null.NullInteger Then
                        _config = ConfigController.GetConfig(_projectid)
                    End If

                Catch
                End Try
            End If

        End Sub

        Protected Function IsProjectLead() As Boolean

            If Not Project Is Nothing Then
                Return (Project.LeadBy = UserId)
            End If

            Return False

        End Function

        Public Function HasProjectLead() As Boolean

            If Not Project Is Nothing Then
                Return (Project.LeadBy <> Null.NullInteger)
            End If

            Return False

        End Function

        Public Function GetParticipantList(Role As ParticipantRole, Hydrate As Boolean) As List(Of ParticipantInfo)

            Dim lst As New List(Of ParticipantInfo)

            If Project Is Nothing Then
                Return lst
            End If

            For Each participant As ParticipantInfo In ParticipantController.ListByProject(ProjectId)

                Select Case Role
                    Case ParticipantRole.Designer
                        If participant.ProjectRole = ParticipantRole.Designer Then
                            lst.Add(participant)
                        End If
                    Case ParticipantRole.Developer
                        If participant.ProjectRole = ParticipantRole.Developer Then
                            lst.Add(participant)
                        End If
                    Case ParticipantRole.Funding
                        If participant.ProjectRole = ParticipantRole.Funding Then
                            lst.Add(participant)
                        End If
                    Case ParticipantRole.Manager
                        If participant.ProjectRole = ParticipantRole.Manager Then
                            lst.Add(participant)
                        End If
                    Case ParticipantRole.Tester
                        If participant.ProjectRole = ParticipantRole.Tester Then
                            lst.Add(participant)
                        End If
                    Case ParticipantRole.Translator
                        If participant.ProjectRole = ParticipantRole.Translator Then
                            lst.Add(participant)
                        End If
                End Select

            Next

            If Hydrate Then
                For Each p As ParticipantInfo In lst

                    Dim u As UserInfo = UserController.GetUserById(PortalId, p.UserId)
                    If Not u Is Nothing Then
                        p.Displayname = u.DisplayName
                        p.PhotoUrl = u.Profile.PhotoURL
                    End If

                Next
            End If

            Return lst

        End Function

        Public Function IsFunding() As Boolean
            Dim blnIsFunding As Boolean = False
            Dim lstFunding As New List(Of FundingInfo)
            lstFunding = FundingController.ListByProject(ProjectId)
            For Each objFunding As FundingInfo In lstFunding
                If objFunding.UserId = UserId Then
                    blnIsFunding = True
                    Exit For
                End If
            Next
            Return blnIsFunding
        End Function

        Public Function MyFundingAsString() As String

            Dim lstFunding As New List(Of FundingInfo)
            lstFunding = FundingController.ListByProject(ProjectId)
            For Each objFunding As FundingInfo In lstFunding
                If objFunding.UserId = UserId Then
                    Return Utilities.FormatFunding(objFunding)
                End If
            Next

            Return ""

        End Function

        Public Function MyFunding() As FundingInfo

            Dim lstFunding As New List(Of FundingInfo)
            lstFunding = FundingController.ListByProject(ProjectId)
            For Each objFunding As FundingInfo In lstFunding
                If objFunding.UserId = UserId Then
                    Return objFunding
                End If
            Next

            Return Nothing

        End Function

        Public Sub HideModule()

            'transverse the oControls parent objects until we can locate 
            'the parent container (ascx file) 
            Dim o As Control = Me 'the module object to hide/show 
            Do
                o = o.Parent
                If TypeOf o Is Page Then
                    'never found an ascx parent control 
                    'we're at the page so we're done 
                    Exit Do
                End If
                Try
                    'if the name of the parent object is an ascx file then 
                    'this must be the parent container (we hope!) 
                    If o.ToString.IndexOf("_ascx") <> -1 Then
                        o.Visible = False 'set to hide/show the container of oControl 
                        Exit Do
                    End If
                Catch
                End Try

            Loop

        End Sub

        Public Sub RefreshPage()

            Response.Redirect(NavigateURL(TabId, "", "ProjectId=" & ProjectId.ToString))

        End Sub

        Public Function ProjectListUrl() As String

            Return NavigateURL(KickstartSettings.ProjectListTabId)

        End Function

        Public Function ProjectUrl(ProjectId As Integer) As String

            Return NavigateURL(KickstartSettings.ProjectDetailsTabId, "", "ProjectId=" & ProjectId)

        End Function

    End Class

End Namespace

