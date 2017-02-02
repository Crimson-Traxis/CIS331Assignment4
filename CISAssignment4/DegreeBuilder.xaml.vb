Public Class DegreeBuilder

    Private _degreeDictionary As Dictionary(Of Degree, List(Of Course))
    Private _courseList As List(Of Course)

    Public Sub New()
        InitializeComponent()
        _degreeDictionary = New Dictionary(Of Degree, List(Of Course))
        _courseList = New List(Of Course)
    End Sub

    Public Sub New(degreeDictionary As Dictionary(Of Degree, List(Of Course)), courseList As List(Of Course))
        InitializeComponent()
        _degreeDictionary = degreeDictionary
        _courseList = courseList
        For Each degree As Degree In degreeDictionary.Keys
            listViewDegrees.Items.Add(New ListViewDegreeControl(degree))
        Next
    End Sub

    Private Sub listViewDegreeCourses_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listViewDegreeCourses.SelectionChanged
        If listViewDegreeCourses.SelectedItems.Count > 0 Then
            buttonMovetoDegree.IsEnabled = True
        Else
            buttonMovetoDegree.IsEnabled = False
        End If
    End Sub

    Private Sub listViewDegrees_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listViewDegrees.SelectionChanged
        Dim coursesInDegree As List(Of Course) = _degreeDictionary.TryGetValue
    End Sub

    Private Sub listViewCourses_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listViewCourses.SelectionChanged
        If listViewCourses.Items.Count > 0 Then
            buttonMovetoCourses.IsEnabled = True
        Else
            buttonMovetoCourses.IsEnabled = False
        End If
    End Sub

    Private Sub buttonMovetoDegree_Click(sender As Object, e As RoutedEventArgs) Handles buttonMovetoDegree.Click

    End Sub

    Private Sub buttonMovetoCourses_Click(sender As Object, e As RoutedEventArgs) Handles buttonMovetoCourses.Click

    End Sub

    Private Sub buttonExit_Click(sender As Object, e As RoutedEventArgs) Handles buttonExit.Click
        Me.Close()
    End Sub
End Class
