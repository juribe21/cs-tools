Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    Dim opcValue As String
    Dim eye As String

    If (e.Row.RowType = DataControlRowType.DataRow) Then

        Dim row As GridViewRow = GridView2.SelectedRow
        opcValue = e.Row.Cells(3).Text
        eye = DataBinder.Eval(e.Row.DataItem, "EyeType").ToString() ' ← get value from dataItem when row is a template

        ' Set a javascript method to onclick event --           pass eye as string → '" & eye & "'" ↓
        e.Row.Attributes("onclick") = "javascript:openModalOPCInfo(" & OrderTrackingID & " ,'" & eye & "');"
        e.Row.Style("cursor") = "pointer" ' ← set cursor pointer

        ' skip some rows
         If eye = "L" Or eye = "R" Then
            e.Row.Attributes("onclick") = "javascript:openModalOPCInfo(" & OrderTrackingID & " ,'" & eye & "');"
            e.Row.Style("cursor") = "pointer"
        End If

    End If
End Sub