Protected Sub ValidateUserAccess()
    '*** Limit page access to Customer Service and System Admins ***

    If HttpContext.Current.Session.Item("ShipPackages") <> "Y" Then
        HttpContext.Current.Response.Redirect("~/PageAccessDenied.aspx?Page=CustServ")
    End If
End Sub