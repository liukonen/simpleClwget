Imports System.Net
Imports System.Threading.Tasks

Module Module1

    'Enhancements planed.
    'DONE - 1 if multiple files are passed in, download all of them (for each, which is already there, just commented out
    'DONE - 2 Multithreading
    '3 an option, to open a text file and download the contents of the file. (line by line, and erase once complete)
    '4 an option to indicate a save to directory
    '5 an override parameter to indicate how many threads to use (default 3)
    '6 external error logging

    Sub Main()

        Dim Errors As New Concurrent.ConcurrentBag(Of Exception)()

        Try
            Dim PO As New Threading.Tasks.ParallelOptions()
            PO.MaxDegreeOfParallelism = 3
            Parallel.ForEach(My.Application.CommandLineArgs.ToArray, PO, Sub(ByVal DownloadLink As String)
                                                                             Dim FileName As String = DownloadLink.Substring(DownloadLink.LastIndexOf("/") + 1).Replace("%", " ")
                                                                             Try
                                                                                 If Not IO.File.Exists(FileName) Then
                                                                                     Console.WriteLine("Downloading file: " & FileName)
                                                                                     Using client As New WebClient()
                                                                                         client.DownloadFile(DownloadLink, FileName)
                                                                                     End Using
                                                                                 Else
                                                                                     Console.WriteLine("Can not download file because it already exists:" & FileName)
                                                                                 End If
                                                                             Catch ex As Exception
                                                                                 Errors.Add(ex)
                                                                             End Try
                                                                         End Sub)
        Catch ex As Exception
            Errors.Add(ex)
        End Try

        If Errors.Any Then
            For Each X As Exception In Errors
                Console.WriteLine(X.ToString())
            Next
        End If
    End Sub

End Module
