 
 Referencia: ListCustomerAccts.aspx.vb
 
 ** BackEnd **
 If (GridView1.Rows.Count > 0) Then
     DivScroll.Style.Add("width", "1370px")
     DivScroll.Style.Add("height", "553px")
     DivScroll.Style.Add("overflow", "auto")

 End If

  <div runat="server" id="DivScroll">
    ...
  </div>