using System;

internal class Ship
{
	int posN = 0;
	int posE = 0;
	int wpE = 10;
	int wpN = 1;
	
	public void MoveNorth(int distance)
	{
		wpN += distance;
	}
	
	public void MoveEast(int distance)
	{
		wpE += distance;
	}
	
	public void Turn(int degrees)
	{
		double angleRad = (Math.PI * degrees) / 180;
		double sin = Math.Sin(angleRad);
		double cos = Math.Cos(angleRad);
		
		double dWpE = wpE * cos - wpN * sin;
		double dWpN = wpE * sin + wpN * cos;
		
		wpE = (int)Math.Round(dWpE);
		wpN = (int)Math.Round(dWpN);
	}
	
	public void MoveForward(int distance)
	{
		posE += distance * wpE;
		posN += distance * wpN;
	}
	
	public int GetDistanceFromOrigin()
	{
		return Math.Abs(posE) + Math.Abs(posN);
	}
	
	public override string ToString()
	{
		return string.Format("(E:{0}, N:{1}, WPE:{2}, WPN:{3})", posE, posN, wpE, wpN);
	}
}