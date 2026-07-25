

PaymentMethod += Utilities.PaymentType(Convert.ToInt32(input.PaymentType))

Public Sub GetDictionaryValues()
    If input.PaymentType.HasValue AndAlso input.PaymentType > 0 Then
        Dim PaymentMethod As String = "Payment Method: "

        If Utilities.PaymentType.ContainsKey(If(input.PaymentType, 0)) Then
            PaymentMethod += Utilities.PaymentType(Convert.ToInt32(input.PaymentType))
        End If

        Dim rowPaymentMethod = New HeaderRowDefinition()
        Dim reportPaymentMethod = New HeaderCellDefinition With {
            .CellIndex = 0,
            .CellValue = PaymentMethod,
            .TextAlignment = CellTextHorizontalAlignment.Left
        }
        rowPaymentMethod.RowIndex = Math.Min(System.Threading.Interlocked.Increment(rowIndex), rowIndex - 1)
        rowPaymentMethod.RowCells.Add(reportPaymentMethod)
        exportCustomerPaymentsResponseModel.HeaderData.Rows.Add(rowPaymentMethod)
    End If
End Sub

Module Utilities
    Public Shared ReadOnly PaymentType As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)() From {
        {1, "Cash"},
        {2, "Credit Card"},
        {3, "Debit Card"},
        {4, "Check"},
        {5, "ACH"},
        {6, "eCheck"},
        {7, "Other"},
        {8, "Business Check"},
        {9, "Account Credit"}
    }
End Module