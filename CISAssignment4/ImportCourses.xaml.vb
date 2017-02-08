'------------------------------------------------------------ 
'-               File Name : ImportCourses.xaml.vb          - 
'-                Part of Project: Assignment 4             - 
'------------------------------------------------------------
'-                Written By: Trent Killinger               - 
'-                Written On: 2-1-17                        - 
'------------------------------------------------------------ 
'- File Purpose:                                            - 
'-                                                          - 
'- This file contains the import courses window where the   -
'- user can import courses from SVSU's website.             -
'------------------------------------------------------------
'- Variable Dictionary                                      - 
'- _degreeList - list of degrees to be added to the main gui-
'------------------------------------------------------------
Imports System.Net
Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports System.Threading
Public Class ImportCourses

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
    '- degreeDictionary - degree dictionary used to construct   -
    '- combobox                                                 - 
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New(degreeDictionary As Dictionary(Of Degree, List(Of Course)))
        InitializeComponent()
        _courseList = New List(Of Course)
        For Each degree In degreeDictionary
            Dim newDegree As DegreeComboBoxItem = New DegreeComboBoxItem()
            newDegree.Degree = degree.Key
            comboBoxDegrees.Items.Add(newDegree)
        Next
    End Sub

    '------------------------------------------------------------ 
    '-    Subprogram Name: comboBoxDegrees_SelectionChanged     - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine gennerates courses based on the selected -
    '- degrees.                                                 -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- comboboxItem - slected combobox item                     -
    '------------------------------------------------------------
    Private Sub comboBoxDegrees_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxDegrees.SelectionChanged
        rowProgressBar.Height = New GridLength(35)
        Dim comboboxItem As DegreeComboBoxItem = comboBoxDegrees.SelectedItem
        listViewCourse.Items.Clear()
        comboBoxDegrees.IsEnabled = False
        GenerateCourses(comboboxItem.Degree.DegreePrefix)
    End Sub

    '------------------------------------------------------------ 
    '-                Subprogram Name: GenerateCourses          - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine creates the courses by scraping SVSU's   -
    '- website.                                                 -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- prefix - course prefix used to grab courses              - 
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Private Async Sub GenerateCourses(prefix As String)
        System.Net.WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials
        Dim responseString As String = ""
        Dim degreeFirstHalf As String = "http://catalog.svsu.edu/content.php?filter%5B27%5D="
        Dim degreeLastHalf As String = "&filter%5B29%5D=&filter%5Bcourse_type%5D=-1&filter%5Bkeyword%5D=&filter%5B32%5D=1&filter%5Bcpage%5D=1&cur_cat_oid=27&expand=&navoid=1524&search_database=Filter#acalog_template_course_filter"
        Using client As HttpClient = New HttpClient()
            Dim address As Uri = New Uri(degreeFirstHalf + prefix + degreeLastHalf)
            Dim response = Await client.GetAsync(address)
            responseString = Await response.Content.ReadAsStringAsync()
            Clipboard.SetText(responseString)
        End Using
        Dim courseFirstHalf As String = "http://catalog.svsu.edu/ajax/preview_course.php?catoid=27&coid="
        Dim courseLastHalf As String = "&display_options=a%3A2%3A%7Bs%3A8%3A~location~%3Bs%3A8%3A~template~%3Bs%3A28%3A~course_program_display_field~%3Bi%3A1%3B%7D&show"
        Dim matchCourse As String = "preview_course_nopop.php.catoid=27.coid=....."
        Dim reg As Regex = New Regex(matchCourse)
        Dim m As Match = reg.Match(responseString)
        Dim doneCounter As Integer = 0
        Dim threadcount As Integer = 0
        While (m.Success)
            Dim url As String = courseFirstHalf + m.Value.Split("=").Last() + courseLastHalf
            Dim t As Thread = New Thread(Async Sub(u As String)
                                             Using c As HttpClient = New HttpClient()
                                                 Dim addr As Uri = New Uri(u)
                                                 Dim resp = Await c.GetAsync(addr)
                                                 Dim responsestr = Await resp.Content.ReadAsStringAsync()
                                                 Me.Dispatcher.Invoke(Sub()
                                                                          SyncLock listViewCourse
                                                                              AddInOrder(New ListViewCourseControl(ParseForCourse(responsestr), False))
                                                                          End SyncLock
                                                                      End Sub)
                                                 Interlocked.Increment(doneCounter)
                                             End Using
                                         End Sub)
            t.Start(url)
            threadcount += 1
            m = m.NextMatch()
        End While
        Dim doneTracker As Thread = New Thread(Sub()
                                                   While doneCounter < threadcount

                                                   End While
                                                   Dispatcher.Invoke(Sub()
                                                                         rowProgressBar.Height = New GridLength(0)
                                                                         comboBoxDegrees.IsEnabled = True
                                                                     End Sub)
                                               End Sub)
        doneTracker.Start()
    End Sub

    '------------------------------------------------------------ 
    '-                Subprogram Name: AddInOrder               - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine adds the course in order                 -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- courseControl - control to add to panel in order         -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- index - index of for loop                                -
    '------------------------------------------------------------
    Private Sub AddInOrder(courseControl As ListViewCourseControl)
        Dim index As String = 0
        For Each control As ListViewCourseControl In listViewCourse.Items
            If String.Compare(courseControl.Course.CoursePrefix, control.Course.CoursePrefix) < 1 Then
                listViewCourse.Items.Insert(index, courseControl)
                Exit Sub
            End If
            index += 1
        Next
        listViewCourse.Items.Add(courseControl)
    End Sub

    '------------------------------------------------------------ 
    '-                Subprogram Name: ParseForCourse           - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine parses the html respones into a course   -
    '- object                                                   -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- postResponse - string response from post request         -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '------------------------------------------------------------
    Private Function ParseForCourse(postResponse As String) As Course
        Dim description As CourseDescription = New CourseDescription("", "", "")
        Dim regx As Regex = New Regex("<div class=""ajaxcourseindentfix""><h3>.+</h3")
        Dim regxMatch As Match = regx.Match(postResponse)
        Dim courseTitle As String = ""
        While regxMatch.Success
            courseTitle = regxMatch.Value.Split(New String() {"</h3", ">"}, StringSplitOptions.RemoveEmptyEntries).Last()
            regxMatch = regxMatch.NextMatch
        End While
        regx = New Regex("(<hr>.*?)<")
        regxMatch = regx.Match(postResponse)
        Dim courseDescription As String = ""
        While regxMatch.Success
            If regxMatch.Value <> "<hr><" Then
                courseDescription = WebUtility.HtmlDecode(regxMatch.Value.Split(New String() {"<hr>", "<"}, StringSplitOptions.RemoveEmptyEntries).Last())
            End If
            regxMatch = regxMatch.NextMatch
        End While
        regx = New Regex("</strong>.+cr")
        regxMatch = regx.Match(postResponse)
        Dim courseCredits As String = ""
        While regxMatch.Success
            courseCredits = regxMatch.Value.Split(New String() {"cr", ">"}, StringSplitOptions.RemoveEmptyEntries).Last().Replace(" ", "").Replace("(", "").Replace(")", "")
            regxMatch = regxMatch.NextMatch
        End While
        regx = New Regex("(Requisites:</strong> .*?)<")
        regxMatch = regx.Match(postResponse)
        Dim coursePreRecList As List(Of String) = New List(Of String)
        While (regxMatch.Success)
            coursePreRecList.Add(regxMatch.Value.Split(New String() {"<", ">", "< "}, StringSplitOptions.RemoveEmptyEntries).Last())
            regxMatch = regxMatch.NextMatch
        End While
        Return New Course(New CourseDescription(courseDescription, String.Join(",", coursePreRecList), courseCredits),
                                                                                                   RTrim(courseTitle.Split("-").First()),
                                                                                                   LTrim(courseTitle.Split("-").Last()))
    End Function

    '------------------------------------------------------------ 
    '-              Subprogram Name: buttonImport_Click         - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine adds all the slected courses to a course -
    '- list to be addded to the main gui's course list          -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (none)                                                   -
    '------------------------------------------------------------
    Private Sub buttonImport_Click(sender As Object, e As RoutedEventArgs) Handles buttonImport.Click
        For Each courseInList As ListViewCourseControl In listViewCourse.SelectedItems
            _courseList.Add(courseInList.Course)
        Next
        Me.Close()
    End Sub

    '------------------------------------------------------------ 
    '-              Subprogram Name: buttonImportAll_Click      - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine adds all the listview courses to a course-
    '- list to be addded to the main gui's course list          -
    '------------------------------------------------------------
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (none)                                                   -
    '------------------------------------------------------------
    Private Sub buttonImportAll_Click(sender As Object, e As RoutedEventArgs) Handles buttonImportAll.Click
        For Each courseInList As ListViewCourseControl In listViewCourse.Items
            _courseList.Add(courseInList.Course)
        Next
        Me.Close()
    End Sub

    '------------------------------------------------------------ 
    '-                Property Name: CourseList                 - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property gets the CourseList                        -
    '------------------------------------------------------------ 
    Public Readonly Property CourseList() As List(Of Course)
        Get
            Return _courseList
        End Get
    End Property
End Class
