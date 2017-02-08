'------------------------------------------------------------ 
'-          File Name : ListViewCourseControl.xaml.vb       - 
'-                Part of Project: Assignment 4             - 
'------------------------------------------------------------
'-                Written By: Trent Killinger               - 
'-                Written On: 2-1-17                        - 
'------------------------------------------------------------ 
'- File Purpose:                                            - 
'-                                                          - 
'- This file contains the course control that is added      -
'- to the list views                                        -
'------------------------------------------------------------
'- Variable Dictionary                                      - 
'- course - inner course used for removing/adding from      -
'-           course list.                                   -
'------------------------------------------------------------
Public Class ListViewCourseControl

    Private _course As Course

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
        _course = New Course()
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
    '- course - course object that represents control           - 
    '- showInfoButton - true to show the info button            -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New(course As Course, showInfoButton As Boolean)
        InitializeComponent()
        _course = course
        CoursePrefix = _course.CoursePrefix
        CourseName = _course.CourseName
        If Not showInfoButton Then
            buttonInfo.Visibility = Visibility.Collapsed
        End If
    End Sub

    '------------------------------------------------------------ 
    '-          Subprogram Name: buttonInfo_Click               - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine shows the window that allows edditing of -
    '- a controls description.                                  -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- detailsWind - detail window                              -
    '------------------------------------------------------------
    Private Sub buttonInfo_Click(sender As Object, e As RoutedEventArgs) Handles buttonInfo.Click
        Dim detailsWind As CourseDetails = New CourseDetails(textBlockCourse.Text, _course.Description)
        detailsWind.ShowDialog()
    End Sub

    '------------------------------------------------------------ 
    '-                Property Name: Course                     - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property sets/gets the Course                       -
    '------------------------------------------------------------ 
    Public Property Course() As Course
        Get
            Return _course
        End Get
        Set(ByVal value As Course)
            _course = value
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
            Return _course.CoursePrefix
        End Get
        Set(ByVal value As String)
            _course.CoursePrefix = value
            textBlockCourse.Text = _course.CoursePrefix + " - " + _course.CourseName
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
            Return _course.CourseName
        End Get
        Set(ByVal value As String)
            _course.CourseName = value
            textBlockCourse.Text = _course.CoursePrefix + " - " + _course.CourseName
        End Set
    End Property
End Class
