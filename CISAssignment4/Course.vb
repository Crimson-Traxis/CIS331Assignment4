'------------------------------------------------------------ 
'-                File Name : Course.vb                     - 
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
'- _coursePrefix - course prefix                            -
'- _courseName - course name                                -
'------------------------------------------------------------
Public Class Course

    Private _description As CourseDescription
    Private _coursePrefix As String
    Private _courseName As String

    '------------------------------------------------------------ 
    '-                Subprogram Name: New                      - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine creates instantiates default member      -
    '- data/objects                                             -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- (none)                                                   -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New()
        _description = New CourseDescription("", "", "")
        _coursePrefix = "Unknown"
        _courseName = "Unkown"
    End Sub

    '------------------------------------------------------------ 
    '-                Subprogram Name: New                      - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine creates instantiates default member      -
    '- data/objects                                             -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- description - course description                         -
    '- coursePrefix - course prefix                             -
    '- courseName - course name                                 -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New(description As CourseDescription, coursePrefix As String, courseName As String)
        _description = description
        _coursePrefix = coursePrefix
        _courseName = courseName
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
    Public Property Description() As CourseDescription
        Get
            Return _description
        End Get
        Set(ByVal value As CourseDescription)
            _description = value
        End Set
    End Property

    '------------------------------------------------------------ 
    '-                Property Name: CoursePrefix               - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property sets/gets the CoursePrefix                 -
    '------------------------------------------------------------
    Public Property CoursePrefix() As String
        Get
            Return _coursePrefix
        End Get
        Set(ByVal value As String)
            _coursePrefix = value
        End Set
    End Property

    '------------------------------------------------------------ 
    '-                Property Name: CourseName                 - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property sets/gets the CourseName                   -
    '------------------------------------------------------------
    Public Property CourseName() As String
        Get
            Return _courseName
        End Get
        Set(ByVal value As String)
            _courseName = value
        End Set
    End Property
End Class
