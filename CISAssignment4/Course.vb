Public Class Course

    Private _description As CourseDescription
    Private _coursePrefix As String
    Private _courseName As String

    Public Sub New()
        _description = New CourseDescription("", "", "")
        _coursePrefix = "Unknown"
        _courseName = "Unkown"
    End Sub

    Public Sub New(description As CourseDescription, coursePrefix As String, courseName As String)
        _description = description
        _coursePrefix = coursePrefix
        _courseName = courseName
    End Sub

    Public Property Description() As CourseDescription
        Get
            Return _description
        End Get
        Set(ByVal value As CourseDescription)
            _description = value
        End Set
    End Property

    Public Property CoursePrefix() As String
        Get
            Return _coursePrefix
        End Get
        Set(ByVal value As String)
            _coursePrefix = value
        End Set
    End Property

    Public Property CourseName() As String
        Get
            Return _courseName
        End Get
        Set(ByVal value As String)
            _courseName = value
        End Set
    End Property
End Class
