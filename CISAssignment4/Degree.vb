Public Class Degree

    Private _degreePrefix As String
    Private _degreeName As String

    Public Sub New(degreePrefix As String, degreeName As String)
        _degreePrefix = degreePrefix
        _degreeName = degreeName
    End Sub

    Public Property DegreePrefix() As String
        Get
            Return _degreePrefix
        End Get
        Set(ByVal value As String)
            _degreePrefix = value
        End Set
    End Property

    Public Property DegreeName() As String
        Get
            Return _degreeName
        End Get
        Set(ByVal value As String)
            _degreeName = value
        End Set
    End Property
End Class
