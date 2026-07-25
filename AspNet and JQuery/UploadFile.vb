    Imports System.Web.Services
    Imports System.Web.Script.Services
    Imports System.IO


' Uploa file using html tool <selec></select>
    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function UploadFile(ByVal fileName As String, ByVal fileData As String) As String
        Try
            ' Remove the "data:image/png;base64," prefix if present
            Dim base64String As String = fileData.Split(",")(1)
            Dim bytes As Byte() = Convert.FromBase64String(base64String)

            Dim uploadPath As String = HttpContext.Current.Server.MapPath("~/Uploads/") ' Create an "Uploads" folder
            If Not Directory.Exists(uploadPath) Then
                Directory.CreateDirectory(uploadPath)
            End If

            Dim filePath As String = Path.Combine(uploadPath, fileName)
            File.WriteAllBytes(filePath, bytes)

            Return "File '" & fileName & "' uploaded successfully."
        Catch ex As Exception
            Return "Error uploading file: " & ex.Message
        End Try
    End Function