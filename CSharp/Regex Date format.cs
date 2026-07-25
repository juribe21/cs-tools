/* Regex Date format */

private bool IsCorrectFormat(string strDate)
{
    Regex format1 = new Regex(@"\d{1}/\d{1}/\d{4}"); // M/d/yyyy   e.g. 07/07/2017
    Regex format2 = new Regex(@"\d{2}/\d{1}/\d{4}"); // MM/d/yyyy  e.g. 7/1/2017
    Regex format3 = new Regex(@"\d{2}/\d{2}/\d{4}"); // MM/dd/yyyy e.g. 07/01/2017
    Regex format4 = new Regex(@"\d{1}/\d{2}/\d{4}"); // M/dd/yyyy  e.g. 7/07/2017

    return format1.IsMatch(strDate) || format2.IsMatch(strDate) || format3.IsMatch(strDate) || format4.IsMatch(strDate);
}