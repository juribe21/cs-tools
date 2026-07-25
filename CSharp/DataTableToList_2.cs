public List<PastDueOrders2> PastDueOrdersTotalCount()
{
    DataTable dtPastDueOrders;
    ActiveLabID = "0110";

    List<PastDueOrders2> miLista = new List<PastDueOrders2>();
    string sp_commandCalculationStatus = @"SELECT LPDS.PastDue01DayCount, LPDS.PastDue01DayPercent, 
                                            LPDS.PastDue02DayCount, LPDS.PastDue02DayPercent, 
                                            LPDS.PastDue03DayCount, LPDS.PastDue03DayPercent, 
                                            LPDS.PastDue04DayCount, LPDS.PastDue04DayPercent,
                                            LPDS.PastDue05DayCount, LPDS.PastDue05DayPercent,
                                            LPDS.PastDue06DayCount, LPDS.PastDue06DayPercent,
                                            LPDS.PastDue07DayCount, LPDS.PastDue07DayPercent, 
                                            LPDS.PastDue08DayCount, LPDS.PastDue08DayPercent,
                                            LPDS.PastDue09DayCount, LPDS.PastDue09DayPercent, 
                                            LPDS.PastDue10DayCount, LPDS.PastDue10DayPercent,
                                            LPDS.PastDueTotalCount
                                          FROM LabPastDueSummary  LPDS
                                          WHERE LPDS.LabID = '" + ActiveLabID + @"'";

    dtPastDueOrders = _dataHelper.ExecuteSelect(sp_commandCalculationStatus, CommandType.Text);

    
    // "PastDueOrders2" this object should contain the same names returned by dataset or call
    List<PastDueOrders2> list =  ConvertTo<PastDueOrders2>(dtPastDueOrders);

    return miLista = Lista;
}



public List<T> ConvertTo<T>(DataTable datatable) where T : new()
{
    List<T> Temp = new List<T>();
    try
    {
        List<string> columnsNames = new List<string>();
        foreach (DataColumn DataColumn in datatable.Columns)
            columnsNames.Add(DataColumn.ColumnName);
        Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
        return Temp;
    }
    catch
    {
        return Temp;
    }

}


    public T getObject<T>(DataRow row, List<string> columnsName) where T : new()
    {
        T obj = new T();
        try
        {
            string columnname = "";
            string value = "";
            PropertyInfo[] Properties;
            Properties = typeof(T).GetProperties();
            foreach (PropertyInfo objProperty in Properties)
            {
                columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                if (!string.IsNullOrEmpty(columnname))
                {
                    value = row[columnname].ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                        {
                            value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                            objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                        }
                        else
                        {
                            value = row[columnname].ToString().Replace("%", "");
                            objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                        }
                    }
                }
            }
            return obj;
        }
        catch
        {
            return obj;
        }
    }
