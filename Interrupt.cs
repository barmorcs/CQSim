using System;
namespace QSim
{
	public class Interrupt : Player
	{
		public Interrupt()
		{
		}

		public bool PassInterrupt()
		{
			return true;
		}

		public bool GoalInterrupt()
		{
			return true;
		}
	}
}