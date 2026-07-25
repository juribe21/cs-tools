/* ***** Process works ***** */

public List<WaitingForFrame2> LoadAwaitingFrameDatesData()
{
    DataTable dtPastDueOrders;
    ActiveLabID = "0975";
    List<WaitingForFrame2> miLista = new List<WaitingForFrame2>();

    // Get row as DataTable
    dtPastDueOrders = _dataHelper.ExecuteSelect(sp_commandCalculationStatus, CommandType.Text);
    int index = 0;

    // Conver to list
    foreach (DataColumn column in dtPastDueOrders.Columns)
    {
        WaitingForFrame2 waitingFor = new WaitingForFrame2();
        // Get type of column
        Type dataType = dtPastDueOrders.Columns[index].DataType;

        // Get column name
        waitingFor.ColumnName = column.ColumnName;

        if (dataType.Name == "Int32")
        {
            // Get Value
            waitingFor.ColumnValue = dtPastDueOrders.Rows[0].Field<int>(waitingFor.ColumnName).ToString();
        }
        else if (dataType.Name == "Decimal")
        {
            // Get Value
            waitingFor.ColumnValue = dtPastDueOrders.Rows[0].Field<decimal>(waitingFor.ColumnName).ToString();
        }

        miLista.Add(waitingFor);
        index++;
    }

    return miLista;
}