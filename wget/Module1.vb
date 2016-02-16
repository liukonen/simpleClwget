Imports System.Net
Module Module1

    Sub Main()
        'For Each s In My.Application.CommandLineArgs.ToArray
        Dim s As String = My.Application.CommandLineArgs(0)
        'MsgBox(s)
        Try
            Dim name As String = s.Substring(s.LastIndexOf("/") + 1).Replace("%", " ")

            Console.WriteLine(("Downloading " & s))
            Dim client As New WebClient
            client.DownloadFile(s, name)
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try

        'Next
    End Sub

End Module
