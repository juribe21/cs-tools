 Protected Sub ExportToZip(ByVal Source As Object, ByVal e As CommandEventArgs)
        FileType = e.CommandArgument

        FileName = SetFileName()
        Dim excelSavePath As String = String.Empty

        Dim ServerLocation As String = "\\USSA2SRXPDB01.cznet.zeiss.org\ProcessQueues\"
        Dim ProductCatalogs As String = "ProductCatalogs"
        Dim ExcelProductCatalogs As String = "ProductCatalogs\ExcelTemp"
        'Upload Successful. File Scheduled for Processing
        excelSavePath = ServerLocation + ExcelProductCatalogs


        If (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) = "http://rxportalprd.zeiss.org") Then
            ServerLocation += ProductCatalogs
        ElseIf (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) = "http://rxportalqas.zeiss.org") Then
            ServerLocation += ProductCatalogs
        ElseIf (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) = "http://rxportaldev.zeiss.org") Then
            ServerLocation += ProductCatalogs
        ElseIf (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) = "http://localhost:61799") Then
            ServerLocation += ProductCatalogs
        End If


        FileName = FileName + "_" + Format(Date.Today, "yyyy-MM-dd")
        Response.Clear()
        Response.Buffer = True

        'Dim zipSavePath As String = "C:\Users\vijuribe\Desktop\BKs\ZipFiles\" + FileName + ".zip"
        Dim zipSavePath As String = Path.Combine(ServerLocation, FileName + ".zip")

        Dim pck = createExcelPackage() 'Crea el excel ***

        'For Each filePath As String In Directory.GetFiles(excelSavePath)
        '    File.Delete(excelSavePath)
        'Next

        Directory.CreateDirectory(excelSavePath)
        excelSavePath = Path.Combine(excelSavePath, FileName + ".xlsx")

        pck.SaveAs(New FileInfo(excelSavePath))

        'Remove existent zipFile
        If System.IO.File.Exists(zipSavePath) Then
            System.IO.File.Delete(zipSavePath)
        End If

        ' Open or create the zip archive
        Using archive As ZipArchive = ZipFile.Open(zipSavePath, ZipArchiveMode.Create)

            If File.Exists(zipSavePath) Then
                ' Get the file name to avoid nested path directories inside the ZIP
                Dim entryName As String = Path.GetFileName(excelSavePath)

                ' Add the file to the archive
                archive.CreateEntryFromFile(excelSavePath, entryName)
            End If
        End Using

        'Remove existent Excel File
        If System.IO.File.Exists(excelSavePath) Then
            System.IO.File.Delete(excelSavePath)
        End If

        Response.Redirect("~/Reporting/CustomerCatalog/CustomerCatalogProducts.aspx?ProductCatalog=ALL&Rxbm=ALL&Ld=ALL&Lm=ALL&EdiCode=ALL")

    End Sub


Private Sub  createExcelPackage()
    'Crear el proceso de creacion del Excel
End Sub



' *********************************** Open File Dialog  ****************************************

Private Sub button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim saveFileDialog1 As SaveFileDialog = New SaveFileDialog()
    saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif"
    saveFileDialog1.Title = "Save an Image File"
    saveFileDialog1.ShowDialog()

    If saveFileDialog1.FileName <> "" Then
        Dim fs As System.IO.FileStream = CType(saveFileDialog1.OpenFile(), System.IO.FileStream)

        Select Case saveFileDialog1.FilterIndex
            Case 1
                Me.button2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg)
            Case 2
                Me.button2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp)
            Case 3
                Me.button2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif)
        End Select

        fs.Close()
    End If
End Sub