using System;
using System.Collections;

public class Environment
{
	public string id;
	public string title;
	public string updated;
	public string description;
	public string feed;
	public string website;
	public ArrayList datastreams;
	public ArrayList tags;
	public string status;
	public string isPrivate;
	public Location location;

	public Environment ()
	{
		datastreams = new ArrayList ();
		tags = new ArrayList ();
	}

	public override string ToString ()
	{
		string datastreamsString = "";
		foreach (Datastream datastream in datastreams) {
			datastreamsString += "Datastream: " + datastream + "\r\n";
		}
		string tagsString = "";
		foreach (String tag in tags) {
			tagsString += tag + ", ";
		}
		string returnString = "id: " + id;
		returnString += "\r\nTitle: " + title;
		returnString += "\r\nDescription: " + description;
		returnString += "\r\nUpdated: " + updated;
		returnString += "\r\nFeed: " + feed;
		returnString += "\r\nWebsite: " + website;
		returnString += "\r\nStatus: " + status;
		returnString += "\r\nPrivate: " + isPrivate;
		returnString += "\r\nTags: " + tagsString;
		if (location != null) {
			returnString += "\r\nLocation: " + location;
		}
		
		returnString += "\r\nDatastreams: \r\n" + datastreamsString;
		
		return returnString;
	}
}
