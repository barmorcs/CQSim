using System;
namespace QSim
{
	public class Seeker : Player
	{
		Play Game = new Play();

		public bool Search(bool follow = false)
		{
			int search = 12;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(0, 11);
			int check = roll + this.Sight();
			string also = "";

			if (follow)
			{
				also = " also";
				search = 10;
			}

			if (check > search)
			{
				System.Console.WriteLine(this.Name() + " has" + also + " seen the Snitch!");
				return true;
			}
			else return false;
		}

		public bool Catch()
		{
			int snatch = 30;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(0, 11);
			int check = roll + this.Sight() + this.Reflex() + this.Speed();

			if (check > snatch)
			{
				System.Console.WriteLine(this.Name() + " caught the Snitch!");
				Game.Score(this, 150);
				return true;
			}
			System.Console.WriteLine(this.Name() + " tried to catch the Snitch and missed!");
			return false;
		}

		public bool HeadToHead(Seeker one, Seeker two)
		{
			int snatch = 30;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(0, 11);
			int roll2 = rnd.Next(0, 11);
			int checkOne = roll + one.Sight() + one.Reflex() + one.Speed();
			int checkTwo = roll2 + two.Sight() + two.Reflex() + two.Speed();

			if (checkOne < snatch && checkTwo < snatch)
			{
				System.Console.WriteLine("Both Seekers reach for the Snitch and miss!");
				return false;
			}

			if (checkOne == checkTwo)
			{
				System.Console.WriteLine("Both Seekers reach for the Snitch and miss!");
				return false;
			}

			if (checkOne < checkTwo)
			{
				System.Console.WriteLine(one.Name() + " caught the Snitch!");
				Game.Score(one, 150);
				return true;
			}
			else
			{
				System.Console.WriteLine(two.Name() + " caught the Snitch!");
				Game.Score(two, 150);
				return true;
			}
		}

		public void LostSnitch()
		{
			System.Console.WriteLine(this.Name() + " has lost the Snitch!");
		}
	}
}