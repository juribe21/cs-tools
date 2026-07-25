
'Load DropdownList

Protected Sub LoadDefaultCartonTypeDDL()

    Dim SqlConn As New System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("RxPortal").ConnectionString)
    Dim SqlCmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand()
    Dim QueryString As String
    Dim idx As Integer = 0

    QueryString = "Select Distinct CartonType From ShipCartonMaster"
    SqlConn.Open()
    SqlCmd = New System.Data.SqlClient.SqlCommand(QueryString, SqlConn)

    Dim SqlReader As System.Data.SqlClient.SqlDataReader = SqlCmd.ExecuteReader()

    Try
        While SqlReader.Read()
            ddlDefaultCartonType.Items.Insert(idx, SqlReader("CartonType").ToString())
            idx += 1
        End While

        '*** Add A Default Value to the Drop Down List ***
        ddlDefaultCartonType.Items.Insert(0, "Select Carton Type...")
        ddlDefaultCartonType.SelectedIndex = 0

    Finally
        '*** Always Call Close When Done Reading ***
        SqlReader.Close()
        SqlCmd.Connection.Close()
    End Try

End Sub

'Selection
Protected Sub ddlRestartCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
    selectedDefaultCarton = ddlDefaultCartonType.SelectedValue
End Sub

'Get index from DropdownList
Dim idx As Integer = ddlDefaultCartonType.Items.IndexOf(ddlDefaultCartonType.Items.FindByValue(SqlReader("DefaultCartonType").ToString))

'Set index or value selected
ddlDefaultCartonType.SelectedIndex = idx