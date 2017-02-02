Imports System.Net.Http
Imports System.Text.RegularExpressions

Class MainWindow

    Private degreeDictionary As Dictionary(Of Degree, List(Of Course))
    Private courseList As List(Of Course)

    Public Sub New()
        InitializeComponent()
        degreeDictionary = New Dictionary(Of Degree, List(Of Course))
        courseList = New List(Of Course)
    End Sub

    Private Sub buttonBuildDegrees_Click(sender As Object, e As RoutedEventArgs) Handles buttonBuildDegrees.Click
        Dim builder As DegreeBuilder = New DegreeBuilder(degreeDictionary, courseList)
        builder.Show()
    End Sub

    Private Sub buttonAddDegree_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddDegree.Click
        If textBoxDegreePrefix.Text <> "" Then
            If textBoxDegreeName.Text <> "" Then
                Dim deg As Degree = New Degree(textBoxDegreePrefix.Text.ToUpper(), textBoxDegreeName.Text)
                degreeDictionary.Add(deg, New List(Of Course))
                listViewDegrees.Items.Add(New ListViewDegreeControl(deg))
                textBoxDegreeName.Text = ""
                textBoxDegreePrefix.Text = ""
            Else
                MessageBox.Show("Degree name must not be left blank.")
            End If
        Else
            MessageBox.Show("Degree Prefix must not be left blank.")
        End If
    End Sub

    Private Sub buttonDeleteDegree_Click(sender As Object, e As RoutedEventArgs) Handles buttonDeleteDegree.Click
        Dim deleteList As List(Of ListViewDegreeControl) = New List(Of ListViewDegreeControl)
        For Each degreeControl As ListViewDegreeControl In listViewDegrees.SelectedItems
            deleteList.Add(degreeControl)
            degreeDictionary.Remove(degreeControl.Degree)
        Next
        For Each degreeControl As ListViewDegreeControl In deleteList
            listViewDegrees.Items.Remove(degreeControl)
        Next
    End Sub

    Private Sub buttonImportDegree_Click(sender As Object, e As RoutedEventArgs) Handles buttonImportDegree.Click
        Dim degreeWind As ImportDegrees = New ImportDegrees()
        degreeWind.ShowDialog()
        Dim degreesNotAdded As List(Of String) = New List(Of String)
        Dim degreesInDictionary As List(Of String) = New List(Of String)
        For Each degree In degreeDictionary
            degreesInDictionary.Add(degree.Key.DegreePrefix)
        Next
        For Each degree As Degree In degreeWind.DegreeList
            If degreesInDictionary.Contains(degree.DegreePrefix) Then
                degreesNotAdded.Add(degree.DegreePrefix + " - " + degree.DegreeName)
            Else
                degreeDictionary.Add(degree, New List(Of Course))
                listViewDegrees.Items.Add(New ListViewDegreeControl(degree))
            End If
        Next
        If degreesNotAdded.Count > 0 Then
            MessageBox.Show("The folowing degrees were not added becuase they already existed." + Environment.NewLine + String.Join(Environment.NewLine, degreesNotAdded))
        End If
    End Sub

    Private Sub buttonAddCourse_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddCourse.Click
        If textBoxCoursePrefix.Text <> "" Then
            If textBoxCourseName.Text <> "" Then
                Dim cor As Course = New Course(New CourseDescription("", "", "0"), textBoxCoursePrefix.Text.ToUpper().Replace(" ", ""), textBoxCourseName.Text)
                courseList.Add(cor)
                listViewCourses.Items.Add(New ListViewCourseControl(cor, False))
                textBoxCourseName.Text = ""
                textBoxCoursePrefix.Text = ""
            Else
                MessageBox.Show("Course name must not be left blank.")
            End If
        Else
            MessageBox.Show("Course Prefix must not be left blank.")
        End If
    End Sub

    Private Sub buttonDeleteCourse_Click(sender As Object, e As RoutedEventArgs) Handles buttonDeleteCourse.Click
        Dim deleteList As List(Of ListViewCourseControl) = New List(Of ListViewCourseControl)
        For Each course As ListViewCourseControl In listViewCourses.SelectedItems
            deleteList.Add(course)
            courseList.Remove(course.Course)
        Next
        For Each course As ListViewCourseControl In deleteList
            listViewCourses.Items.Remove(course)
        Next
    End Sub

    Private Sub buttonImportCourse_Click(sender As Object, e As RoutedEventArgs) Handles buttonImportCourse.Click
        Dim importCourseWind As ImportCourses = New ImportCourses()
        importCourseWind.ShowDialog()
    End Sub

    Private Sub textBoxDegreePrefix_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxDegreePrefix.TextChanged
        For Each degree As Degree In degreeDictionary.Keys
            If degree.DegreePrefix = textBoxDegreePrefix.Text.ToUpper() Then
                buttonAddDegree.IsEnabled = False
                Exit For
            Else
                buttonAddDegree.IsEnabled = True
            End If
        Next
    End Sub

    Private Sub textBoxCoursePrefix_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxCoursePrefix.TextChanged
        For Each course As Course In courseList
            If course.CoursePrefix = textBoxCoursePrefix.Text.ToUpper() Then
                buttonAddCourse.IsEnabled = False
                Exit For
            Else
                buttonAddCourse.IsEnabled = True
            End If
        Next
    End Sub

    Public Sub RefreshGui()
        listViewDegrees.Items.Clear()
        listViewCourses.Items.Clear()
        For Each degree As Degree In degreeDictionary.Keys
            listViewDegrees.Items.Add(New ListViewDegreeControl(degree))
        Next
        For Each course As Course In courseList
            listViewCourses.Items.Add(course)
        Next
    End Sub



End Class
