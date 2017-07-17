using System;
namespace QSim
{
	public class Chaser : Keeper
	{
		Play Game = new Play();
		
		public bool Pass(Chaser next)
		{
			int fail = 8;
			int threshold = 11;
			int reversePass = 29;
			int ploy = 33;

			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);

			int roll = rnd.Next(0, 11);
			int check = roll + this.Aim() + this.Strength();
			int help = check - threshold;

			if (check < fail)//crit fail!
			{
				System.Console.WriteLine(this.Name() + " tried to pass the Quaffle to " + next.Name() + " but missed");
				Game.SetBaller();
				return false;
			}
			else
			{
				if ((check < threshold))
				{
					System.Console.Write(this.Name() + " threw a wide pass");
				}
				else if (check > ploy)
				{
					System.Console.Write(this.Name() + " threw a Porskoff Ploy");
				}
				else if (roll == 10 || (check > reversePass))
				{
					System.Console.Write(this.Name() + " threw a Reverse Pass");
				}
				else
				{
					System.Console.Write(this.Name() + " passed the Quaffle");
				}
			}
			if (StealAttempt(help))
				return false;
			return (next.Catch(false, (help)));

		}

		public bool Catch(bool first = false, int help = 0)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(0, 11);
			int total = roll + help + this.Sight() + this.Reflex();
			if (total < 8)//crit fail! or a regular fail
			{
				if (first) System.Console.WriteLine(this.Name() + " reached for the Quaffle and missed");
				else if (help < 0) System.Console.WriteLine(" and " + this.Name() + " missed it");
				else System.Console.WriteLine(" to " + this.Name() + " but they dropped it");
				Game.SetBaller();
				return false;
			}
			if (first) System.Console.WriteLine(this.Name() + " grabbed the Quaffle");
			else if (help < 0) System.Console.WriteLine(" but " + this.Name() + " caught it");
			else System.Console.WriteLine(" to " + this.Name());
			if (Game.PlayInterrupt(this))
			{
				return false;
			}
			return true;
		}

		public bool Shoot(Keeper keeper, int bonus = 0)
		{
			int fail = 17;
			int threshold = 20;
			int special = 33;
			int flick = 36;

			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(0, 11);
			int check = roll + this.Aim() + this.Strength() + this.Sight() + this.Size() + bonus;

			Chaser next = Game.GetChaser(null, keeper.GetKey().Substring(0, 2));
			Game.SetBaller(next);

			if (check < fail)//crit fail!
			{
				System.Console.WriteLine(this.Name() + " attempted to shoot goal and missed");
				return false;
			}
			if (check > flick)
			{
				System.Console.Write(this.Name() + " hit the Quaffle towards the goalposts with a Finbourgh Flick!");
				return (keeper.DefendGoal(flick - check));
			}
			else if (check > special && this.Aim() > this.Strength())
			{
				System.Console.WriteLine(this.Name() + " aimed a Dionysus Dive!");
				return (keeper.DefendGoal(special - check));
			}
			else if (check > special)
			{
				System.Console.WriteLine(this.Name() + " aimed a Chelmondiston Charge!");
				return (keeper.DefendGoal(special - check));
			}
			else if (check > threshold)
			{
				System.Console.WriteLine(this.Name() + " attempted to shoot a goal");
				return (keeper.DefendGoal(threshold - check));
			}
			else if (check > fail)
			{
				System.Console.WriteLine(this.Name() + " attempted to shoot a goal");
				return (keeper.DefendGoal(check - threshold));
			}
			return false;
		}

		//returns true if ball is intercepted
		public bool Interception(Chaser victim)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll1 = rnd.Next(0, 11);
			int roll2 = rnd.Next(0, 11);
			int interceptorCheck = roll1 + this.Sight() + this.Reflex() + this.Strength() + this.Size();
			int victimCheck = roll2 + victim.Sight() + victim.Reflex() + victim.Strength() + this.Size();

			if (interceptorCheck >= victimCheck)
			{
				System.Console.WriteLine(this.Name() + " intercepted the Quaffle from " + victim.Name());
				Game.SetBaller(this);
				return true;
			}
			else
			{
				System.Console.WriteLine(victim.Name() + " dodged an interception from " + this.Name());
				return false;
			}
		}

		//returns true if ball is stolen
		public bool StealAttempt(int help)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(0, 7);
			Chaser thief = Game.GetChaser(null, this.GetKey().Substring(0, 2), true);

			if (roll == 1 || (roll == 2 && thief.IsLead()))
			{
				return Steal(help, thief);
			}
			return false;
		}

		//returns true if ball is stolen
		public bool Steal(int help, Chaser thief) 
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(0, 21);

			string text = "";
			if (help < 0) text = " and ";
			else text = " but ";

			int check = thief.Sight() + thief.Reflex() + thief.Reflex() - (help/2);
			if (roll == 0)
				return false;
			if ((check > 18 && roll > 10) || roll == 20)
			{
				System.Console.WriteLine(text + thief.Name() + " stole it!");
				Game.SetBaller(thief);
				return true;
			}
				return false;
		}
	}
}
