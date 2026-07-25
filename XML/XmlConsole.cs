    
    public void buildXml()
    {
        Console.WriteLine("\n\n");

        Console.WriteLine(
            new XElement("Foo",
                new XAttribute("type", "SubmitJob"),
                new XElement("Nested", "data")));

        Console.WriteLine("\n\n");
        Foo foo = new Foo
        {
        Bar = "some and value",
        Nested = "data"
        };

        new XmlSerializer(typeof(Foo)).Serialize(Console.Out, foo);

    }
    