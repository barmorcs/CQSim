using System;
namespace QSim
{
	public class Foul
	{
		readonly string[] foulStrings = new string[] { "blag", "blatch", "blurt", "cob", };

		public Foul()
		{
		}

		public bool FoulVersus(Player fouler, Player fouled, string type = null)
		{
			int foulerStats = 0;
			int fouledStats = 0;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int roll1 = rnd.Next(0, 10);
			int roll2 = rnd.Next(0, 10);
			int swap = rnd.Next(0, 4);
			string passString = "";
			string failString = "";
			bool hurt = false;
			int diff = 0;

			if (type == null)
			{
				type = foulStrings[swap];
			}
			switch (type)
			{
				case "blag":
					foulerStats = fouler.Strength() + fouler.Aim() + roll1;
					fouledStats = fouled.Strength() + fouled.Reflex() + roll2;
					passString = (fouler.Name() + " grabbed onto " + fouled.Name() + "'s broom!");
					failString = (fouler.Name() + " tried to grab " + fouled.Name() + "'s broom and missed");
					break;

				case "blatch":
					foulerStats = fouler.Strength() + fouler.Speed() + roll1;
					fouledStats = fouled.Speed() + fouled.Reflex() + roll2;
					passString = (fouler.Name() + " collided into " + fouled.Name() + "!");
					failString = (fouler.Name() + " tried to collide into " + fouled.Name() + " and missed");
					diff = foulerStats - fouledStats;
					hurt = true;
					break;

				case "blurt":
					foulerStats = fouler.Strength() + roll1;
					if (fouled.Reflex() > fouled.Strength())
					{
						fouledStats = fouled.Reflex() + roll2;
					}
					else
					{
						fouledStats = fouled.Strength() + roll2;
					}
					passString = (fouler.Name() + " locked broomhandles with " + fouled.Name() + "!");
					failString = (fouler.Name() + " tried to lock broomhandles with " + fouled.Name() + " and missed");
					break;

				case "cob":
					foulerStats = fouler.Strength() + roll1;
					if (fouled.Reflex() > fouled.Strength())
					{
						fouledStats = fouled.Reflex() + roll2;
					}
					else
					{
						fouledStats = fouled.Strength() + roll2;
					}
					passString = (fouler.Name() + " elbowed " + fouled.Name() + "!");
					failString = (fouler.Name() + " tried to elbow " + fouled.Name() + " and missed");
					diff = foulerStats - fouledStats;
					hurt = true;
					break;

				case "stoog":
					foulerStats = fouler.Strength() + roll1;
					if (fouled.Reflex() > fouled.Strength())
					{
						fouledStats = fouled.Reflex() + roll2;
					}
					else
					{
						fouledStats = fouled.Strength() + roll2;
					}
					diff = foulerStats - fouledStats;
					passString = (fouler.Name() + " knocked " + fouled.Name() + " out of the way!");
					failString = (fouler.Name() + " tried to knock " + fouled.Name() + " out of the way and missed");
					hurt |= diff > 5;
					break;

				case "block":
					foulerStats = fouler.Strength() + fouler.Speed() + roll1;
					fouledStats = fouled.Speed() + roll2;

					if (fouled.Reflex() > fouled.Strength())
					{
						fouledStats += fouled.Reflex();
					}
					else
					{
						fouledStats += fouled.Strength();
					}
					diff = foulerStats - fouledStats;
					passString = (fouler.Name() + " blocked " + fouled.Name() + "!");
					failString = (fouler.Name() + " tried to block " + fouled.Name() + " and missed");
					hurt |= diff > 5;
					break;

			}//end switch

			if (foulerStats > fouledStats)
			{
				System.Console.WriteLine(passString);
				if (hurt)
				{
					fouled.Hurt((diff + 1) / 2);
				}
				return true;
			}
			else
			{
				System.Console.WriteLine(failString);
				return false;
			}
		}
	}
}