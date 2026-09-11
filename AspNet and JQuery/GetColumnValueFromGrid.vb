 Protected Sub btnRxNumberDetails(ByVal sender As Object, ByVal e As EventArgs)
     'Get the button that raised the event
     Dim btn As Button = CType(sender, Button)

     'Get the row that contains this button
     Dim grv As GridViewRow = CType(btn.NamingContainer, GridViewRow)

     ' Get the cell value grv.Cells([Position])
     Dim rxNumber As String = grv.Cells(0).Text
     Response.Redirect("~/CustomerService/Credits/RxOrderCredit.aspx?OrderID=" & rxNumber)
 End Sub