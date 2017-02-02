Public Class CourseDetails

    Private _description As CourseDescription

    Public Sub New(course As String, description As CourseDescription)

        ' This call is required by the designer.
        InitializeComponent()
        _description = description
        textBlockCourse.Text = course
        textBlockDescription.Text = description.Description
        textBlockPrerequisite.Text = description.Prerequisite
        textBlockCredits.Text = description.Credits
        buttonSave.Visibility = Visibility.Hidden
    End Sub

    Private Sub buttonSave_Click(sender As Object, e As RoutedEventArgs) Handles buttonSave.Click
        Dim tempInt As Integer = 0
        If Integer.TryParse(textBlockCredits.Text, tempInt) Then
            _description.Credits = textBlockCredits.Text
            _description.Description = textBlockDescription.Text
            _description.Prerequisite = textBlockPrerequisite.Text
            buttonSave.Visibility = Visibility.Hidden
        Else
            MessageBox.Show("Credits must be a numberical value.")
        End If
    End Sub

    Private Sub textBlockDescription_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBlockDescription.TextChanged
        buttonSave.Visibility = Visibility.Visible
    End Sub

    Private Sub textBlockPrerequisite_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBlockPrerequisite.TextChanged
        buttonSave.Visibility = Visibility.Visible
    End Sub

    Private Sub textBlockCredits_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBlockCredits.TextChanged
        buttonSave.Visibility = Visibility.Visible
    End Sub
End Class
