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
