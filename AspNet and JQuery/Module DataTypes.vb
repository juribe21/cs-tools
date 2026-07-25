Module DataTypes
    Public Const _int32 As String = "Int32"
    Public Const _decimal As String = "Decimal"
    Public Const _string As String = "string"
    Public Const _bool As String = "bool"
End Module

Public Function WipByCustomerGroup() As List(Of WipByCustomer)
    If dataType.Name = DataTypes._int32 Then
        pastDueOrder.PastDueOrderValue = dtPastDueOrders.Rows(0).Field(Of Integer)(pastDueOrder.PastDueOrderName).ToString()
    End If
End Function