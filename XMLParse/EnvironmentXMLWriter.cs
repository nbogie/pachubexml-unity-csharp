using System;
using System.Xml;
using System.Collections;
using System.IO;
public class EnvironmentXMLWriter
{
	public EnvironmentXMLWriter ()
	{
	}

	public String Write (Environment env)
	{
		XmlDocument xmlDoc = new XmlDocument ();
		string basexml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
		<eeml xmlns=""http://www.eeml.org/xsd/0.5.1"" 
			  xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""0.5.1"" 
              xsi:schemaLocation=""http://www.eeml.org/xsd/0.5.1 http://www.eeml.org/xsd/0.5.1/0.5.1.xsd""></eeml>";
		xmlDoc.LoadXml (basexml);
		XmlNode root = xmlDoc.DocumentElement;

		//It seems we must explicitly specify each node's namespace
		//If we leave ns out, it won't just inherit from parent re ns but will say xmlns=""
		XmlElement nEnv = xmlDoc.CreateElement ("environment",xmlDoc.DocumentElement.NamespaceURI);
		
		//Add the node to the document.
		root.AppendChild (nEnv);
		nEnv.SetAttribute ("id", env.id);
		nEnv.AppendChild (UtilSimpleTextNode (xmlDoc, "title", env.title));
		nEnv.AppendChild (UtilSimpleTextNode (xmlDoc, "description", env.description));
		nEnv.AppendChild (UtilSimpleTextNode (xmlDoc, "website", env.website));
		nEnv.AppendChild (MakeLocationXML (env.location, xmlDoc));
		foreach (String tag in env.tags) {
			nEnv.AppendChild (UtilSimpleTextNode (xmlDoc, "tag", tag));
		}
		foreach (Datastream ds in env.datastreams) {
			nEnv.AppendChild (MakeDatastreamXML (ds, xmlDoc));
		}
		
		// Now create StringWriter object to get data from xml document.
		StringWriter sw = new StringWriter ();
		XmlTextWriter xw = new XmlTextWriter (sw);
		xw.Formatting = Formatting.Indented;
		xw.Indentation = 4;
		xmlDoc.WriteTo (xw);
		return sw.ToString ();
	}

	public XmlNode UtilSimpleTextNode (XmlDocument doc, String tag, String val)
	{
		XmlElement elem = doc.CreateElement (tag, doc.DocumentElement.NamespaceURI);
		elem.InnerText = val;
		return elem;
	}

	XmlNode MakeDatastreamXML (Datastream ds, XmlDocument doc)
	{
		XmlElement nDS = doc.CreateElement ("data", doc.DocumentElement.NamespaceURI);
		nDS.SetAttribute ("id", ds.id);
		nDS.AppendChild (UtilSimpleTextNode (doc, "current_value", ds.currentValue));
		foreach (String tag in ds.tags) {
			nDS.AppendChild (UtilSimpleTextNode (doc, "tag", tag));
		}
		return nDS;
	}

	XmlNode MakeLocationXML (Location loc, XmlDocument doc)
	{
		String x = doc.DocumentElement.NamespaceURI;
		XmlElement nLoc = doc.CreateElement ("location", x);
		nLoc.SetAttribute ("domain", loc.domain);
		nLoc.SetAttribute ("exposure", loc.exposure);
		nLoc.SetAttribute ("disposition", loc.disposition);
		nLoc.AppendChild (UtilSimpleTextNode (doc, "name", loc.name));
		nLoc.AppendChild (UtilSimpleTextNode (doc, "lat", loc.latitude));
		nLoc.AppendChild (UtilSimpleTextNode (doc, "lon", loc.longitude));
		return nLoc;
	}
}
