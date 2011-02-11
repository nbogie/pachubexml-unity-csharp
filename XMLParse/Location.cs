using System;

public class Location
{
    public string name;
    public string latitude;
    public string longitude;
    public string elevation;
    public string domain;
    public string exposure;
    public string disposition;
		
	public Location()
	{
	}
	
    override public string ToString()
    {
        string s = "Location: name: " + name;
        s += "\r\nlatitude: " + latitude;
        s += "\r\nlongitude: " + longitude;
        s += "\r\nelevation: " + elevation;
        s += "\r\ndomain: " + domain;
        s += "\r\nexposure: " + exposure;
        s += "\r\ndisposition: " + disposition;
        return s;
    }
}
