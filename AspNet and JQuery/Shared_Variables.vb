

Public Class RxLabConfig
    Inherits System.Web.UI.Page
    Dim ActiveLabID As String
    Dim ReportDate As String
    Dim GridView2 As GridView = New GridView()
    Dim printOption As Printer = New Printer()

    Shared localValue As String ' Shared keyword allows to use variable accros class
    Shared localSelectedOption As String ' Shared keyword allows to use variable accros class


 Private Function GetPrintData() As GridView
    Dim myDataSet As New LabConfigurationDS

    If localSelectedOption = "sap" Then
        Dim SurfaceToolsDA As New LabConfigurationDSTableAdapters.RxLSapLabConfigTableAdapter
        SurfaceToolsDA.Fill(myDataSet.RxLSapLabConfig, localValue)
        GridView1.DataSource = myDataSet.Tables("RxLSapLabConfig")
        GridView1.DataBind()
    End If

    If localSelectedOption = "labId" Then
        Dim SurfaceToolsDA As New LabConfigurationDSTableAdapters.RxLabIdConfigTableAdapter
        SurfaceToolsDA.Fill(myDataSet.RxLabIdConfig, localValue)
        GridView1.DataSource = myDataSet.Tables("RxLabIdConfig")
        GridView1.DataBind()
    End If

    Return GridView1
End Function

' *
' * ShipCanceledByCustomerAccount
' *
' *

<WebMethod>
Public Shared Function SearchButton_Click(ByVal value As String, ByVal selectedOption As String)

    ' Build query and fetch information
    Dim SqlReader As SqlDataReader = Query(value, selectedOption)
    Dim rxConfig As New List(Of RxLabConfigDto)
    ' Build list
    rxConfig = ListLabConfig(SqlReader)

    localValue = value ' Using shared variable
    localSelectedOption = selectedOption ' Using shared variable

    Return rxConfig
End Function


End Class