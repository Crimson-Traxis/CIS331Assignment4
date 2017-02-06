Imports System.IO
Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports Microsoft.Win32

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
        builder.ShowDialog()
        RefreshTreeView()
    End Sub

    Private Sub buttonAddDegree_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddDegree.Click
        If textBoxDegreePrefix.Text <> "" Then
            If textBoxDegreeName.Text <> "" Then
                Dim deg As Degree = New Degree(textBoxDegreePrefix.Text.ToUpper(), textBoxDegreeName.Text)
                degreeDictionary.Add(deg, New List(Of Course))
                AddToListViewInOrder(listViewDegrees, New ListViewDegreeControl(deg))
                textBoxDegreeName.Text = ""
                textBoxDegreePrefix.Text = ""
            Else
                MessageBox.Show("Degree name must not be left blank.")
            End If
        Else
            MessageBox.Show("Degree Prefix must not be left blank.")
        End If
        RefreshTreeView()
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
        RefreshTreeView()
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
                AddToListViewInOrder(listViewDegrees, New ListViewDegreeControl(degree))
            End If
        Next
        If degreesNotAdded.Count > 0 Then
            MessageBox.Show("The folowing degrees were not added becuase they already existed." + Environment.NewLine + String.Join(Environment.NewLine, degreesNotAdded))
        End If
        RefreshTreeView()
    End Sub

    Private Sub buttonAddCourse_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddCourse.Click
        If textBoxCoursePrefix.Text <> "" Then
            If textBoxCourseName.Text <> "" Then
                Dim cor As Course = New Course(New CourseDescription("", "", "0"), textBoxCoursePrefix.Text.ToUpper(), textBoxCourseName.Text)
                courseList.Add(cor)
                AddToListViewInOrder(listViewCourses, New ListViewCourseControl(cor, True))
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
        Dim importCourseWind As ImportCourses = New ImportCourses(degreeDictionary)
        importCourseWind.ShowDialog()
        Dim coursesInList As List(Of String) = New List(Of String)
        Dim coursesNotAdded As List(Of String) = New List(Of String)
        For Each course As Course In courseList
            coursesInList.Add(course.CoursePrefix)
        Next
        For Each course As Course In importCourseWind.CourseList
            If Not coursesInList.Contains(course.CoursePrefix) Then
                AddToListViewInOrder(listViewCourses, New ListViewCourseControl(course, True))
                courseList.Add(course)
            Else
                coursesNotAdded.Add(course.CoursePrefix + " - " + course.CourseName)
            End If
        Next
        If coursesNotAdded.Count > 0 Then
            MessageBox.Show("The folowing courses were not added becuase they already existed." + Environment.NewLine + String.Join(Environment.NewLine, coursesNotAdded))
        End If
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

    Public Sub RefreshTreeView()
        treeViewDegrees.Items.Clear()
        For Each degree As KeyValuePair(Of Degree, List(Of Course)) In degreeDictionary
            Dim degreeHeader As TreeViewItem = New TreeViewItem()
            degreeHeader.Header = degree.Key.DegreePrefix + " - " + degree.Key.DegreeName
            For Each course As Course In degree.Value
                Dim courseTreeItem As TreeViewItem = New TreeViewItem()
                courseTreeItem.Header = course.CoursePrefix + " - " + course.CourseName
                degreeHeader.Items.Add(courseTreeItem)
            Next
            AddToTreeInOrder(treeViewDegrees, degreeHeader)
        Next
    End Sub

    Private Sub AddToListViewInOrder(listView As ListView, course As ListViewCourseControl)
        Dim index As Integer = 0
        For Each control As ListViewCourseControl In listView.Items
            If String.Compare(course.Course.CoursePrefix, control.Course.CoursePrefix) < 1 Then
                listView.Items.Insert(index, course)
                Exit Sub
            End If
            index += 1
        Next
        listView.Items.Add(course)
    End Sub

    Private Sub AddToListViewInOrder(listView As ListView, degree As ListViewDegreeControl)
        Dim index As Integer = 0
        For Each control As ListViewDegreeControl In listView.Items
            If String.Compare(degree.Degree.DegreePrefix, control.Degree.DegreePrefix) < 1 Then
                listView.Items.Insert(index, degree)
                Exit Sub
            End If
            index += 1
        Next
        listView.Items.Add(degree)
    End Sub

    Private Sub AddToTreeInOrder(tree As TreeView, degree As TreeViewItem)
        Dim index As Integer = 0
        For Each control As TreeViewItem In tree.Items
            If String.Compare(CStr(degree.Header), CStr(control.Header)) < 1 Then
                tree.Items.Insert(index, degree)
                Exit Sub
            End If
            index += 1
        Next
        tree.Items.Add(degree)
    End Sub

    Private Sub buttonSave_Click(sender As Object, e As RoutedEventArgs) Handles buttonSave.Click
        Dim saveDialog As SaveFileDialog = New SaveFileDialog()
        saveDialog.DefaultExt = ".dcb"
        saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        saveDialog.AddExtension = True
        saveDialog.Title = "Save Degrees"
        If saveDialog.ShowDialog() = True Then
            Using stream As StreamWriter = New StreamWriter(saveDialog.FileName)
                For Each course As Course In courseList
                    stream.WriteLine("course<split/>" + course.CoursePrefix +
                                     "<split/>" + course.CourseName +
                                     "<split/>" + course.Description.Description +
                                     "<split/>" + course.Description.Credits +
                                     "<split/>" + course.Description.Prerequisite)
                Next
                For Each degree As KeyValuePair(Of Degree, List(Of Course)) In degreeDictionary
                    stream.Write("degree<split/>" + degree.Key.DegreePrefix +
                 "<split/>" + degree.Key.DegreeName)
                    Dim courseListAsStrings As List(Of String) = New List(Of String)
                    For Each course As Course In degree.Value
                        courseListAsStrings.Add(course.CoursePrefix)
                    Next
                    stream.Write("<split/>" + String.Join("<split/>", courseListAsStrings) + Environment.NewLine)
                Next
            End Using
        End If
    End Sub

    Private Sub buttonLoad_Click(sender As Object, e As RoutedEventArgs) Handles buttonLoad.Click
        Dim openDialog As OpenFileDialog = New OpenFileDialog()
        openDialog.Title = "Open Degrees(s)"
        openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        If openDialog.ShowDialog() = True Then
            listViewCourses.Items.Clear()
            listViewDegrees.Items.Clear()
            courseList.Clear()
            degreeDictionary.Clear()
            treeViewDegrees.Items.Clear()
            For Each file In openDialog.FileNames
                Using stream As StreamReader = New StreamReader(file)
                    While Not stream.EndOfStream
                        Dim line As String = stream.ReadLine()
                        Select Case line.Split(New String() {"<split/>"}, StringSplitOptions.RemoveEmptyEntries).First()
                            Case "course"
                                Try
                                    Dim splitLine As String() = line.Split(New String() {"<split/>"}, StringSplitOptions.RemoveEmptyEntries)
                                    Dim newCourse As Course = New Course(New CourseDescription(splitLine(3), splitLine(5), splitLine(4)), splitLine(1), splitLine(2))
                                    courseList.Add(newCourse)
                                    AddToListViewInOrder(listViewCourses, New ListViewCourseControl(newCourse, True))
                                Catch ex As Exception

                                End Try
                            Case "degree"
                                Try
                                    Dim splitLine As String() = line.Split(New String() {"<split/>"}, StringSplitOptions.RemoveEmptyEntries)
                                    Dim newDegree As Degree = New Degree(splitLine(1), splitLine(2))
                                    Dim degreeList As List(Of Course) = New List(Of Course)
                                    degreeDictionary.Add(newDegree, degreeList)
                                    listViewDegrees.Items.Add(New ListViewDegreeControl(newDegree))
                                    For index As Integer = 2 To splitLine.Length
                                        For Each course As Course In courseList
                                            If course.CoursePrefix = splitLine(index) Then
                                                degreeList.Add(course)
                                                Exit For
                                            End If
                                        Next
                                    Next
                                Catch ex As Exception

                                End Try

                            Case Else

                        End Select
                    End While
                End Using
            Next
            RefreshTreeView()
        End If
    End Sub
End Class
