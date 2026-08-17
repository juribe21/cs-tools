' ******************* Bind DropDownList to Code  behaind *******************

' Set DropDownList with AutoPostBack and set event OnTextChanged
<asp:DropDownList ID="ddlCriteria" runat="server" OnTextChanged="btnSelectedCriteria" CssClass="select2-dropdown" AutoPostBack="true"></asp:DropDownList>


' inherit event with Handles ddlCriteria.SelectedIndexChanged
Protected Sub btnSelectedCriteria(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCriteria.SelectedIndexChanged

     selectedOption = ddlCriteria.SelectedItem.Text
     localSelectedOption = selectedOption
     BuildReduceProductoCriteria(selectedOption)
 End Sub

' bind DropDownList with DataTable
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

    ' Add Select option before to fill from datatable
    ddlReduceProductoCriteria.Items.Add("SELECT")

    For Each row As DataRow In dt.Rows

        Dim value As String = row("Option").ToString()
        ddlReduceProductoCriteria.Items.Add(value)
    Next
    ddlReduceProductoCriteria.DataBind()

End Sub

' **************************************
' Fill ddl manually
 Protected Sub LoadddlSurfacingLab()
     ddlSurfacingLab.Items.Add("Y")
     ddlSurfacingLab.Items.Add("N")
 End Sub

' Get text or value from ddl
Protected Sub ddlSurfacingLab_SelectedIndexChanged(sender As Object, e As EventArgs)
    selectedSurfacingLab = ddlSurfacingLab.SelectedValue
    EditShowHidden()
End Sub


'Get Index
Dim idx As Integer = ddlDefaultCartonType.Items.IndexOf(ddlDefaultCartonType.Items.FindByValue(SqlReader("DefaultCartonType").ToString))
