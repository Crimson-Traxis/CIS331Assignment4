Public Class DegreeBuilder

    Private _degreeDictionary As Dictionary(Of Degree, List(Of Course))
    Private _courseList As List(Of Course)

    Public Sub New()
        InitializeComponent()
        _degreeDictionary = New Dictionary(Of Degree, List(Of Course))
        _courseList = New List(Of Course)
        buttonMovetoCourses.IsEnabled = False
        buttonMovetoDegree.IsEnabled = False
    End Sub

    Public Sub New(degreeDictionary As Dictionary(Of Degree, List(Of Course)), courseList As List(Of Course))
        InitializeComponent()
        _degreeDictionary = degreeDictionary
        _courseList = courseList
        buttonMovetoCourses.IsEnabled = False
        buttonMovetoDegree.IsEnabled = False
        For Each degree As Degree In degreeDictionary.Keys
            AddInOrderDegree(New ListViewDegreeControl(degree))
        Next
    End Sub

    Private Sub listViewDegreeCourses_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listViewDegreeCourses.SelectionChanged
        If listViewDegreeCourses.SelectedItems.Count > 0 Then
            buttonMovetoCourses.IsEnabled = True
        Else
            buttonMovetoCourses.IsEnabled = False
        End If
    End Sub

    Private Sub listViewDegrees_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listViewDegrees.SelectionChanged
        listViewDegreeCourses.Items.Clear()
        listViewCourses.Items.Clear()
        Dim listViewDegree As ListViewDegreeControl = listViewDegrees.SelectedItem
        Dim degreeCourseList As List(Of Course) = New List(Of Course)
        _degreeDictionary.TryGetValue(listViewDegree.Degree, degreeCourseList)
        For Each course As Course In degreeCourseList
            listViewDegreeCourses.Items.Add(New ListViewCourseControl(course, False))
        Next
        For Each course As Course In _courseList
            If Not degreeCourseList.Contains(course) Then
                listViewCourses.Items.Add(New ListViewCourseControl(course, False))
            End If
        Next
    End Sub

    Private Sub listViewCourses_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listViewCourses.SelectionChanged
        If listViewCourses.Items.Count > 0 Then
            buttonMovetoDegree.IsEnabled = True
        Else
            buttonMovetoDegree.IsEnabled = False
        End If
    End Sub

    Private Sub buttonMovetoDegree_Click(sender As Object, e As RoutedEventArgs) Handles buttonMovetoDegree.Click
        Dim coursestoMove As List(Of ListViewCourseControl) = New List(Of ListViewCourseControl)
        Dim listViewDegree As ListViewDegreeControl = listViewDegrees.SelectedItem
        Dim degreeCourseList As List(Of Course) = New List(Of Course)
        _degreeDictionary.TryGetValue(listViewDegree.Degree, degreeCourseList)
        For Each listViewCourse As ListViewCourseControl In listViewCourses.SelectedItems
            coursestoMove.Add(listViewCourse)
            degreeCourseList.Add(listViewCourse.Course)
        Next
        For Each listViewCourse As ListViewCourseControl In coursestoMove
            listViewCourses.Items.Remove(listViewCourse)
            AddInOrderDegreeCourse(listViewCourse)
        Next
    End Sub

    Private Sub buttonMovetoCourses_Click(sender As Object, e As RoutedEventArgs) Handles buttonMovetoCourses.Click
        Dim coursestoMove As List(Of ListViewCourseControl) = New List(Of ListViewCourseControl)
        Dim listViewDegree As ListViewDegreeControl = listViewDegrees.SelectedItem
        Dim degreeCourseList As List(Of Course) = New List(Of Course)
        _degreeDictionary.TryGetValue(listViewDegree.Degree, degreeCourseList)
        For Each listViewCourse As ListViewCourseControl In listViewDegreeCourses.SelectedItems
            coursestoMove.Add(listViewCourse)
            degreeCourseList.Remove(listViewCourse.Course)
        Next
        For Each listViewCourse As ListViewCourseControl In coursestoMove
            listViewDegreeCourses.Items.Remove(listViewCourse)
            AddInOrderCourse(listViewCourse)
        Next
    End Sub

    Private Sub buttonExit_Click(sender As Object, e As RoutedEventArgs) Handles buttonExit.Click
        Me.Close()
    End Sub

    Private Sub AddInOrderDegreeCourse(courseControl As ListViewCourseControl)
        Dim index As String = 0
        For Each control As ListViewCourseControl In listViewDegreeCourses.Items
            If String.Compare(courseControl.Course.CoursePrefix, control.Course.CoursePrefix) < 1 Then
                listViewDegreeCourses.Items.Insert(index, courseControl)
                Exit Sub
            End If
            index += 1
        Next
        listViewDegreeCourses.Items.Add(courseControl)
    End Sub

    Private Sub AddInOrderCourse(courseControl As ListViewCourseControl)
        Dim index As String = 0
        For Each control As ListViewCourseControl In listViewCourses.Items
            If String.Compare(courseControl.Course.CoursePrefix, control.Course.CoursePrefix) < 1 Then
                listViewCourses.Items.Insert(index, courseControl)
                Exit Sub
            End If
            index += 1
        Next
        listViewCourses.Items.Add(courseControl)
    End Sub

    Private Sub AddInOrderDegree(degreeControl As ListViewDegreeControl)
        Dim index As String = 0
        For Each control As ListViewDegreeControl In listViewDegrees.Items
            If String.Compare(degreeControl.Degree.DegreePrefix, control.Degree.DegreePrefix) < 1 Then
                listViewDegrees.Items.Insert(index, degreeControl)
                Exit Sub
            End If
            index += 1
        Next
        listViewDegrees.Items.Add(degreeControl)
    End Sub
End Class
