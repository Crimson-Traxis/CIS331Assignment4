'------------------------------------------------------------ 
'-                File Name : Degree.vb                     - 
'-                Part of Project: Assignment 4             - 
'------------------------------------------------------------
'-                Written By: Trent Killinger               - 
'-                Written On: 2-1-17                        - 
'------------------------------------------------------------ 
'- File Purpose:                                            - 
'-                                                          - 
'- This file contains degree class definition               -
'------------------------------------------------------------
'- Variable Dictionary                                      - 
'- _degreePrefix - degree prefix                            -
'- _degreeName - degree name                                -
'------------------------------------------------------------
Public Class Degree

    Private _degreePrefix As String
    Private _degreeName As String

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
    '- degreePrefix - degree prefix                             -
    '- degreeName - degree name                                 -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New(degreePrefix As String, degreeName As String)
        _degreePrefix = degreePrefix
        _degreeName = degreeName
    End Sub

    '------------------------------------------------------------ 
    '-                Property Name: DegreePrefix               - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property sets/gets the DegreePrefix                 -
    '------------------------------------------------------------ 
    Public Property DegreePrefix() As String
        Get
            Return _degreePrefix
        End Get
        Set(ByVal value As String)
            _degreePrefix = value
        End Set
    End Property

    '------------------------------------------------------------ 
    '-                Property Name: DegreeName                 - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property sets/gets the DegreeName                   -
    '------------------------------------------------------------ 
    Public Property DegreeName() As String
        Get
            Return _degreeName
        End Get
        Set(ByVal value As String)
            _degreeName = value
        End Set
    End Property
End Class
