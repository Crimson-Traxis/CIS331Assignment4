Public Class DegreeComboBoxItem
    Inherits ComboBoxItem

    Private _degree As Degree

    Public Property Degree() As Degree
        Get
            Return _degree
        End Get
        Set(ByVal value As Degree)
            _degree = value
            Content = value.DegreePrefix + " - " + value.DegreeName
        End Set
    End Property
End Class
