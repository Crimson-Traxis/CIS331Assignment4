Public Class CourseDetails

    Public Sub New(course As String, description As CourseDescription)

        ' This call is required by the designer.
        InitializeComponent()

        textBlockCourse.Text = course
        textBlockDescription.Text = description.Description
        textBlockPrerequisite.Text = description.Prerequisite
        textBlockCredits.Text = description.Credits
    End Sub

    Private Sub buttonSave_Click(sender As Object, e As RoutedEventArgs) Handles buttonSave.Click

    End Sub
End Class
