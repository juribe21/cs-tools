Imports System.IO
Imports System.IO.Compression

Public Sub SaveBytesToZipFile(fileData As Byte(), filenameInZip As String, destinationZipPath As String)
    ' Open or create the physical ZIP file on disk
    Using fileStream As New FileStream(destinationZipPath, FileMode.Create)
        
        ' Initialize ZipArchive linked to the file stream
        Using archive As New ZipArchive(fileStream, ZipArchiveMode.Create)
            
            Dim zipEntry = archive.CreateEntry(filenameInZip, CompressionLevel.Optimal)
            
            Using entryStream As Stream = zipEntry.Open()
                entryStream.Write(fileData, 0, fileData.Length)
            End Using
            
        End Using
    End Using
End Sub


Public Shared Function CreateZipFromByteArray(ByVal fileData As Byte(), ByVal fileNameWithinZip As String) As Byte()
    
    Using outputStream = New MemoryStream()
        Using archive = New ZipArchive(outputStream, ZipArchiveMode.Create, True)
            Dim zipEntry = archive.CreateEntry(fileNameWithinZip, CompressionLevel.Optimal)

            Using entryStream = zipEntry.Open()
                entryStream.Write(fileData, 0, fileData.Length)
            End Using
        End Using

        Return outputStream.ToArray()
    End Using
End Function