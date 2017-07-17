using System;
namespace QSim
{
	public class Keeper : Player
	{
		public bool DefendGoal(int bonus = 0, bool penalty = false)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);

			int fail = 17;
			int threshold = 21;
			int special = 29;
			string str = "";

			if (penalty)
			{
				fail = 21;
				threshold = 25;
				special = 34;
				str = " penalty";
			}

			int roll = rnd.Next(0, 11);
			int check = roll + this.Reflex() + this.Speed() + this.Sight() + this.Size() + bonus;

			if (check < fail)//crit fail!
			{
				System.Console.WriteLine(this.Name() + " failed to block the"+str+" goal!");
				return false;
			}
			else if (check > special)
			{
				System.Console.WriteLine(this.Name() + " blocked the"+str+" goal with a Starfish and Stick!");
				return true;
			}
			else if (check > threshold)
			{
				System.Console.WriteLine(this.Name() + " caught the Quaffle!");
				return true;
			}
			else
			{
				System.Console.WriteLine(this.Name() + " blocked the"+str+" goal!");
				return true;
			}
		}
	}
}