
[XmlElementAttribute(IsNullable = true)]
[XmlElementAttribute(IsNullable = false)]

/* SPECS */

/*
TagUDF5 = Tag.UDF5_Double (format with commas and suppress trailing zeros)
TagUDF6 = Tag.UDF6_Double (format with commas and suppress trailing zeros)
TagUDF7 = Tag.UDF7_Int (format as a whole number with commas)
TagUDF8 Tag.UDF8_Int (format as a whole number with commas)
TagUDF9 = Tag.UDF9_Date (format as a date)
TagUDF10 = Tag.UDF10_Date (format as a date)

*/

/* String.Join */
string selectedFields = String.Join(",", SelectFields);


// *File Path
string filePath = string.Concat(@"C:\inetpub\Capstone_WS_Dev\Services\ImportExport\");

private bool IsCorrectFormat(string strDate)
{
    Regex format1 = new Regex(@"\d{1}/\d{1}/\d{4}"); // M/d/yyyy   e.g. 07/07/2017
    Regex format2 = new Regex(@"\d{2}/\d{1}/\d{4}"); // MM/d/yyyy  e.g. 7/1/2017
    Regex format3 = new Regex(@"\d{2}/\d{2}/\d{4}"); // MM/dd/yyyy e.g. 07/01/2017
    Regex format4 = new Regex(@"\d{1}/\d{2}/\d{4}"); // M/dd/yyyy  e.g. 7/07/2017

    return format1.IsMatch(strDate) || format2.IsMatch(strDate) || format3.IsMatch(strDate) || format4.IsMatch(strDate);
}

// Percentage Format
string.Format("{0:P2}", obj.MarginPercentage)

switch (val)
{
    case 1:
        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.TransactionDate.Value.ToString("M/d/yyyy"));
        cellnum++;
        break;
    case 2:
        CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, item.DueDate.Value.ToString("M/d/yyyy"));
        cellnum++;
        break;
    case 24:
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF5Label))
        {
            CreateCell(detailRow, cellnum, normalStyleTextAlignmentRight, string.Format("{0:###,###,##0.00}", tag.UDF5_Double));
            cellnum++;
        }
        break;
    case 25:
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF6Label))
        {
            CreateCell(detailRow, cellnum, normalStyleTextAlignmentRight, string.Format("{0:###,###,###0.00#}", tag.UDF6_Double));
            cellnum++;
        }
        break;
    case 26:
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF7Label))
        {
            CreateCell(detailRow, cellnum, normalStyleTextAlignmentRight, string.Format("{0:###,###,###}", tag.UDF7_Int));
            cellnum++;
        }
        break;
    case 27:
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF8Label))
        {
            CreateCell(detailRow, cellnum, normalStyleTextAlignmentRight, string.Format("{0:###,###,###}", tag.UDF8_Int));
            cellnum++;
        }
        break;
    case 28:
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF9Label))
        {
            CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, tag.UDF9_Date.HasValue ? tag.UDF9_Date.Value.ToString("MM/dd/yyyy") : string.Empty);
            cellnum++;
        }
        break;
    case 29:
        if (!string.IsNullOrEmpty(data.InventoryInitData.TagUDF10Label))
        {
            CreateCell(detailRow, cellnum, normalStyleTextAlignmentLeft, tag.UDF10_Date.HasValue ? tag.UDF10_Date.Value.ToString("MM/dd/yyyy") : string.Empty);
            cellnum++;
        }
    case 30: // Margin Percentage 
        CreateCell(detailRow, cellnum, doubleCellStyleFixedTwoDecimal, string.Format("{0:P2}", obj.MarginPercentage));
        cellnum++;
        break;
        break;
}



[XmlElementAttribute(IsNullable = true)]
[XmlElementAttribute(IsNullable = false)]

// **************** Format - Result *********************** //

Format Result
DateTime.Now.ToString("MM/dd/yyyy")	05 / 29 / 2015
DateTime.Now.ToString("dddd, dd MMMM yyyy") Friday, 29 May 2015
DateTime.Now.ToString("dddd, dd MMMM yyyy")	Friday, 29 May 2015 05:50
DateTime.Now.ToString("dddd, dd MMMM yyyy") Friday, 29 May 2015 05:50 AM
DateTime.Now.ToString("dddd, dd MMMM yyyy") Friday, 29 May 2015 5:50
DateTime.Now.ToString("dddd, dd MMMM yyyy") Friday, 29 May 2015 5:50 AM
DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")    Friday, 29 May 2015 05:50:06
DateTime.Now.ToString("MM/dd/yyyy HH:mm")   05 / 29 / 2015 05:50
DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")    05 / 29 / 2015 05:50 AM
DateTime.Now.ToString("MM/dd/yyyy H:mm")    05 / 29 / 2015 5:50
DateTime.Now.ToString("MM/dd/yyyy h:mm tt") 05 / 29 / 2015 5:50 AM
DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")    05 / 29 / 2015 05:50:06
DateTime.Now.ToString("MMMM dd")    May 29
DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK")	2015 - 05 - 16T05: 50:06.7199222 - 04:00
DateTime.Now.ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘GMT’") Fri, 16 May 2015 05:50:06 GMT
DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss")  2015 - 05 - 16T05: 50:06
DateTime.Now.ToString("HH:mm")  05:50
DateTime.Now.ToString("hh:mm tt")   05:50 AM
DateTime.Now.ToString("H:mm")   5:50
DateTime.Now.ToString("h:mm tt")    5:50 AM
DateTime.Now.ToString("HH:mm:ss")   05:50:06
DateTime.Now.ToString("yyyy MMMM")  2015 May


// **************** Decimal Format - Result *********************** //
double floating = 72.948615;

Console.WriteLine("P02: {0}", (floating / 100).ToString("P02", ci));
Console.WriteLine("P01: {0}", (floating / 100).ToString("P01", ci));
Console.WriteLine("P: {0}", (floating / 100).ToString("P", ci));
Console.WriteLine("P0: {0}", (floating / 100).ToString("P0", ci));
Console.WriteLine("P1: {0}", (floating / 100).ToString("P1", ci));
Console.WriteLine("P3: {0}", (floating / 100).ToString("P3", ci));

Output:
"P02: 72.95%"
"P01: 72.9%"
"P: 72.95%"
"P0: 72%"
"P1: 72.9%"
"P3: 72.949%"


// **************** Negative Format - Result *********************** //
string summaryFormat = "###,###,###0.###; (###,###,###0.00#)";


string fmt1 = "#,##0.00";
string fmt2 = "#,##0.00;(#,##0.00)";
double posAmount = 12345.67;
double negAmount = -12345.67;
Console.WriteLine("posAmount.ToString(fmt1) returns " + posAmount.ToString(fmt1));
Console.WriteLine("negAmount.ToString(fmt1) returns " + negAmount.ToString(fmt1));
Console.WriteLine("posAmount.ToString(fmt2) returns " + posAmount.ToString(fmt2));
Console.WriteLine("negAmount.ToString(fmt2) returns " + negAmount.ToString(fmt2));

Output:
posAmount.ToString(fmt1) returns 12,345.67
negAmount.ToString(fmt1) returns - 12,345.67
posAmount.ToString(fmt2) returns 12,345.67
negAmount.ToString(fmt2) returns(12, 345.67)

// Military Format
line.PostedDate.Value.ToString("M/d/yyyy HH:mm:ss"));

//standard time - AM and PM
line.PostedDate.Value.ToString("M/d/yyyy h:mm tt"));

// Calculate difference between two dates (number of days)?
//Assuming StartDate and EndDate are of type DateTime:
var X = (EndDate - StartDate).TotalDays


[XmlElementAttribute(IsNullable = true)]
[XmlElementAttribute(IsNullable = false)]