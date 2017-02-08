'------------------------------------------------------------ 
'-               File Name : DegreeBuilder.xaml.vb          - 
'-                Part of Project: Assignment 4             - 
'------------------------------------------------------------
'-                Written By: Trent Killinger               - 
'-                Written On: 2-1-17                        - 
'------------------------------------------------------------ 
'- File Purpose:                                            - 
'-                                                          - 
'- This file contains the gui that adds the courses to the  -
'- degrees                                                  -
'------------------------------------------------------------
'- Variable Dictionary                                      - 
'- _degreeDictionary - degree dictionary                    -
'- _courseList - course list                                -
'------------------------------------------------------------

Public Class DegreeBuilder

    Private _degreeDictionary As Dictionary(Of Degree, List(Of Course))
    Private _courseList As List(Of Course)

    '------------------------------------------------------------ 
    '-                Subprogram Name: New                      - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine creates the gui and instantiates default -
    '- member data/objects                                      -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- (None)                                                   - 
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New()
        InitializeComponent()
        _degreeDictionary = New Dictionary(Of Degree, List(Of Course))
        _courseList = New List(Of Course)
        buttonMovetoCourses.IsEnabled = False
        buttonMovetoDegree.IsEnabled = False
    End Sub

    '------------------------------------------------------------ 
    '-                Subprogram Name: New                      - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine creates the gui and instantiates default -
    '- member data/objects                                      -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- degreeDictionary - degree dictionary                     -
    '- courseList - course list                                 -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
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

    '------------------------------------------------------------ 
    '- Subprogram Name: listViewDegreeCourses_SelectionChanged  -
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine determines if the user can add courses   -
    '- to the selected degree and vise versa                    -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- courseControl - control that contains the course object  -
    '------------------------------------------------------------
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

    '------------------------------------------------------------ 
    '-     Subprogram Name: UpdateCourseDescriptionLayout       - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine updates the visual course description    -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- course - course object to display                        -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Private Sub UpdateCourseDescriptionLayout(course As Course)
        textBlockCourse.Text = course.CoursePrefix + " - " + course.CourseName
        textBlockCredits.Text = course.Description.Credits + " cr"
        textBlockCreditsLabel.Visibility = Visibility.Visible
        textBlockDescription.Text = course.Description.Description
        textBlockPrerequisite.Text = course.Description.Prerequisite
        textBlockPrerequisiteLabel.Visibility = Visibility.Visible
    End Sub

    '------------------------------------------------------------ 
    '-     Subprogram Name: ClearCourseDescriptionLayout        - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine clears the visual course description     -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- (none)                                                   -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Private Sub ClearCourseDescriptionLayout()
        textBlockCourse.Text = ""
        textBlockCredits.Text = ""
        textBlockCreditsLabel.Visibility = Visibility.Hidden
        textBlockDescription.Text = ""
        textBlockPrerequisite.Text = ""
        textBlockPrerequisiteLabel.Visibility = Visibility.Hidden
    End Sub

    '------------------------------------------------------------ 
    '-     Subprogram Name: listViewDegrees_SelectionChanged    -
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine updates the listviews by reading the     -
    '- degreedictionary and courselist                          -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- listViewDegree - selected degree control                 -
    '- degreeCourseList - list of courses in slected degree     -
    '------------------------------------------------------------
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

    '------------------------------------------------------------ 
    '-      Subprogram Name: listViewCourses_SelectionChanged   -
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine determines if the user can add courses   -
    '- to the selected degree and vise versa                    -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- courseControl - control that contains the course object  -
    '------------------------------------------------------------
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

    '------------------------------------------------------------ 
    '-          Subprogram Name: buttonMovetoDegree_Click       -
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine updates the degree in the degree         -
    '- dictionary then moves the controls to the appropriate    -
    '- listview                                                 -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- coursestoMove - course to move                           -
    '- listViewDegree - selected degree                         -
    '- degreeCourseList - degree's list of courses              -
    '------------------------------------------------------------
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

    '------------------------------------------------------------ 
    '-          Subprogram Name: buttonMovetoCourses_Click      -
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine updates the degree in the degree         -
    '- dictionary then moves the controls to the appropriate    -
    '- listview                                                 -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- coursestoMove - course to move                           -
    '- listViewDegree - selected degree                         -
    '- degreeCourseList - degree's list of courses              -
    '------------------------------------------------------------
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

    '------------------------------------------------------------ 
    '-              Subprogram Name: buttonExit_Click           -
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine exits the application                    -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (none)                                                   -
    '------------------------------------------------------------
    Private Sub buttonExit_Click(sender As Object, e As RoutedEventArgs) Handles buttonExit.Click
        Me.Close()
    End Sub

    '------------------------------------------------------------ 
    '-      Subprogram Name: AddToListViewInOrder               - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine adds the course to the passed panel      -
    '- in order                                                 -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- listView - panel to add the control                      -
    '- course - course control to add inorder                   -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- index - index of for loop                                -
    '------------------------------------------------------------
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

    '------------------------------------------------------------ 
    '-      Subprogram Name: AddToListViewInOrder               - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine adds the course to the passed panel      -
    '- in order                                                 -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- listView - panel to add the control                      -
    '- degree - degree control to add inorder                   -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- index - index of for loop                                -
    '------------------------------------------------------------
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
