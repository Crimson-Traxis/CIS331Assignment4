Imports System.Net.Http

Public Class ImportDegrees

    Private _degreeList As List(Of Degree)

    Public Sub New()
        InitializeComponent()
        _degreeList = New List(Of Degree)
        CreateDegreese("")
    End Sub
    Public Property DegreeList() As List(Of Degree)
        Get
            Return _degreeList
        End Get
        Set(ByVal value As List(Of Degree))
            _degreeList = value
        End Set
    End Property

    Private Async Function ParseData() As Task(Of String)

    End Function

    Private Async Sub CreateDegreese(data As String)
        Dim response As String = ""
        Using client As HttpClient = New HttpClient()
            Try
                Dim u As Uri = New Uri("http://catalog.svsu.edu/content.php?catoid=27&navoid=1524")
                Dim r = Await client.GetAsync(u)
                response = Await r.Content.ReadAsStringAsync()
            Catch ex As Exception

            End Try
        End Using

    End Sub
End Class
