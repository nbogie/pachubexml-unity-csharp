using System.Xml;
using System;

public class Demo
{
	public static void Main (string[] args)
	{
		String inputFilePath =  "../../TestFiles/basic.xml" ;
		String yourXmlString = System.IO.File.ReadAllText( inputFilePath);

		EnvironmentParser parser = new EnvironmentParser ();
		
		//Load from a string
		Environment envBar = parser.ParseFromString(yourXmlString);
		Console.WriteLine ("Environment: " + envBar);

		//Or load from file
		Environment envFoo = parser.ParseFromFile (inputFilePath);
		//Console.WriteLine ("Environment: " + envFoo);
		
		//Serialize the Environment object back to xml.
		EnvironmentXMLWriter w = new EnvironmentXMLWriter ();
		String generatedXMLStr = w.Write (envFoo);
		Console.WriteLine ("Regenerated XML: " + generatedXMLStr);
		
		//Check we can parse the string we just generated.
		Environment envBaz = parser.ParseFromString (generatedXMLStr);
		Console.WriteLine ("Parsing our generated xml: " + envBaz);
	}
}
