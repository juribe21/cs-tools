
/// Distinct

/// Get a list - with duplicate values
IQueryable<ItemsCode> itemsCode = (from itm in Context.ItemEntities
                                   join iss in Context.ItemStandardSizeEntities on itm.ItemId equals iss.ItemId into iss1
                                   from ss in iss1.DefaultIfEmpty()
                                   join cat in Context.CategoryEntities on itm.CategoryId equals cat.CategoryId
                                   join rc in Context.ReplacementCostEntities on itm.ItemId equals rc.ItemId
                                   join wh in Context.WarehouseEntities on rc.WarehouseId equals wh.WarehouseId
                                   where itm.ItemCode.StartsWith(itemCodeBeginsWith) && rc.WarehouseId == warehouseId && (ss.StandardSizeId > 0 || ss.StandardSizeId == rc.ItemStandardSizeId)
                                   select new ItemsCode
                                   {
                                       ItemId = itm.ItemId,
                                       ItemCode = itm.ItemCode,
                                       BriefDescription = itm.BriefDescription,
                                       StandardSizeId = ss.StandardSizeId,
                                       ImperialDescription = ss.ImperialDescription,
                                       MetricDescription = ss.MetricDescription,
                                       ReplacementCost = rc.Cost,
                                       ReplacementCostUOM = rc.CostUOM,
                                       EffectiveDate = rc.EffectiveDate,
                                       CatSortSequence = cat.SortSequence,
                                       Description = cat.Description,
                                       ItemSortSequence = itm.SortSequence,
                                       ImperialWidth = ss.ImperialWidth,
                                       ImperialLength = ss.ImperialLength
                                   });

// Check list is not empty
if (!itemsCode.Any())
{
    return null;
}

// Select disctinct
var listItemsCode = (from ic in itemsCode
                     where ic.StandardSizeId > 0
                     orderby ic.EffectiveDate descending
                     select ic).GroupBy(g => g.StandardSizeId).Select(x => x.FirstOrDefault());


// -----------  select()GroupBy()--------------
var listItemsCode = (from ic in itemsCode
                     where ic.StandardSizeId > 0
                     orderby ic.EffectiveDate
                     select ic).GroupBy(g => g.StandardSizeId).Select(x => x.FirstOrDefault());

// union linq: https://dotnettutorials.net/lesson/linq-union-method/

---------------------------------------------------------------------------------------------
// distict linq: https://dotnettutorials.net/lesson/linq-distinct-method/
var distinctJurisdictions = (from inv in invoices select inv.SalesJurisdictionId).Distinct();

// Distinct list of strings
return TargetedDeliveryOrShipOrPickUpDate = (from t in TargetedDeliveryOrShipOrPickUpDate select t).Distinct().ToList();



/// **** DTOConversion
var activeWarehouses = notownedwarehouses.Union(WarehousesOwned).ToList();

foreach (WarehouseEntity warahouse in activeWarehouses)
{
    activeWarehouse.Add(DTOConversion.ConvertTo<Warehouse>(warahouse));
}

// ----------------------IMPLEMENT DISTINCT------------------
// https://www.tutorialsteacher.com/linq/linq-set-operators-distinct


/* The Distinct extension method doesn't compare values of complex type objects. You need to implement IEqualityComparer<T> interface in order to compare the values of complex types */
public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
}

class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.StudentID == y.StudentID
                && x.StudentName.ToLower() == y.StudentName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.StudentID.GetHashCode();
    }
}

// Step 2
IList<Student> studentList = new List<Student>() {
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
    };


var distinctStudents = studentList.Distinct(new StudentComparer());

foreach (Student std in distinctStudents)
    Console.WriteLine(std.StudentName);