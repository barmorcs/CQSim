using System;
namespace QSim
{
	public class Beater : Player
	{
		Play Game = new Play();

		public bool Attack(Player defend, bool noDefender = false)
		{
			int thresh = 40;
			int fail = 46;
			int backbeat = 54;
			int dopple = 60;

			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);

			Beater helper = Game.GetBeater(this, true);
			int combined = helper.StatsTotal() + helper.Size();

			int roll = rnd.Next(1, 21);
			int check = roll + this.StatsTotal() + this.Size();

			if (check < thresh || roll==1)
			{
				System.Console.WriteLine(this.Name() + " aimed a Bludger at " + defend.Name() + " but missed");
				return false;
			}
			else if ((roll ==20 || check > dopple) && combined > thresh)
			{
				System.Console.WriteLine(this.Name() + " and " + helper.Name() + " aimed a Dopplebeater Defence at " + defend.Name());
				return DefendHelper(this, defend, combined - fail, noDefender);
			}
			else if (check > backbeat)
			{
				System.Console.WriteLine(this.Name() + " aimed a Bludger Backbeat at " + defend.Name());
				return DefendHelper(this, defend, check - fail, noDefender);
			}
			else if (check > fail)
			{
				System.Console.WriteLine(this.Name() + " aimed a Bludger at " + defend.Name());
				return DefendHelper(this, defend, check - fail, noDefender);
			}
			else 
			{
				System.Console.WriteLine(this.Name() + " aimed a wild Bludger at " + defend.Name());
				return DefendHelper(this, defend, check - fail, noDefender);
			}
		}

		//returns false if the defence was unsuccessful
		public bool Defend(int help, Beater offender = null)
		{
			int fail = 40;
			int newtarget = 52;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);

			int roll = rnd.Next(1, 21);
			int check = roll + this.StatsTotal() + this.Size();

			if (check < fail || roll==0)
			{
				System.Console.WriteLine(this.Name() + " tried to block the Bludger but missed");
				return false;
			}
			else
			{
				System.Console.WriteLine(this.Name() + " blocked the Bludger");

				if (check > newtarget || roll>18)
				{

					if (roll < 6 && !(offender == null))
					{
						this.Attack(offender, false);
					}
                    else if(roll < 12 && !(offender == null))
					{
						this.Attack(offender, true);
					}
					else
					{
						this.Attack(Game.GetChaser(null, (this.GetKey().Substring(0, 2)), true));
					}
				}
				return true;
			}
		}

		public bool BlockGoal()
		{
			int fail = 60;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);

			int roll = rnd.Next(1, 21);
			int check = roll + this.StatsTotal() + this.Size();


			if (roll == 20 || check > fail)
			{
				System.Console.WriteLine(this.Name() + " knocked the Quaffle out of play with a Bludger!");
				return true;
			}
			else return false;

		}
		//returns true if player is hit
		private bool DefendHelper(Beater attacker, Player defend, int roll, bool noDefender = false)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int chance = rnd.Next(0, 2);
			int leadChance = rnd.Next(0, 3);
			Beater defender = null;

			if (leadChance > 0)defender = Game.GetBeater(this, false, true);
			else defender = Game.GetBeater(this);

			if (!noDefender && (chance == 1 || leadChance == 1))
			{
				if (!defender.Defend(roll, attacker))
				{
					return !defend.AvoidBludger(roll);
				}
				else return false;
			}
			else
			{
				return !defend.AvoidBludger(roll);
			}
		}

	}
}