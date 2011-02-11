using System;
using System.IO;
using NUnit.Framework;
using System.Xml;

[TestFixture]
public class EnvironmentParserTest
{
	[Test]
	public void TestWhereami ()
	{
		Console.WriteLine("cwd: " + Directory.GetCurrentDirectory());
	}

	[Test]
	public void TestParseBasic ()
	{
		XHelp.QuickParseFile ("basic.xml");
	}

	[Test]
	public void TestParseMinimal ()
	{
		XHelp.QuickParseFile ("minimal.xml");
	}

	[Test]
	public void TestParseExampleFromAPIDocs ()
	{
		XHelp.QuickParseFile ("fullExample.xml");
	}

	[Test]
	public void TestParseNoEnvIdThrowsEx ()
	{
		try {
			XHelp.QuickParseFile ("badNoEnvId.xml");
			Assert.Fail ("should have thrown exception - no env id");
		} catch (MissingAttributeException ex) {
			//expected
		}
	}
}

public class XHelp
{
	public static XmlDocument LoadXMLFromFile (String filepath)
	{
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load (filepath);
		return xmlDoc;
	}

	// Quickly load and parse a test data file.
	// Fixes up the file path (which is expected relative) to account for the 
	// expected nunit runtime dir bin/Debug or bin/Release.
	// That's a hack because we don't know what nunit will have as its runtime dir, 
	// and shouldn't care.
	//TODO: find a way to copy these files into a KNOWN relative location
	// (e.g. in an after-build action).
	// I tried a shell command but the variables $ProjectDir, $TargetDir aren't set
	// and anyway that's going to be platform-specific.
	public static Environment QuickParseFile (String filePath)
	{
		String fixedFilePath = "../../TestFiles/" + filePath;
		XmlDocument xmlDoc = LoadXMLFromFile (fixedFilePath);
		XmlNamespaceManager nsman = new XmlNamespaceManager (xmlDoc.NameTable);
		nsman.AddNamespace ("ns", xmlDoc.DocumentElement.NamespaceURI);
		EnvironmentParser parser = new EnvironmentParser ();
		Environment e = parser.Parse (xmlDoc, nsman);
		return e;
	}
}
