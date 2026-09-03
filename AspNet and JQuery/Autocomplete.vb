Public Class SystemPageTemplateAux2V5
    ' Declare an string
    Public ArraySapCompaniesCodes As String() = {}

    Protected Sub Page_Load()
        ' Fill the atring           ' Call mehtod to fill string
         ArraySapCompaniesCodes = DirectoryFunctionsClass.getLensMasterVCAFilters(21)
    End Sub

End Class


Public Class DirectoryFunctionsClass

' Send a case number to build the query
Public Shared Function getLensMasterVCAFilters(ByVal ListNo As Integer) As String()

    Dim QueryString As String = ""
    Dim myList As New List(Of String)()

    Select Case ListNo
        Case 1
            QueryString = "SELECT DISTINCT LensDesignCode AS custValue FROM LensDesignMaster ORDER BY LensDesignCode"
            'QueryString = "SELECT DISTINCT LensDesignDesc AS custValue FROM LensDesignMaster ORDER BY LensDesignDesc"

        Case 2
        QueryString = "SELECT DISTINCT (LensMaterialCode + ' - ' + LensMaterialDesc) AS custValue FROM LensMaterialMaster ORDER BY custValue"
        End Select

    If (QueryString <> "") Then
        Dim SqlConn As New System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("RxPortal").ConnectionString)
        Dim SqlCmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand()

        SqlConn.Open()
        SqlCmd = New System.Data.SqlClient.SqlCommand(QueryString, SqlConn)
        Dim SqlReader As System.Data.SqlClient.SqlDataReader = SqlCmd.ExecuteReader()

        While SqlReader.Read()
            myList.Add(Replace(SqlReader("custValue"), "'", ""))
        End While

        SqlReader.Close()
        SqlCmd.Connection.Close()
    End If

Return myList.ToArray()
End Function

End Class