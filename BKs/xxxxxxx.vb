Public Class EntAdminHome
    Inherits System.Web.UI.Page
    Dim OrderTrackingID As String
    Dim ActiveLabID As String
    Dim TrayNumber As String
    Public ArrayCustomerSap As String() = {}
    Public SapCompany As String = "ALL"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SapCompany = DirectoryFunctionsClass.GetSapCompanyByLabID(Session("LabID"))

        ActiveLabID = Session("LabID")
        ArrayCustomerSap = DirectoryFunctionsClass.getCustomerInfoBySapCompany(3, SapCompany)

        If Page.IsPostBack Then
            '*** If there is no serch value then get the next page ***'
            If RxSearchBox.Text = String.Empty Then
                '*** Process the search value ***'
                RxSearchBox.Text = Nothing
                GetShipCarrierCutoff()
            Else
                '*** Process the search value ***'
                Response.Redirect("RxOrderSearch.aspx?SearchValue=" + RxSearchBox.Text)
            End If
        Else
            GetShipCarrierCutoff()
        End If
    End Sub
    Protected Sub SearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click

        'Value sent using HttpResponse
        Response.Redirect("RxOrderSearch.aspx?SearchValue=" + RxSearchBox.Text)

    End Sub

    Protected Sub GetShipCarrierCutoff()

        Dim myDataSet As New ShipCarrierControlDS
        Dim ShipingCarrierDA As New ShipCarrierControlDSTableAdapters.ShipCarrierControlTableAdapter

        ShipingCarrierDA.Fill(myDataSet.ShipCarrierControl, ActiveLabID)

        GridView1.DataSource = myDataSet.Tables("ShipCarrierControl")
        GridView1.DataBind()

    End Sub
End Class