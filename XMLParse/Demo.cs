using System.Xml;
using System;

public class Demo
{
	public static void Main (string[] args)
	{
		String yourXmlString = System.IO.File.ReadAllText( "basic.xml" );

		EnvironmentParser parser = new EnvironmentParser ();
		
		//Load from a string
		Environment envBar = parser.ParseFromString(yourXmlString);
		Console.WriteLine ("Environment: " + envBar);

		//Or load from file
		String inputFilename = "basic.xml";
		Environment envFoo = parser.ParseFromFile (inputFilename);
		//Console.WriteLine ("Environment: " + envFoo);
		
		//Serialize the Environment object back to xml.
		EnvironmentXMLWriter w = new EnvironmentXMLWriter ();
		String xmlStr = w.Write (envFoo);
		Console.WriteLine ("Regenerated XML: " + xmlStr);
	}

}