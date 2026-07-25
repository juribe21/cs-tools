Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports OfficeOpenXml
Imports OfficeOpenXml.Table
Imports System.IO
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Web.Script.Services
Imports System.ComponentModel.DataAnnotations


Public Class CzvReduceProducts
    Inherits System.Web.UI.Page

    Dim selectedOption As String = String.Empty


    Dim ActiveLabID As String
    Dim ReportDate As String
    Dim GridView2 As GridView = New GridView()

    Shared localValue As String
    Shared localSelectedOption As String
    Shared FileNameII As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActiveLabID = Session("LabID")
        'lblLabID.Text = Session("LabShortName")
        ReportDate = Format(Date.Today, "yyyy-MM-dd")


        If Page.IsPostBack Then
            '*** If there is no serch value then get the next page ***'
            GetGlobalReduceColorMaster()
        Else
            GetGlobalReduceColorMaster()
            LoadDdlCriteria()
        End If
    End Sub


    Private Sub GetGlobalReduceColorMaster()

        Dim myDataSet As New MasterDataCodesDS
        Dim SurfaceToolsDA As New MasterDataCodesDSTableAdapters.GlobalReduceProductMasterTableAdapter

        SurfaceToolsDA.Fill(myDataSet.GlobalReduceProductMaster)

        GridView1.DataSource = myDataSet.Tables("GlobalReduceProductMaster")
        GridView1.DataBind()

        If (GridView1.Rows.Count > 0) Then
            DivScroll.Style.Add("width", "1230px")
            DivScroll.Style.Add("height", "650px")
            DivScroll.Style.Add("overflow", "auto")

        End If
    End Sub

    Private Sub LoadDdlCriteria()

        ddlCriteria.Items.Add("SELECT")
        ddlCriteria.Items.Add("EdiCode")
        ddlCriteria.Items.Add("LensMaterial")
        ddlCriteria.Items.Add("RegionCountry")
        ddlCriteria.Items.Add("MaterialGroup")

        ddlCriteria.DataBind()
    End Sub

    Protected Sub gridView_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        GridView1.DataBind()
    End Sub


    Private Function GetPrintData() As GridView
        Dim myDataSet As New MasterDataCodesDS

        ' ToDo: Crear TableAdapters

        If localSelectedOption = "colorCode" Then
            FileNameII = "_ColorCode_"
            Dim SurfaceToolsDA As New MasterDataCodesDSTableAdapters.RxColorCodeTableAdapter
            SurfaceToolsDA.Fill(myDataSet.RxColorCode, localValue)
            GridView1.DataSource = myDataSet.Tables("RxColorCode")
            GridView1.DataBind()
        End If

        If localSelectedOption = "lensColor" Then
            FileNameII = "_LensColor_"
            Dim SurfaceToolsDA As New MasterDataCodesDSTableAdapters.RxLensColorCodeTableAdapter
            SurfaceToolsDA.Fill(myDataSet.RxLensColorCode, localValue)
            GridView1.DataSource = myDataSet.Tables("RxLensColorCode")
            GridView1.DataBind()
        End If

        Return GridView1
    End Function
    Sub ExportToOffice(ByVal Source As Object, ByVal e As CommandEventArgs)
        Dim FileName As String, FileType As String
        FileType = e.CommandArgument

        GridView1 = GetPrintData()

        FileName = ActiveLabID.ToString & FileNameII
        FileName = FileName + ReportDate
        Response.Clear()
        Response.Buffer = True

        Select Case FileType

            Case "Excel"

                Dim pck = createExcelPackage()

                Response.BinaryWrite(pck.GetAsByteArray())
                Response.ContentType = "application/vnd.ms-excel.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment;  filename=" & FileName & ".xlsx")
                Response.End()

            Case "Word"

                Response.AddHeader("content-disposition", "attachment;filename=" & FileName & ".doc")
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-word"
                GridView1.Font.Size = 8

                Dim sw As New System.IO.StringWriter()
                Dim hw As New System.Web.UI.HtmlTextWriter(sw)

                GridView1.Font.Name = "arial"
                GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White
                GridView1.AllowPaging = "False"
                GridView1.DataBind()

                '*** Change the Header Row to Blue Color 
                GridView1.HeaderRow.Style.Add("background-color", "#0D5692")

                For i As Integer = 0 To GridView1.Rows.Count - 1
                    Dim row As GridViewRow = GridView1.Rows(i)

                    '*** Set the Default Row Background to White ***
                    row.BackColor = System.Drawing.Color.White

                    '*** Apply style to Individual Cells of Alternating Row ***
                    If i Mod 2 <> 0 Then
                        For j As Integer = 0 To row.Cells.Count - 1
                            row.Cells(j).Style.Add("background-color", "#F1F1F1")
                        Next
                    End If
                Next

                GridView1.RenderControl(hw)
                Response.Output.Write(sw.ToString())
                Response.Flush()
                Response.End()

        End Select

    End Sub
    Private Function createExcelPackage() As ExcelPackage

        Dim dt As DataTable = New DataTable()

        GridView1.AllowPaging = False
        GridView1.DataBind()

        For i As Integer = 0 To GridView1.Columns.Count - 1
            dt.Columns.Add(GridView1.Columns(i).HeaderText.ToString())
        Next

        For Each row As GridViewRow In GridView1.Rows
            Dim dr As DataRow = dt.NewRow()

            For j As Integer = 0 To GridView1.Columns.Count - 1
                dr(GridView1.Columns(j).HeaderText.ToString()) = row.Cells(j).Text.Replace("&nbsp;", "")

            Next
            dt.Rows.Add(dr)
        Next

        Dim package = New ExcelPackage()

        package.Workbook.Properties.Title = "Surface Tool List Report"
        package.Workbook.Properties.Author = "Rx Portal"
        package.Workbook.Properties.Subject = "Surface Tool List Report"
        package.Workbook.Properties.Keywords = "Surface Tool List Report"

        Dim worksheet = package.Workbook.Worksheets.Add("Surface Tool List")
        worksheet.View.FreezePanes(2, 2)


        For i As Integer = 0 To dt.Columns.Count - 1
            worksheet.Cells(1, i + 1).Value = dt.Columns(i).ColumnName
        Next

        Dim numberformat = "#,##0"
        Dim dataCellStyleName = "TableNumber"
        Dim numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyleName)
        numStyle.Style.Numberformat.Format = numberformat

        For rowNum As Integer = 0 To dt.Rows.Count - 1

            For colNum As Integer = 0 To dt.Columns.Count - 1

                Dim text = dt.Rows(rowNum)(colNum).ToString()
                worksheet.Cells(rowNum + 2, colNum + 1).Value = text

            Next

        Next

        worksheet.Cells(1, 1, dt.Rows.Count + 2, dt.Columns.Count + 1).AutoFitColumns()
        Dim tbl = worksheet.Tables.Add(New ExcelAddressBase(fromRow:=1, fromCol:=1, toRow:=dt.Rows.Count + 2, toColumn:=dt.Columns.Count), "Data")
        tbl.ShowHeader = True
        tbl.TableStyle = TableStyles.Medium6
        Return package

    End Function

    Protected Sub ExportToPDF(ByVal sender As Object, ByVal e As EventArgs)
        Dim FileName As String

        GridView1 = GetPrintData()

        FileName = ActiveLabID.ToString & "_SurfaceToolList_"
        FileName = FileName + ReportDate
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=" & FileName & ".pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Dim sw As New StringWriter()
        Dim hw As New HtmlTextWriter(sw)

        GridView1.Font.Name = "arial"
        GridView1.Font.Size = 10
        GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Black
        GridView1.AllowPaging = False
        GridView1.DataBind()


        '*** Change the Header Row to Blue Color 
        GridView1.HeaderRow.Style.Add("background-color", "#f0f8ff") '0D5692

        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim row As GridViewRow = GridView1.Rows(i)

            '*** Set the Default Row Background to White ***
            row.BackColor = System.Drawing.Color.AliceBlue

            '*** Apply text style to each Row ***
            row.Attributes.Add("class", "textmode")

            '*** Apply style to Individual Cells of Alternating Row ***
            If i Mod 2 <> 0 Then
                row.Cells(0).Style.Add("background-color", "#F1F1F1")
                row.Cells(1).Style.Add("background-color", "#F1F1F1")
                row.Cells(2).Style.Add("background-color", "#F1F1F1")
                row.Cells(3).Style.Add("background-color", "#F1F1F1")
                row.Cells(4).Style.Add("background-color", "#F1F1F1")
                row.Cells(5).Style.Add("background-color", "#F1F1F1")
                row.Cells(6).Style.Add("background-color", "#F1F1F1")
            End If

        Next

        GridView1.RenderControl(hw)

        Dim sr As New StringReader(sw.ToString())
        Dim pdfDoc As New Document(PageSize.LEGAL.Rotate(), 10.0F, 10.0F, 10.0F, 0.0F)
        Dim htmlparser As New HTMLWorker(pdfDoc)

        PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
        pdfDoc.Open()
        htmlparser.Parse(sr)
        pdfDoc.Close()
        Response.Write(pdfDoc)
        Response.End()

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        '*** Ensures That The GridView is Rendered Before File Export ***
    End Sub


    Protected Sub btnSelectedCriteria(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCriteria.SelectedIndexChanged

        selectedOption = ddlCriteria.SelectedItem.Text
        localSelectedOption = selectedOption
        BuildReduceProductoCriteria(selectedOption)
    End Sub

    Private Sub BuildReduceProductoCriteria(ByVal selectedOption As String)
        Dim SqlConn As New System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("RxPortal").ConnectionString)
        Dim SqlCmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand()
        Dim QueryString As String = String.Empty
        Dim dt As DataTable = New DataTable()
        ddlReduceProductoCriteria.Items.Clear()

        If selectedOption = "EdiCode" Then
            QueryString = "Select Distinct EdiCode As SearchValue From GlobalReduceProductMaster"
        End If

        If selectedOption = "LensMaterial" Then
            QueryString = "Select Distinct LensMaterial As SearchValue From GlobalReduceProductMaster"
        End If

        If selectedOption = "RegionCountry" Then
            QueryString = "Select Distinct RegionCountry As SearchValue From GlobalReduceProductMaster"
        End If

        If selectedOption = "MaterialGroup" Then
            QueryString = "Select Distinct MaterialGroup As SearchValue From GlobalReduceProductMaster"
        End If

        SqlConn.Open()
        SqlCmd = New SqlCommand(QueryString, SqlConn)
        Dim SqlReader As SqlDataReader = SqlCmd.ExecuteReader()

        dt.Columns.Add("Option")

        Dim counter = 0
        Try
            'logic to know the departments that has breakages
            While SqlReader.Read()
                Dim dr = dt.NewRow()
                dr("Option") = SqlReader("SearchValue").ToString
                dt.Rows.Add(dr)
                counter = counter + 1
            End While
        Finally
            '** Always Call SQL Close When Done Reading ***
            SqlReader.Close()
            SqlCmd.Connection.Close()
        End Try

        ddlReduceProductoCriteria.Items.Add("SELECT")
        For Each row As DataRow In dt.Rows

            Dim value As String = row("Option").ToString()
            ddlReduceProductoCriteria.Items.Add(value)
        Next
        ddlReduceProductoCriteria.DataBind()

    End Sub

    '***************************************************
    '           lOAD GRID WITH INFORMATION
    '***************************************************

    Dim myDataSet As New MasterDataCodesDS

    Protected Sub btnSelectedReduceProductoCriteria(ByVal sender As Object, ByVal e As EventArgs) Handles ddlReduceProductoCriteria.SelectedIndexChanged

        Dim value = ddlReduceProductoCriteria.SelectedItem.Text
        BuildReduceProductList(value, localSelectedOption)
    End Sub

    Private Sub BuildReduceProductList(ByVal value As String, ByVal selectedOption As String)
        Dim SurfaceToolsDA As New MasterDataCodesDSTableAdapters.GlobalReduceProductMasterFilterTableAdapter
        SurfaceToolsDA.Fill(myDataSet.GlobalReduceProductMasterFilter, value)

        GridView1.DataSource = myDataSet.Tables("GlobalReduceProductMasterFilter")
        GridView1.DataBind()
    End Sub


End Class