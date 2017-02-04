Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class ImportCourses

    Private _courseList As List(Of Course)

    Public Sub New(degreeDictionary As Dictionary(Of Degree, List(Of Course)))
        InitializeComponent()
        _courseList = New List(Of Course)
        For Each degree In degreeDictionary
            Dim newDegree As DegreeComboBoxItem = New DegreeComboBoxItem()
            newDegree.Degree = degree.Key
            comboBoxDegrees.Items.Add(newDegree)
        Next
    End Sub

    Private Sub comboBoxDegrees_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxDegrees.SelectionChanged
        rowProgressBar.Height = New GridLength(35)
        Dim comboboxItem As DegreeComboBoxItem = comboBoxDegrees.SelectedItem
        listViewCourse.Items.Clear()
        GenerateCourses(comboboxItem.Degree.DegreePrefix)
    End Sub

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
                                                                          listViewCourse.Items.Add(New ListViewCourseControl(ParseForCourse(responsestr), True))
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
                                                                     End Sub)
                                               End Sub)
        doneTracker.Start()
    End Sub

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
                courseDescription = regxMatch.Value.Split(New String() {"<hr>", "<"}, StringSplitOptions.RemoveEmptyEntries).Last()
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

    Private Sub buttonImport_Click(sender As Object, e As RoutedEventArgs) Handles buttonImport.Click
        For Each courseInList As ListViewCourseControl In listViewCourse.SelectedItems
            _courseList.Add(courseInList.Course)
        Next
        Me.Close()
    End Sub

    Private Sub buttonImportAll_Click(sender As Object, e As RoutedEventArgs) Handles buttonImportAll.Click
        For Each courseInList As ListViewCourseControl In listViewCourse.Items
            _courseList.Add(courseInList.Course)
        Next
        Me.Close()
    End Sub

    Public Property CourseList() As List(Of Course)
        Get
            Return _courseList
        End Get
        Set(ByVal value As List(Of Course))
            _courseList = value
        End Set
    End Property
End Class
