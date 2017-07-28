using System;
namespace QSim
{
	public class Keeper : Player
	{
		public bool DefendGoal(Chaser chaser, int bonus = 0, bool penalty = false)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			Play Game = new Play();

			int fail = 22;
			int threshold = 30;
			int special = 39;
			string str = "";

			if (penalty)
			{
				fail = 27;
				threshold = 30;
				special = 42;
				str = " penalty";
			}

			int roll = rnd.Next(0, 11);
			int check = roll + this.Reflex() + this.Speed() + this.Sight() + this.Size() - bonus;

			if (check < fail)//crit fail!
			{
				if (rnd.Next(0, 5) == 1 && this.PlaysDirty() && !penalty)
				{
					System.Console.WriteLine(this.Name() + " pushed the Quaffle back through the hoop!");
					Game.Referee(this, chaser);
					return true;
				}
				else
				{
					System.Console.WriteLine(this.Name() + " failed to block the" + str + " goal!");
					return false;
				}
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