
' **************************************************
' ********** Fill GridView from DataTable **********
' **************************************************

    Protected Sub CustomerCatalogProductsData(ByVal ProductCatalog As String, ByVal RxBillingMaterial As String, ByVal LensDesign As String, ByVal LensMaterial As String)

        QueryString = "EXEC usp_Reporting_ProductCatalog " + "'" + ProductCatalog + "', " + "'" + RxBillingMaterial + "', " + "'" + LensDesign + "', '" + LensMaterial + "'"
        SqlConn.Open()
        SqlCmd = New System.Data.SqlClient.SqlCommand(QueryString, SqlConn)
        Dim SqlReader As System.Data.SqlClient.SqlDataReader = SqlCmd.ExecuteReader()
        Dim dt As DataTable = New DataTable()

        dt.Columns.Add("ProductCatalog")
        dt.Columns.Add("EdiCode")
        dt.Columns.Add("EdiColor")
        dt.Columns.Add("RxBillingMaterial")
        dt.Columns.Add("Reduce")
        dt.Columns.Add("CommercialDesc")
        dt.Columns.Add("LensDesign")
        dt.Columns.Add("LensMaterial")
        dt.Columns.Add("LensColor")

        Dim counter = 0
        Try
            'logic to know the departments that has breakages
            While SqlReader.Read()
                Dim dr = dt.NewRow()
                dr("ProductCatalog") = SqlReader("ProductCatalog").ToString
                dr("EdiCode") = SqlReader("EdiCode").ToString
                dr("EdiColor") = SqlReader("EdiColor").ToString
                dr("RxBillingMaterial") = SqlReader("RxBillingMaterial").ToString
                dr("Reduce") = SqlReader("ReduceLogic").ToString
                dr("CommercialDesc") = SqlReader("CommercialDesc").ToString
                dr("LensDesign") = SqlReader("LensDesign").ToString
                dr("LensMaterial") = SqlReader("LensMaterial").ToString
                dr("LensColor") = SqlReader("LensColor").ToString

                dt.Rows.Add(dr)
                counter = counter + 1
            End While
        Finally
            '** Always Call SQL Close When Done Reading ***
            SqlReader.Close()
            SqlCmd.Connection.Close()
        End Try
        GridView1.DataSource = dt
        GridView1.DataBind()


        '' DB Connection
        'Dim SqlConn As New System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("RxPortal").ConnectionString)
        'Dim SqlCmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand()


        'Dim myDataSet As New CustomerCatalogDS
        'Dim AllJobsInWipDA As New CustomerCatalogDSTableAdapters.CustomerCatalogProductsTableAdapter

        'Try

        '    AllJobsInWipDA.Fill(myDataSet.CustomerCatalogProducts, ProductCatalog)
        'Catch ex As Exception
        '    Dim TextEx = ex.Message.ToString()
        'End Try

        'GridViewRowCount = myDataSet.Tables("CustomerCatalogProducts").Rows.Count
        'GridView1.DataSource = myDataSet.Tables("CustomerCatalogProducts")
        'GridView1.DataBind()

        'If (GridView1.Rows.Count > 0) Then
        '    DivScroll.Style.Add("width", "1270px")
        '    DivScroll.Style.Add("height", "650px")
        '    DivScroll.Style.Add("overflow", "auto")

        'End If


    End Sub


    Protected Sub CustomerCatalogProductsData()
        '' DB Connection
        Dim SqlConn As New System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("RxPortal").ConnectionString)
        Dim SqlCmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand()


        Dim myDataSet As New CustomerCatalogDS
        Dim AllJobsInWipDA As New CustomerCatalogDSTableAdapters.CustomerCatalogProductsTableAdapter

        Try

            AllJobsInWipDA.Fill(myDataSet.CustomerCatalogProducts, ProductCatalog)
        Catch ex As Exception
            Dim TextEx = ex.Message.ToString()
        End Try

        GridViewRowCount = myDataSet.Tables("CustomerCatalogProducts").Rows.Count
        GridView1.DataSource = myDataSet.Tables("CustomerCatalogProducts")
        GridView1.DataBind()

    End Sub