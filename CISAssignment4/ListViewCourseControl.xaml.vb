Public Class ListViewCourseControl

    Private _course As Course
    Public Sub New()
        InitializeComponent()
        _course = New Course()
    End Sub

    Public Sub New(course As Course, showInfoButton As Boolean)
        InitializeComponent()
        _course = course
        CoursePrefix = _course.CoursePrefix
        CourseName = _course.CourseName
        If Not showInfoButton Then
            buttonInfo.Visibility = Visibility.Collapsed
        End If
    End Sub

    Private Sub buttonInfo_Click(sender As Object, e As RoutedEventArgs) Handles buttonInfo.Click
        Dim detailsWind As CourseDetails = New CourseDetails(textBlockCourse.Text, _course.Description)
        detailsWind.ShowDialog()
    End Sub

    Public Property Course() As Course
        Get
            Return _course
        End Get
        Set(ByVal value As Course)
            _course = value
        End Set
    End Property

    Public Property CoursePrefix() As String
        Get
            Return _course.CoursePrefix
        End Get
        Set(ByVal value As String)
            _course.CoursePrefix = value
            textBlockCourse.Text = _course.CoursePrefix + " - " + _course.CourseName
        End Set
    End Property

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
