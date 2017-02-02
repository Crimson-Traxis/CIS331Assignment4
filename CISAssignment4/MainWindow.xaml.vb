Imports System.Net.Http
Imports System.Text.RegularExpressions

Class MainWindow

    Private degreeDictionary As Dictionary(Of Degree, List(Of Course))
    Private courseList As List(Of Course)

    Public Sub New()
        InitializeComponent()
        degreeDictionary = New Dictionary(Of Degree, List(Of Course))
        courseList = New List(Of Course)
        SyncFromOnline()
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

    End Sub

    Private Sub buttonAddCourse_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddCourse.Click
        If textBoxCoursePrefix.Text <> "" Then
            If textBoxCourseName.Text <> "" Then
                Dim cor As Course = New Course(New CourseDescription("", "", ""), textBoxCoursePrefix.Text.ToUpper().Replace(" ", ""), textBoxCourseName.Text)
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

    Public Async Sub SyncFromOnline()
        Dim degreePrefixes As List(Of String) = New List(Of String)
        Dim responseString As String = ""
        Using client As HttpClient = New HttpClient()
            Dim address As Uri = New Uri("http://catalog.svsu.edu/content.php?catoid=20&navoid=529")
            Dim response = Await client.GetAsync(address)
            responseString = Await response.Content.ReadAsStringAsync()
        End Using
        Dim pattern As String = "<option value=""[a-zA-Z]+"">.+</option>"
        Dim r As Regex = New Regex(pattern)
        Dim match As Match = r.Match(responseString)
        While (match.Success)
            Dim seperatedValues As String() = match.Value.Split(New Char() {"<", ">"}, StringSplitOptions.RemoveEmptyEntries)
            degreePrefixes.Add(seperatedValues(1))
            match = match.NextMatch
        End While
        MessageBox.Show(String.Join(",", degreePrefixes))
        Dim firstHalf As String = "http://catalog.svsu.edu/content.php?filter%5B27%5D="
        Dim lastHalf As String = "&filter%5B29%5D=&filter%5Bcourse_type%5D=-1&filter%5Bkeyword%5D=&filter%5B32%5D=1&filter%5Bcpage%5D=1&cur_cat_oid=20&expand=&navoid=529&search_database=Filter&filter%5Bexact_match%5D=1#acalog_template_course_filter"

    End Sub

End Class
