using System.Xml;
using System;

public class Demo
{
	public static void Main (string[] args)
	{
		String filename = "basic.xml";
		EnvironmentParser parser = new EnvironmentParser ();
		Environment env = parser.ParseFromFile (filename);
		
		// Environment ez = parser.Parse (xmlDoc, nsman);
		Console.WriteLine ("Environment: " + env);
		//Serialize the Environment object back to xml.
		EnvironmentXMLWriter w = new EnvironmentXMLWriter ();
		String xmlGenerated = w.Write (env);
		Console.WriteLine ("Regenerated XML: " + xmlGenerated);		
	}
	
}
