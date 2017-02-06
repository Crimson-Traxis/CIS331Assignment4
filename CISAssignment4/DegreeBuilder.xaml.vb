﻿Public Class DegreeBuilder

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
            AddToListViewInOrder(listViewDegrees, New ListViewDegreeControl(degree))
        Next
    End Sub

    Private Sub listViewDegreeCourses_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listViewDegreeCourses.SelectionChanged
        ClearCourseDescriptionLayout()
        If listViewDegreeCourses.SelectedItems.Count > 0 Then
            buttonMovetoCourses.IsEnabled = True
        Else
            buttonMovetoCourses.IsEnabled = False
        End If
        If listViewDegreeCourses.SelectedItems.Count = 1 Then
            Dim courseControl As ListViewCourseControl = listViewDegreeCourses.SelectedItem
            UpdateCourseDescriptionLayout(courseControl.Course)
        End If

    End Sub

    Private Sub UpdateCourseDescriptionLayout(course As Course)
        textBlockCourse.Text = course.CoursePrefix + " - " + course.CourseName
        textBlockCredits.Text = course.Description.Credits + " cr"
        textBlockCreditsLabel.Visibility = Visibility.Visible
        textBlockDescription.Text = course.Description.Description
        textBlockPrerequisite.Text = course.Description.Prerequisite
        textBlockPrerequisiteLabel.Visibility = Visibility.Visible
    End Sub

    Private Sub ClearCourseDescriptionLayout()
        textBlockCourse.Text = ""
        textBlockCredits.Text = ""
        textBlockCreditsLabel.Visibility = Visibility.Hidden
        textBlockDescription.Text = ""
        textBlockPrerequisite.Text = ""
        textBlockPrerequisiteLabel.Visibility = Visibility.Hidden
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
                AddToListViewInOrder(listViewCourses, New ListViewCourseControl(course, False))
            End If
        Next
    End Sub

    Private Sub listViewCourses_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listViewCourses.SelectionChanged
        ClearCourseDescriptionLayout()
        If listViewCourses.Items.Count > 0 Then
            buttonMovetoDegree.IsEnabled = True
        Else
            buttonMovetoDegree.IsEnabled = False
        End If
        If listViewCourses.SelectedItems.Count = 1 Then
            Dim courseControl As ListViewCourseControl = listViewCourses.SelectedItem
            UpdateCourseDescriptionLayout(courseControl.Course)
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
            listViewDegreeCourses.Items.Add(listViewCourse)
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
            AddToListViewInOrder(listViewCourses, listViewCourse)
        Next
    End Sub

    Private Sub buttonExit_Click(sender As Object, e As RoutedEventArgs) Handles buttonExit.Click
        Me.Close()
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
End Class
