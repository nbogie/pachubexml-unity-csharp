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

        //Parse xml doc using System.XML (we'll hide this eventually)
        XmlDocument xmlDoc = new XmlDocument ();
        xmlDoc.Load ("path_to_file.xml");
        XmlNamespaceManager nsman = new XmlNamespaceManager (xmlDoc.NameTable);
        nsman.AddNamespace ("ns", xmlDoc.DocumentElement.NamespaceURI);

        //From the xml doc, create an Environment.
        EnvironmentParser parser = new EnvironmentParser ();
        Environment env = parser.Parse (xmlDoc, nsman);
        //Use your environment

        //Serialize the Environment object back to xml.
        EnvironmentXMLWriter w = new EnvironmentXMLWriter ();
        String xmlGenerated = w.Write(env);
        Console.WriteLine ("Regenerated XML: " + xmlGenerated);

Getting Help
============

Reporting Bugs
==============

Contributors
============
