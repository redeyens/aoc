public partial class Program
{
    private class Rule
	{
		string name;
		int low1;
		int high1;
		int low2;
		int high2;
		
		public Rule(string name, int low1, int high1, int low2, int high2)
		{
			this.name = name;
			this.low1 = low1;
			this.high1 = high1;
			this.low2 = low2;
			this.high2 = high2;
		}
		
		public string Name
		{
			get
			{
				return this.name;
			}
		}
		
		public bool IsValid(int val)
		{
			if(val >= low1 && val <= high1)
				return true;
			if(val >= low2 && val <= high2)
				return true;
			return false;
		}
		
		public override string ToString()
		{
			return string.Format("{0}: {1}-{2} or {3}-{4}", name, low1, high1, low2, high2);
		}
	}
}
