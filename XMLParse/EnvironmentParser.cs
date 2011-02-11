using System;
using System.Xml;
using System.Collections;

class MissingAttributeException : System.Exception
{
	public MissingAttributeException (String m) : base(m)
	{
	}
}

class MissingNodeException : System.Exception
{
	public MissingNodeException (String m) : base(m)
	{
	}
}

public class EnvironmentParser
{
	public EnvironmentParser ()
	{
	}
	public Environment ParseFromFile(String filename)
	{
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load (filename);
		XmlNamespaceManager nsman = new XmlNamespaceManager (xmlDoc.NameTable);
		nsman.AddNamespace ("ns", xmlDoc.DocumentElement.NamespaceURI);		
		return Parse(xmlDoc, nsman);
	}
	
	//TODO: handle parse exceptions (missing attrs, nodes, unexpected types (e.g. non-integer env id, bad timestamp, etc)
	//TODO: split datastream parsing so we can do that without an env.
	public Environment Parse (XmlDocument doc, XmlNamespaceManager nsman)
	{
		Environment e = new Environment ();
		XmlNode nEnv = doc.SelectSingleNode ("ns:eeml/ns:environment", nsman);
		if (nEnv == null) {
			throw new MissingNodeException ("eeml/environment");
		}
		e.id = UtilAttribOrFail (nEnv, "id", "environment");
		e.title = UtilSingleTextNodeContentOr (nEnv, "ns:title", nsman, "");
		e.website = UtilSingleTextNodeContentOr (nEnv, "ns:website", nsman, "");
		e.feed = UtilSingleTextNodeContentOr (nEnv, "ns:feed", nsman, "");
		e.description = UtilSingleTextNodeContentOr (nEnv, "ns:description", nsman, "");
		e.isPrivate = UtilSingleTextNodeContentOr (nEnv, "ns:private", nsman, "");
		e.status = UtilSingleTextNodeContentOr (nEnv, "ns:status", nsman, "");
		// TODO: bail on missing status?
		foreach (XmlNode tagNode in nEnv.SelectNodes ("ns:tag", nsman)) {
			e.tags.Add (tagNode.InnerText);
		}
		
		//parse the location
		XmlNode nLoc = nEnv.SelectSingleNode ("ns:location", nsman);
		if (nLoc != null) {
			Location loc = new Location ();
			loc.name = UtilSingleTextNodeContentOr (nLoc, "ns:name", nsman, "");
			loc.latitude = UtilSingleTextNodeContentOr (nLoc, "ns:lat", nsman, "");
			loc.longitude = UtilSingleTextNodeContentOr (nLoc, "ns:lon", nsman, "");
			loc.elevation = UtilSingleTextNodeContentOr (nLoc, "ns:ele", nsman, "");
			loc.domain = UtilAttribOr (nLoc, "domain", "");
			loc.exposure = UtilAttribOr (nLoc, "exposure", "");
			loc.disposition = UtilAttribOr (nLoc, "disposition", "");
			e.location = loc;
		}
		
		//Loop through any datastreams
		foreach (XmlNode nDS in nEnv.SelectNodes ("ns:data", nsman)) {
			Datastream ds = new Datastream ();
			ds.id = UtilAttribOrFail (nDS, "id", "data");
			XmlNode nDSCurrentVal = nDS.SelectSingleNode ("ns:current_value", nsman);
			if (nDSCurrentVal != null) {
				ds.currentValue = nDSCurrentVal.InnerText;
				ds.currentValueTimestamp = UtilAttribOr (nDSCurrentVal, "at", "");
				//TODO: parse timestamp?
			}
			ds.minValue = UtilSingleTextNodeContentOr (nDS, "ns:min_value", nsman, "");
			ds.maxValue = UtilSingleTextNodeContentOr (nDS, "ns:max_value", nsman, "");
			foreach (XmlNode tagNode in nDS.SelectNodes ("ns:tag", nsman)) {
				ds.tags.Add (tagNode.InnerText);
			}
			
			//Loop through any datapoints in the current datastream
			foreach (XmlNode nDP in nDS.SelectNodes ("ns:datapoints/ns:value", nsman)) {
				//Warning: potentially too much object instantiation, with long histories.
				//Perhaps tuples better, or a matrix.
				DataPoint dp = new DataPoint ();
				dp.value = nDP.InnerText;
				dp.timestamp = UtilAttribOrFail (nDP, "at", "value");
				ds.dataPoints.Add (dp);
			}
			e.datastreams.Add (ds);
		}
		return e;
	}

	private String UtilAttribOr (XmlNode xmlNode, String name, String defaultVal)
	{
		XmlNode attrib = xmlNode.Attributes.GetNamedItem (name);
		if (attrib != null) {
			return attrib.Value;
		} else {
			return defaultVal;
		}
	}
	private String UtilAttribOrFail (XmlNode xmlNode, String name, String parentNodeName)
	{
		XmlNode attrib = xmlNode.Attributes.GetNamedItem (name);
		if (attrib != null) {
			return attrib.Value;
		} else {
			throw new MissingAttributeException ("Missing attribute: " + name + " on node " + parentNodeName);
		}
	}

	private String UtilSingleTextNodeContentOr (XmlNode xmlNodeParent, String xpath, XmlNamespaceManager nsman, String defaultStr)
	{
		XmlNode n = xmlNodeParent.SelectSingleNode (xpath, nsman);
		if (n == null) {
			return defaultStr;
		} else {
			return n.InnerText;
		}
	}
}
