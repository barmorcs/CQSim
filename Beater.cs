using System;
namespace QSim
{
	public class Beater : Player
	{
		Play Game = new Play();

		public bool Attack(Player defend, bool noDefender = false)
		{
			int thresh = 29;
			int fail = 33;
			int backbeat = 44;
			int dopple = 46;

			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);

			Beater helper = Game.GetBeater(this, true);
			int combined = helper.StatsTotal() + helper.Size();

			int roll = rnd.Next(0, 11);
			int check = roll + this.StatsTotal() + this.Size();

			if (check < thresh)
			{
				System.Console.WriteLine(this.Name() + " aimed a Bludger at " + defend.Name() + " but missed");
				return false;
			}
			else if (check > dopple && combined > fail)
			{
				System.Console.WriteLine(this.Name() + " and " + helper.Name() + " aimed a Dopplebeater Defence at " + defend.Name());
				return DefendHelper(this, defend, combined - fail);
			}
			else if (check > backbeat)
			{
				System.Console.WriteLine(this.Name() + " aimed a Bludger Backbeat at " + defend.Name());
				return DefendHelper(this, defend, check - fail);
			}
			else if (check > fail)
			{
				System.Console.WriteLine(this.Name() + " aimed a Bludger at " + defend.Name());
				return DefendHelper(this, defend, check - fail);
			}
			else 
			{
				System.Console.WriteLine(this.Name() + " aimed a wild Bludger at " + defend.Name());
				return DefendHelper(this, defend, check - fail);
			}
		}

		//returns false if the defence was unsuccessful
		public bool Defend(int help, Beater offender = null)
		{
			int fail = 33;
			int newtarget = 40;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);

			int roll = rnd.Next(0, 11);
			int chance = rnd.Next(0, 2);
			int check = roll + this.StatsTotal() + this.Size();

			if (check < fail)
			{
				System.Console.WriteLine(this.Name() + " tried to block the Bludger but missed");
				return false;
			}
			else
			{
				System.Console.WriteLine(this.Name() + " blocked the Bludger");

				if (check > newtarget)
				{

					if (roll < 3 && !(offender == null))
					{
						this.Attack(offender, false);
					}
					else
					{
						this.Attack(Game.GetChaser(null, (this.GetKey().Substring(0, 2)), true));
					}
				}
				return true;
			}
		}
		//returns true if player is hit
		private bool DefendHelper(Beater attacker, Player defend, int roll, bool noDefender = false)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int chance = rnd.Next(0, 2);
			Beater defender = Game.GetBeater(this);

			if (!noDefender && chance == 1)
			{
				if (!defender.Defend(roll, attacker))
				{
					return !defend.AvoidBludger((roll / 2));
				}
				else return false;
			}
			else
			{
				return !defend.AvoidBludger(roll / 2);
			}
		}

	}
}