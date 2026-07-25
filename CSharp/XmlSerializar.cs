public static class MySerializer<T> where T : class
{
    public static string Serialize(T obj)
    {
        // Step 1: Remove or replace default namespaces
        var xsn = new XmlSerializerNamespaces();

        try
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            // Step 2: Remove or replace default namespaces
            xsn.Add(string.Empty, string.Empty);

            using (var sww = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
                {
                    xsSubmit.Serialize(writer, obj, xsn); //Step 3 add as an argument
                    return sww.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return null;
        }
    }
}


public class XmlSerializer()
{
    // Step 1: Remove or replace default namespaces              
    var xns = new XmlSerializerNamespaces();
    // Step 1: Serialize Object
    var xmlSerializer = new XmlSerializer(typeof(CZVCalcData));
    // Step 2: Remove or replace default namespaces              
    xns.Add(string.Empty, string.Empty);
    var xml = ""; // xml Output

    // Step 2: Serialize Object
    using (var sww = new StringWriter())
    {
        using (XmlWriter writer = XmlWriter.Create(sww))
        {
            xmlSerializer.Serialize(writer, czv, xns); //Step 3 add as an argument
            xml = sww.ToString(); // Our XMLs
        }
    }

    // Load as XML Document
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.LoadXml(xml);

}