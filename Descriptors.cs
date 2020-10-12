using System;
using System.Xml.Linq;

public class ObjectDescriptor
{
    public string Name { get; private set; }
    public string Class { get; private set; }
    public int Health { get; private set; }

    /// <summary>
    /// Reads every property of a object xml.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="element"></param>
    public ObjectDescriptor(string name, XElement element)
    {
        Name = name;
        Health = element.Element("Health") != null ? int.Parse(element.Element("Health").Value) : -1;
        Console.WriteLine("ObjectDescriptor created for entity: " + name + " Object has " + Health + " HP!");
    }
}