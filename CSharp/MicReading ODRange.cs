
/// private string MicReading - TagEntityAccessor
private string MicReading(TagSizeDetail item, ref string totalMicReading)
{
    List<decimal?> micReadings = new List<decimal?>();
    List<decimal?> micReadingList = new List<decimal?>();

    decimal? itemMicReading = 0M;
    string micReading = string.Empty;

    micReadings.Add(item.MicReading1);
    micReadings.Add(item.MicReading2);
    micReadings.Add(item.MicReading3);
    micReadings.Add(item.MicReading4);
    micReadings.Add(item.MicReading5);
    micReadings.Add(item.MicReading6);
    micReadings.Add(item.MicReading7);
    micReadings.Add(item.MicReading8);
    micReadings.Add(item.MicReading9);
    micReadings.Add(item.MicReading10);
    micReadings.Add(item.MicReading11);
    micReadings.Add(item.MicReading12);

    foreach (var mic in micReadings)
    {
        if (mic > 0)
        {
            itemMicReading = mic;
            micReadingList.Add(mic);
        }
    }

    if (micReadingList.Count == 1)
    {
        totalMicReading = "1";
        if (itemMicReading < 1)
        {
            micReading = string.Format("{0:##0.000000}", itemMicReading);
        }
        else
        {
            micReading = string.Format("{0:##0.######}", itemMicReading);
        }
        return micReading + "\'\'";
    }
    if (micReadingList.Count > 1)
    {
        // get first and last value of the list
        totalMicReading = "12";
        micReadingList = micReadingList.OrderBy(x => x.Value).ToList();

        decimal? micLowest = micReadingList.First();
        decimal? micHighest = micReadingList.Last();
        micReading = string.Format("{0:##0.000}", micLowest + "\'\'") + " - " + string.Format("{0:##0.000}", micHighest + "\'\'");
        return micReading;
    }
    else
    {
        return string.Empty;
    }
}

/// private string MicReading - TagEntityAccessor
private string MicReading(List<decimal?> micReadings, ref string totalMicReading)
{
    List<decimal?> micReadingList = new List<decimal?>();
    decimal? itemMicReading = 0M;
    string micReading = string.Empty;

    foreach (var mic in micReadings)
    {
        if (mic > 0)
        {
            itemMicReading = mic;
            micReadingList.Add(mic);
        }
    }

    if (micReadingList.Count == 1)
    {
        totalMicReading = "1";
        if (itemMicReading < 1)
        {
            micReading = string.Format("{0:##0.000000}", itemMicReading);
        }
        else
        {
            micReading = string.Format("{0:##0.######}", itemMicReading);
        }
        return micReading + "\'\'";
    }
    if (micReadingList.Count > 1)
    {
        // get first and last value of the list
        totalMicReading = "12";
        micReadingList = micReadingList.OrderBy(x => x.Value).ToList();

        decimal? micLowest = micReadingList.First();
        decimal? micHighest = micReadingList.Last();
        micReading = string.Format("{0:##0.000}", micLowest + "\'\'") + " - " + string.Format("{0:##0.000}", micHighest + "\'\'");
        return micReading;
    }
    else
    {
        return string.Empty;
    }
}

/* *********************************************************************** */
/// Build a list of decimals
List<decimal?> micReadingList = new List<decimal?>()
{
    tagInfo.MicReading1,  tagInfo.MicReading2, tagInfo.MicReading3,
    tagInfo.MicReading4,  tagInfo.MicReading5, tagInfo.MicReading6,
    tagInfo.MicReading7,  tagInfo.MicReading8, tagInfo.MicReading9,
    tagInfo.MicReading10, tagInfo.MicReading11,tagInfo.MicReading12
};


string totalMicReading = string.Empty;
// MicReading - pass a list of decimals to method
string micReading = MicReading(micReadingList, ref totalMicReading);

if (string.IsNullOrEmpty(totalMicReading))
{
    tagInfo.ODRange = string.Empty;
}
else if (!string.IsNullOrEmpty(micReading) && totalMicReading == "1") //only one reading
{
    tagInfo.ODRange = micReading;
}
else if (!string.IsNullOrEmpty(micReading) && totalMicReading == "12") // more than one reading 
{
    tagInfo.ODRange = micReading;
}
