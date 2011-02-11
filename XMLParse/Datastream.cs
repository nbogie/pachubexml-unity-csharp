using System;
using System.Collections;

class DataPoint
{
	public string value;
	public string timestamp;
	public override string ToString ()
	{
		return value + " at " + timestamp;
	}
}

class Datastream
{
	public string id;
	public string currentValue;
	public string minValue;
	public string maxValue;
	public string currentValueTimestamp;
	public ArrayList tags;
	public ArrayList dataPoints;

	public Datastream ()
	{
		tags = new ArrayList ();
		dataPoints = new ArrayList ();
	}

	public override string ToString ()
	{
		string returnString = "id: " + id;
		string tagsString = "";
		foreach (String tag in tags) {
			tagsString += tag + ", ";
		}
		
		returnString += "\r\nCurrent Value: " + currentValue;
		returnString += "\r\nCurrent Value Timestamp: " + currentValueTimestamp;
		returnString += "\r\nMinValue: " + minValue;
		returnString += "\r\nMaxValue: " + maxValue;
		returnString += "\r\nTags: " + tagsString;
		String dataPointsString = "";
		foreach (DataPoint p in dataPoints) {
			dataPointsString += p + "\r\n";
		}
		returnString += "\r\nPrevious DataPoints: " + dataPointsString;
		
		return returnString;
	}
}
