Public Class CourseDescription

    Private _description As String
    Private _prerequisite As String
    Private _credits As String

    Public Sub New(description As String, prerequisite As String, credits As String)
        _description = description
        _prerequisite = prerequisite
        _credits = credits
    End Sub

    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Property Credits() As String
        Get
            Return _credits
        End Get
        Set(ByVal value As String)
            _credits = value
        End Set
    End Property

    Public Property Prerequisite() As String
        Get
            Return _prerequisite
        End Get
        Set(ByVal value As String)
            _prerequisite = value
        End Set
    End Property
End Class
