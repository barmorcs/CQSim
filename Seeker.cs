using System;
namespace QSim
{
	public class Seeker : Player
	{
		Play Game = new Play();

		public bool Search(bool follow = false)
		{
			int search = 18;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(1, 21);
			int check = roll + this.Sight();
			string also = "";

			if (follow)
			{
				also = " also";
				search = 10;
			}

			if (check > search && roll != 1)
			{
				System.Console.WriteLine(this.Name() + " has" + also + " seen the Snitch!");
				return true;
			}
			else return false;
		}

		public bool Catch()
		{
			int snatch = 40;
			int spiral = 46;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(1, 21);

			int check = roll + this.Sight() + this.Reflex() + this.Speed();

			if (check > spiral && roll != 1)
			{
				System.Console.WriteLine(this.Name() + " goes into a Spiral Dive!");
			}
			if (check > snatch && roll != 1)
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
			int snatch = 40;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll = rnd.Next(1, 21);
			int roll2 = rnd.Next(1, 21);
			int checkOne = roll + one.Sight() + one.Reflex() + one.Speed();
			int checkTwo = roll2 + two.Sight() + two.Reflex() + two.Speed();

			if ((roll==1 && roll2==1) || (checkOne < snatch && checkTwo < snatch))
			{
				System.Console.WriteLine("Both Seekers reach for the Snitch and miss!");
				return false;
			}

			if (checkOne == checkTwo)
			{
				System.Console.WriteLine("Both Seekers reach for the Snitch and miss!");
				return false;
			}

			if (checkOne < checkTwo || roll == 1 || roll2==20)
			{
				System.Console.WriteLine(one.Name() + " caught the Snitch!");
				Game.Score(one, 150);
				return true;
			}
			else if (checkOne < checkTwo || roll2 == 1 || roll==20)
			{
				System.Console.WriteLine(two.Name() + " caught the Snitch!");
				Game.Score(two, 150);
				return true;
			}
			else
			{
				System.Console.WriteLine("Both Seekers reach for the Snitch and miss!");
				return false;
			}
		}

		public void LostSnitch()
		{
			System.Console.WriteLine(this.Name() + " has lost the Snitch!");
		}
	}
}