//int countitemsCode = itemsCode.Count();
string folder = @"C:\Users\juribe\Documents\Jorge Uribe Notes\";
string fullPath = folder + "listaItems.txt";

foreach (var itc in listItemsCode)
{
    // code her ...

    string itemstext = itemsCodeStart.ItemId.ToString() + " " + itemsCodeStart.ItemCode + " " + itemsCodeStart.StandardSizeId.ToString() + " " +
            itemsCodeStart.ReplacementCost + " " + itemsCodeStart.EffectiveDate.ToString("yyyy/MM/dd") + Environment.NewLine;

    //File.WriteAllText(fullPath, itemstext);
    using (StreamWriter sw = File.AppendText(fullPath))
    {
        sw.WriteLine(itemstext);
    }
}


string.Format("{0:###,##0.00}", tag.TotalCost3) : String.Empty;
string.Format("{0:###,###,##0.###}", line.LastCreditAmount);


/// Array declaration
int[] statusTypeShipmentPickedUpOrDelivered = new[] { 2, 3, 5 };

------------
var activeWarehouses = notownedwarehouses.Union(WarehousesOwned).ToList();

foreach (WarehouseEntity warahouse in activeWarehouses)
{
    activeWarehouse.Add(DTOConversion.ConvertTo<Warehouse>(warahouse));
}
