Private Sub SetCookiesExpiredII()

    Dim cookies As HttpCookieCollection = Request.Cookies

    If (Request.Cookies.Count > 0) Then
        Dim myCookie As HttpCookie
        myCookie = New HttpCookie("UserNetworkID")

        myCookie.Expires = DateTime.Now.AddDays(-100D)
        Response.Cookies.Add(myCookie)
    End If
End Sub
