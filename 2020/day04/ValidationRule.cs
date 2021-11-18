using System;
using System.Text.RegularExpressions;

public class ValidationRule
{
	private string name;
	private Regex template;
	private Predicate<string> rule;
	
	public ValidationRule(string name, Regex template, Predicate<string> rule)
	{
		this.name = name;
		this.template = template;
		this.rule = rule;
	}
	
	public bool IsSatisfied(string input)
	{
		var match = template.Match(input);
		
		if(!match.Success)
		{
			//Console.WriteLine("{0} missing or wrong format.", name);
			return false;
		}
		
		if(!rule(match.Groups[1].Value))
		{
			//Console.WriteLine("{0} invalid value {1}.", name, match.Groups[1].Value);
			return false;
		}
		
		return true;
	}
}
