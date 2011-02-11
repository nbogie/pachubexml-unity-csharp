pachubexml-unity-csharp
=======================
This is a bare-bones library to help work with Pachube XML documents in C#.  It will specifically need to support Unity game development tool, though it won't depend on unity libraries.

It is very rough.  It's my first bit of C# or .NET, and I won't have much time to spend on it.

It is unsupported.  Contributions are most welcome.

Initial ideas and code from Paul Tondeur's blog post: http://www.paultondeur.com/2010/03/23/tutorial-loading-and-parsing-external-xml-and-json-files-with-unity-part-1-xml/

Current Features
================
- Create an Environment from a pachube (eeml) XML Document
- Serialize such an Environment back to XML.

Missing Features
================
- Parse standalone datastream documents.

Installation
============

Patches
=======

Usage
=====

::

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


Getting Help
============

Reporting Bugs
==============

Contributors
============
