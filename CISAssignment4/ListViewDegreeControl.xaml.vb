'------------------------------------------------------------ 
'-          File Name : ListViewDegreeControl.xaml.vb       - 
'-                Part of Project: Assignment 4             - 
'------------------------------------------------------------
'-                Written By: Trent Killinger               - 
'-                Written On: 2-1-17                        - 
'------------------------------------------------------------ 
'- File Purpose:                                            - 
'-                                                          - 
'- This file contains the degree control that is added      -
'- to the list views                                        -
'------------------------------------------------------------
'- Variable Dictionary                                      - 
'- _degree - inner degree used for removing/adding from     -
'-           degree dictionary.                             -
'------------------------------------------------------------
Public Class ListViewDegreeControl

    Private _degree As Degree

    '------------------------------------------------------------ 
    '-                Subprogram Name: New                      - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine creates the gui and instantiates default -
    '- member data/objects                                      -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- (None)                                                   - 
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New()
        InitializeComponent()
        _degree = New Degree("Unknown", "Unknown")
        textBlockDegree.Text = _degree.DegreePrefix + " - " + _degree.DegreeName
    End Sub

    '------------------------------------------------------------ 
    '-                Subprogram Name: New                      - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine creates the gui and instantiates default -
    '- member data/objects                                      -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- degree - degree object that represents control           - 
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New(degree As Degree)
        InitializeComponent()
        _degree = degree
        textBlockDegree.Text = _degree.DegreePrefix + " - " + _degree.DegreeName
    End Sub

    '------------------------------------------------------------ 
    '-                Property Name: Degree                     - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property sets/gets the Degree                       -
    '------------------------------------------------------------ 
    Public Property Degree() As Degree
        Get
            Return _degree
        End Get
        Set(ByVal value As Degree)
            _degree = value
        End Set
    End Property
End Class
