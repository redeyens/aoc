using System;

public class IntRangeCheck
{
	private int min;
	private int max;
	
	public IntRangeCheck(int min, int max)
	{
		this.min = min;
		this.max = max;
	}
	
	public bool IsOK(string input)
	{
		int arg = 0;
		
		if(!Int32.TryParse(input, out arg))
			return false;
		if(arg < min)
			return false;
		if(arg > max)
			return false;
		
		return true;
	}
}
