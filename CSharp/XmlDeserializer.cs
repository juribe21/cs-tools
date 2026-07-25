
public static class CzvDeserializer
{
    public static T DeserializeFromXml<T>(string xml)
    {
        T result;

        var ser = new XmlSerializer(typeof(T));
        using (var tr = new StringReader(xml))
        {
            result = (T)ser.Deserialize(tr);
        }
        return result;
    }

}
