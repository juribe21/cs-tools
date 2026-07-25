
/// Implemented in: CustomerMiscellaneousCreditEntityAccessor
/// CreateCustomerMiscellaneousCredit - METHOD
/// #13174 - Changeset 14175

// input var creditIds = new int[] { 7, 13, 8, 12, 10, 11, 14 };
public int GetGapAmongNumerationOne(int[] creditIds)
{
    List<int> list = creditIds.Select(s => s).OrderByDescending(i => i).ToList();

    /// Option 1
    var result1 = Enumerable.Range(list.Min(), list.Count).Except(list).First(); // 9
                                                                                 // or
    /// Option 2
    int min = list.Min();
    int max = list.Max();
    var result = Enumerable.Range(min, max - min + 1).Except(list).First();// 9

    return result;
}


//Input: var strings = new string[] { "7", "13", "8", "12", "10", "11", "14" };
public int GetGapAmongNumerationTwo(string[] creditIds)
{
    /// Step One get numbers and conver to int
    var list1 = Array.ConvertAll(creditIds, s => Int32.Parse(s)).OrderBy(i => i);
    // or 
    /// get numbers and conver to int
    var list2 = creditIds.Select(s => int.Parse(s)).OrderBy(i => i);
    // or
    /// get numbers and conver to int
    var creditIdslist = creditIds.OrderBy(s => int.Parse(s));

    /// Step Two
    /// Option 1
    var result1 = Enumerable.Range(list2.Min(), list2.Count()).Except(list2).First(); // 9
    // or
    /// Option 2
    int min = list2.Min();
    int max = list2.Max();
    var result = Enumerable.Range(min, max - min + 1).Except(list2).First();// 9

    return result;
}
