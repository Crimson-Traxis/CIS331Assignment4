Public Class ListViewDegreeControl

    Private _degree As Degree

    Public Sub New()
        InitializeComponent()
        _degree = New Degree("Unknown", "Unknown")
        textBlockDegree.Text = _degree.DegreePrefix + " - " + _degree.DegreeName
    End Sub

    Public Sub New(degree As Degree)
        InitializeComponent()
        _degree = degree
        textBlockDegree.Text = _degree.DegreePrefix + " - " + _degree.DegreeName
    End Sub

    Public Property Degree() As Degree
        Get
            Return _degree
        End Get
        Set(ByVal value As Degree)
            _degree = value
        End Set
    End Property
End Class
