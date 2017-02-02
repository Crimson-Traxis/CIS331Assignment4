Public Class ImportCourses
    Private Sub comboBoxDegrees_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxDegrees.SelectionChanged
        rowProgressBar.Height = New GridLength(35)
    End Sub
End Class
