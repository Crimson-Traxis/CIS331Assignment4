'------------------------------------------------------------ 
'-               File Name : CourseDescription.vb           - 
'-                Part of Project: Assignment 4             - 
'------------------------------------------------------------
'-                Written By: Trent Killinger               - 
'-                Written On: 2-1-17                        - 
'------------------------------------------------------------ 
'- File Purpose:                                            - 
'-                                                          - 
'- This file contains class definition for the course       -
'- description                                              -
'------------------------------------------------------------
'- Variable Dictionary                                      - 
'- _description - course description                        -
'- _prerequistie - course prerequisites                     -
'- _credits - course credits amout                          -
'------------------------------------------------------------
Public Class CourseDescription

    Private _description As String
    Private _prerequisite As String
    Private _credits As String

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
    '- description - course description                         -
    '- prerequistie - course prerequisites                      -
    '- credits - course credits amout                           -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New(description As String, prerequisite As String, credits As String)
        _description = description
        _prerequisite = prerequisite
        _credits = credits
    End Sub

    '------------------------------------------------------------ 
    '-                Property Name: Description                - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property sets/gets the Description                  -
    '------------------------------------------------------------
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    '------------------------------------------------------------ 
    '-                Property Name: Credits                    - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property sets/gets the Credits                      -
    '------------------------------------------------------------
    Public Property Credits() As String
        Get
            Return _credits
        End Get
        Set(ByVal value As String)
            _credits = value
        End Set
    End Property

    '------------------------------------------------------------ 
    '-                Property Name: Prerequisite               - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property sets/gets the Prerequisite                 -
    '------------------------------------------------------------
    Public Property Prerequisite() As String
        Get
            Return _prerequisite
        End Get
        Set(ByVal value As String)
            _prerequisite = value
        End Set
    End Property
End Class
