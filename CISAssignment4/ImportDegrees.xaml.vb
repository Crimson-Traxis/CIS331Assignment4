Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class ImportDegrees

    Private _degreeList As List(Of Degree)

    Public Sub New()
        InitializeComponent()
        _degreeList = New List(Of Degree)
        SyncFromOnline()
    End Sub

    Public Async Sub SyncFromOnline()
        System.Net.WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials
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
        Dim firstHalf As String = "http://catalog.svsu.edu/content.php?filter%5B27%5D="
        Dim lastHalf As String = "&filter%5B29%5D=&filter%5Bcourse_type%5D=-1&filter%5Bkeyword%5D=&filter%5B32%5D=1&filter%5Bcpage%5D=1&cur_cat_oid=27&expand=&navoid=1524&search_database=Filter#acalog_template_course_filter"
        For Each prefix In degreePrefixes
            Dim t As Thread = New Thread(Async Sub(coursePrefix As String)
                                             Using client As HttpClient = New HttpClient()
                                                 Dim str As String = ""
                                                 Dim address As Uri = New Uri(firstHalf + coursePrefix + lastHalf)
                                                 Dim response = Await client.GetAsync(address)
                                                 str = Await response.Content.ReadAsStringAsync()
                                                 Dim matchString As String = "<td colspan=""2""><br><p><b>[a-zA-Z ]+</b></p></td>"
                                                 Dim reg As Regex = New Regex(matchString)
                                                 Dim m As Match = reg.Match(str)
                                                 While (m.Success)
                                                     Dispatcher.Invoke(Sub()
                                                                           listViewDegrees.Items.Add(New ListViewDegreeControl(New Degree(coursePrefix, m.Value.Replace("<td colspan=""2""><br><p><b>", "").Replace("</b></p></td>", ""))))
                                                                       End Sub)
                                                     Exit While
                                                 End While
                                                 If coursePrefix = degreePrefixes.Last() Then
                                                     Dispatcher.Invoke(Sub()
                                                                           rowProgressBar.Height = New GridLength(0)
                                                                       End Sub)
                                                 End If
                                             End Using
                                         End Sub)
            t.Start(prefix)
        Next
    End Sub

    Private Sub buttonImport_Click(sender As Object, e As RoutedEventArgs) Handles buttonImport.Click
        For Each listViewDegree As ListViewDegreeControl In listViewDegrees.SelectedItems
            _degreeList.Add(listViewDegree.Degree)
        Next
        Me.Close()
    End Sub

    Public Property DegreeList() As List(Of Degree)
        Get
            Return _degreeList
        End Get
        Set(ByVal value As List(Of Degree))
            _degreeList = value
        End Set
    End Property

    Private Sub buttonImportAll_Click(sender As Object, e As RoutedEventArgs) Handles buttonImportAll.Click
        For Each listViewDegree As ListViewDegreeControl In listViewDegrees.Items
            _degreeList.Add(listViewDegree.Degree)
        Next
        Me.Close()
    End Sub
End Class
