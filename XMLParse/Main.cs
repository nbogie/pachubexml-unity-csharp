using System.Xml;
using System;
 
public class Demo
{
	public static void Main (string[] args)
	{
		String filename = "basic.xml";
		XmlDocument xmlDoc = new XmlDocument();	    
		Boolean loadedOk = false;
		try
		{
		    xmlDoc.Load(filename);
			Console.WriteLine ("read xml file ok: "+filename);
			loadedOk = true;
		}
		catch (XmlException e)
		{
		    Console.WriteLine(e.Message);			
		}
		catch (System.IO.FileNotFoundException e)
		{
		    Console.WriteLine(e.Message);			
		}
		
		if (loadedOk)
		{
			XmlNamespaceManager nsman= new XmlNamespaceManager(xmlDoc.NameTable);
			nsman.AddNamespace("ns", xmlDoc.DocumentElement.NamespaceURI);
			
			//From the xml doc, create an Environment.
			EnvironmentParser parser = new EnvironmentParser();
			Environment ez = parser.Parse(xmlDoc, nsman);
			Console.WriteLine ("Environment: " + ez);
			
			//Serialize the Environment object back to xml.
			EnvironmentXMLWriter w = new EnvironmentXMLWriter();
			Console.WriteLine("Regenerated XML: " + w.Write(ez));
		}
		
    }
}
