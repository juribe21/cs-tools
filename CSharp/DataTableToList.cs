/// Convert DataTable to List using a Generic Method
private static List<T> ConvertDataTable<T>(DataTable dt)
{
    List<T> data = new List<T>();
    foreach (DataRow row in dt.Rows)
    {
        T item = GetItem<T>(row);
        data.Add(item);
    }
    return data;
}

private static T GetItem<T>(DataRow dr)
{
    Type temp = typeof(T);
    T obj = Activator.CreateInstance<T>();

    foreach (DataColumn column in dr.Table.Columns)
    {
        foreach (PropertyInfo pro in temp.GetProperties())
        {
            if (pro.Name == column.ColumnName)
                pro.SetValue(obj, dr[column.ColumnName], null);
            else
                continue;
        }
    }
    return obj;
}

/* ***************************************************************** */

/// Convert DataTable to List Using Linq
public void StudentListUsingLink()
{
    //  DataTable dt = new DataTable("Branches");
    DataTable dt = new DataTable("Student");
    dt.Columns.Add("StudentId", typeof(Int32));
    dt.Columns.Add("StudentName", typeof(string));
    dt.Columns.Add("Address", typeof(string));
    dt.Columns.Add("MobileNo", typeof(string));
    //Data
    dt.Rows.Add(1, "Manish", "Machanda", "0000000000");
    dt.Rows.Add(2, "Jorge", "Uribe", "111111111");
    dt.Rows.Add(3, "Carl", "Zeiis", "1222222222");
    dt.Rows.Add(4, "Diciembre", "Navidad", "3333333333");
    
    
    List<Student> studentList = new List<Student>();
    studentList = (from DataRow dr in dt.Rows
                   select new Student()
                   {
                       StudentId = Convert.ToInt32(dr["StudentId"]),
                       StudentName = dr["StudentName"].ToString(),
                       Address = dr["Address"].ToString(),
                       MobileNo = dr["MobileNo"].ToString()
                   }).ToList();


// different way from datatable → dtPastDueOrders
 var Lista = dtPastDueOrders.AsEnumerable().Select(row =>
  new PastDueOrders2
  {
      PastDue01DayCount = row.Field<int>("PastDue01DayCount").ToString(),
      PastDue02DayCount = row.Field<int>("PastDue02DayCount").ToString(),
      PastDue03DayCount = row.Field<int>("PastDue03DayCount").ToString(),
      PastDue04DayCount = row.Field<int>("PastDue04DayCount").ToString(),
      PastDue05DayCount = row.Field<int>("PastDue05DayCount").ToString(),
      PastDue06DayCount = row.Field<int>("PastDue06DayCount").ToString(),
      PastDue07DayCount = row.Field<int>("PastDue07DayCount").ToString(),
      PastDue08DayCount = row.Field<int>("PastDue08DayCount").ToString(),
      PastDue09DayCount = row.Field<int>("PastDue09DayCount").ToString(),
      PastDue10DayCount = row.Field<int>("PastDue10DayCount").ToString(),
      PastDue01DayPercent = row.Field<decimal>("PastDue01DayPercent").ToString(),
      PastDue02DayPercent = row.Field<decimal>("PastDue02DayPercent").ToString(),
      PastDue03DayPercent = row.Field<decimal>("PastDue03DayPercent").ToString(),
      PastDue04DayPercent = row.Field<decimal>("PastDue04DayPercent").ToString(),
      PastDue05DayPercent = row.Field<decimal>("PastDue05DayPercent").ToString(),
      PastDue06DayPercent = row.Field<decimal>("PastDue06DayPercent").ToString(),
      PastDue07DayPercent = row.Field<decimal>("PastDue07DayPercent").ToString(),
      PastDue08DayPercent = row.Field<decimal>("PastDue08DayPercent").ToString(),
      PastDue09DayPercent = row.Field<decimal>("PastDue09DayPercent").ToString(),
      PastDue10DayPercent = row.Field<decimal>("PastDue10DayPercent").ToString(),
      PastDueTotalCount = Convert.ToInt32(row.Field<int>("PastDueTotalCount").ToString())

  }).ToList();

}

public class Student
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public string Address { get; set; }
    public string MobileNo { get; set; }
}