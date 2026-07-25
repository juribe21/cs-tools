Imports System.Data.SqlClient
Imports System.Net.Http
Imports System.Net.NetworkInformation
Imports Progistics.Utility

Public Class AddAddressBook
    Inherits System.Web.UI.Page
    Dim MsgImageA As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image()
    Public SAPCustomerID As String
    Public UserAccount As String

    Dim SqlConn As New System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("RxPortal").ConnectionString)
    Dim SqlCmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand()
    Dim eventTarget As String
    Dim eventArgument As String
    Dim saveFlag As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        MsgImageA.ID = "imageA"
        MsgImageA.ImageUrl = "~/Images/Cust_Acct_Search_48x48.png"
        image_placeholder.Controls.Add(MsgImageA)

        ValidateUserAccess()

        lblAccountHeader.Text = ""
        EditShowHidden()

        Dim AddressIndex As String = Request.QueryString("AddressIndex")

        If Not Page.IsPostBack Then
            'LoadShipToNameValues(AddressIndex)
            saveFlag = True
            txtShipToCountryCode.Enabled = False

            tcAddress2.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCFF00")
            tcAddress3.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCFF00")
        Else
            'lblMsgShipToUpdated.Text = String.Empty
            txtShipToCountryCode.Enabled = False

        End If

    End Sub

    Protected Sub ValidateUserAccess()
        '*** Limit page access to Customer Service and System Admins ***
        If Session.Item("PrimaryRole") = "Administrator" Or Session.Item("PrimaryRole") = "Enterprise Admin" Or Session.Item("PrimaryRole") = "Shipping" Then
            If Session.Item("ShipPackages") = "Y" Then
            Else
                Response.Redirect("~/PageAccessDenied.aspx?Page=Admin")
            End If
        End If

    End Sub

    Private Sub LoadShipToNameValues(addressIndex As String)

        Dim SqlReader As System.Data.SqlClient.SqlDataReader = GetLastInsertedAddressIndex()
        Dim dt As DataTable = New DataTable()

        dt.Columns.Add("ShipToName")
        dt.Columns.Add("ShipToEmail")
        dt.Columns.Add("ShipToAddress1")
        dt.Columns.Add("ShipToAddress2")
        dt.Columns.Add("ShipToAddress3")

        dt.Columns.Add("ShipToCity")
        dt.Columns.Add("ShipToState")
        dt.Columns.Add("ShipToCountry")
        dt.Columns.Add("ShipToCountryCode")

        dt.Columns.Add("ShipToPostalCode")
        dt.Columns.Add("ShipToPhone")
        dt.Columns.Add("AddressIndex")

        Dim counter = 0
        Try
            'logic to know the departments that has breakages
            While SqlReader.Read()
                Dim dr = dt.NewRow()
                txtShipToName.Text = SqlReader("ShipToName").ToString
                txtAddress1.Text = SqlReader("ShipToAddress1").ToString
                txtAddress2.Text = SqlReader("ShipToAddress2").ToString
                txtAddress3.Text = SqlReader("ShipToAddress3").ToString
                txtCity.Text = SqlReader("ShipToCity").ToString
                txtState.Text = SqlReader("ShipToState").ToString
                txtCountry.Text = SqlReader("ShipToCountry").ToString
                txtShipToCountryCode.Text = AddressBookExtnsion.LoadCountryCode(txtCountry.Text) 'qlReader("ShipToCountryCode").ToString
                txtPostalCode.Text = SqlReader("ShipToPostalCode").ToString
                txtPhone.Text = SqlReader("ShipToPhone").ToString
                txtEmail.Text = SqlReader("ShipToEmail").ToString

                dt.Rows.Add(dr)
                counter = counter + 1
            End While
        Finally
            '** Always Call SQL Close When Done Reading ***
            SqlReader.Close()
            SqlCmd.Connection.Close()
            lblAccountHeader.Text = txtShipToName.Text
        End Try

    End Sub

    Private Sub EditShowHidden()
        lblHeader.Text = "Add New Address "
        lblHeader.ForeColor = System.Drawing.Color.FromArgb(13, 86, 146) ' #0D5692 - Zeiss Blue
        lblMsgShipToUpdated.Text = String.Empty
    End Sub


    Private Function GetLastInsertedAddressIndex() As SqlDataReader
        QueryString = "SELECT TOP 1 * FROM ShipAddressBook ORDER BY AddressIndex DESC"
        SqlConn.Open()
        SqlCmd = New SqlCommand(QueryString, SqlConn)
        Dim SqlReader As SqlDataReader = SqlCmd.ExecuteReader()

        Return SqlReader
    End Function


    Protected Sub SaveButton_Click(sender As Object, e As EventArgs)

        Dim ship As New ShipAddressBookDto()

        If AddressBookExtnsion.ValidateShippingName(txtShipToName.Text) Then

            lblMsgShipToUpdated.Text = $"The new Ship Address name exist in our records, please check information"
            lblMsgShipToUpdated.ForeColor = System.Drawing.Color.Red

            Return
        End If


        ship.ShipToName = txtShipToName.Text
        ship.ShipToEmail = txtEmail.Text
        ship.ShipToAddress1 = txtAddress1.Text
        ship.ShipToAddress2 = txtAddress2.Text
        ship.ShipToAddress3 = txtAddress3.Text
        ship.ShipToCity = txtCity.Text
        ship.ShipToState = txtState.Text
        ship.ShipToCountry = txtCountry.Text
        ship.ShipToCountryCode = AddressBookExtnsion.LoadCountryCode(ship.ShipToCountry) 'txtShipToCountryCode.Text
        ship.ShipToPostalCode = txtPostalCode.Text
        ship.ShipToPhone = txtPhone.Text

        'If Not AddressBookValidation.ValidateAddressBook(ship) Then
        '    Return
        'End If

        Dim errorList() As String = {}
        Dim list As List(Of String) = New List(Of String)

        list = AddressBookExtnsion.ValidateAddressBookInfo(ship)

        If list.Count > 0 Then
            lblMsgShipToUpdated.Text = $"Please check information for next fields"
            lblMsgShipToUpdated.ForeColor = System.Drawing.Color.Red

            MarkRequiredFields(list)
            saveFlag = False

        End If

        If saveFlag Then
            Try
                QueryString = $"
                            INSERT INTO ShipAddressBook
	                        VALUES(
			                        '{ship.ShipToName}',
			                        '{ship.ShipToEmail}',
			                        '{ship.ShipToAddress1}',
			                        '{ship.ShipToAddress2}',
			                        '{ship.ShipToAddress3}',
			                        '{ship.ShipToCity}',
			                        '{ship.ShipToState}',
			                        '{ship.ShipToCountry}',
			                        '{ship.ShipToCountryCode}',
			                        '{ship.ShipToPostalCode}',                           
			                        '{ship.ShipToPhone}'
		                           )
                           "
                SqlConn.Open()
                SqlCmd = New System.Data.SqlClient.SqlCommand(QueryString, SqlConn)
                SqlCmd.ExecuteNonQuery()
                lblAccountHeader.Text = ship.ShipToName

            Catch ex As Exception
                lblMsgShipToUpdated.Text = "Update process fail"
                lblMsgShipToUpdated.ForeColor = System.Drawing.Color.Red
            Finally
                If Not SqlCmd.Connection Is Nothing Then
                    SqlCmd.Connection.Close()
                    SqlCmd.Connection.Dispose()

                    lblMsgShipToUpdated.Text = $"The new Ship Address was added"
                    lblMsgShipToUpdated.ForeColor = System.Drawing.Color.FromArgb(13, 86, 146) ' #0D5692 - Zeiss Blue

                Else
                    If Not String.IsNullOrEmpty(lblMsgShipToUpdated.Text) Then
                        lblMsgShipToUpdated.ForeColor = System.Drawing.Color.Red
                    Else
                        lblMsgShipToUpdated.Text = $"The new Ship Address was not added, please check form"
                        lblMsgShipToUpdated.ForeColor = System.Drawing.Color.Red
                    End If
                End If
            End Try


        End If

    End Sub


    Private Sub MarkRequiredFields(list As List(Of String))

        'tcAddress1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        'tcAddress2.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        'tcAddress3.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        'tcShipCity.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        'tcShipState.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        'tcShipCountry.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        'tcCountryCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        'tcShipPostalCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        'tcShipPhone.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        'tcShipEmail.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")


        If list.Contains("Name") Then
            tcShipName.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("Address1") ThentxtCity
            tcAddress1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("Address1") Then
            tcAddress2.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("Address1") Then
            tcAddress3.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("City") Then
            tcShipCity.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("State") Then
            tcShipState.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("Country") Then
            tcShipCountry.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("StateCodeMustContain2") Then
            tcCountryCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("PostalCode") Then
            tcShipPostalCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("PostalCodeMustContain5") Then
            tcShipPostalCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("Phone") Then
            tcShipPhone.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("Email") Then
            tcShipEmail.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

        If list.Contains("ShippingEmailFormat") Then
            tcShipEmail.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE")
        End If

    End Sub

End Class