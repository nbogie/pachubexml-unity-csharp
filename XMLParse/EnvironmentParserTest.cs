using System;
using NUnit.Framework;
using System.Xml;

[TestFixture]
public class EnvironmentParserTest
{

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
		XHelp.QuickParseFile ("fullExampleFixed.xml");
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

	public static Environment QuickParseFile (String filepath)
	{
		XmlDocument xmlDoc = LoadXMLFromFile (filepath);
		XmlNamespaceManager nsman = new XmlNamespaceManager (xmlDoc.NameTable);
		nsman.AddNamespace ("ns", xmlDoc.DocumentElement.NamespaceURI);
		EnvironmentParser parser = new EnvironmentParser ();
		Environment e = parser.Parse (xmlDoc, nsman);
		return e;
	}
	
}
