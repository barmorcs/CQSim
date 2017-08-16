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

			int fail = 32;
			int threshold = 40;
			int special = 49;
			string str = "";

			if (penalty)
			{
				fail = 38;
				threshold = 40;
				special = 55;
				str = " penalty";
			}

			int roll = rnd.Next(0, 11);
			int check = roll + this.Reflex() + this.Speed() + this.Sight() + this.Size() - bonus;

			if (check < fail || roll == 1)//crit fail!
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
			else if (check > special || roll==20)
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