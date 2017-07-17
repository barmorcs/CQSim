using System;
using System.Collections;
using System.Collections.Generic;

namespace QSim
{
	public class Play
	{
		public Play()
		{
		}
		static Dictionary<string, Player> players = new Dictionary<string, Player>();
		static Queue knockedOutPlayers = new Queue();
		static int team1score = 0;
		static int team2score = 0;
		static string team1 = "";
		static string team2 = "";
		static int playcount = 0;

		public static Chaser hasBall;
		public static Seeker isChasing = null;
		public static Seeker isFollowing = null;

		public static bool snitchCaught = false;
		public static bool chasing = false;
		public static bool chasing2 = false;
		public static bool heading = false;
		public int seekCount = 0;
		public int catchAttempts = 0;

		public static void Main()
		{
			players = Reader.GetPlayers();
			team1 = Reader.GetTeam1();
			team2 = Reader.GetTeam2();

			Play Game = new Play();
			Game.SetBaller();
			while (snitchCaught == false)
			{
				Game.PlayCounter();

				if (hasBall.Catch(true)) Game.ChaserPlay(hasBall);

				Game.ReviveChance();

				Game.SeekerChance();

			}
		}// end main

		public void ChaserPlay(Chaser hasBall, int pass = 0)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);

			int passcount = 0;
			Keeper keeper = GetKeeper(hasBall);

			Chaser nextChaser = GetChaser(hasBall);
			if (pass > 3 || (pass > 2 && hasBall.IsLead()))
			{
				if (!hasBall.Shoot(keeper))
					Score(hasBall, 10);
			}
			else if (hasBall.Pass(nextChaser))
			{
				if (rnd.Next(1, 3) > 1 || passcount > 2)
					pass = pass + 1;
				passcount += 1;
				ChaserPlay(nextChaser, pass);
			}
		}

		public void SetBaller(Chaser next = null)
		{
			if (next == null)
				hasBall = GetChaser();
			else hasBall = players[next.GetKey()] as Chaser;
		}

		//no argument gets random chaser
		//string team gets chaser on particular team
		//Chaser hadBall gets "next chaser" on team
		//boolean oppose means flip player team
		public Chaser GetChaser(Chaser hadBall = null, string team = null, bool oppose = false)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			string outTeam = "";
			string position = "";
			if (hadBall != null)
			{
				int swap = rnd.Next(1, 4);
				int playerNum = int.Parse(hadBall.GetKey().Substring(3));
				switch (playerNum)
				{
					case 1:
						if (swap == 1) position = "C3";
						else position = "C2";
						break;
					case 2:
						if (swap == 1) position = "C1";
						else position = "C3";
						break;
					case 3:
						if (swap == 1) position = "C2";
						else position = "C1";
						break;
				}

				if (oppose)
				{
					if (int.Parse(hadBall.GetKey().Substring(1, 1)) == 1)
						team = "T2";
					team = "T1";
				}
				else
				{
					team = hadBall.GetKey().Substring(0, 2);
				}
				Chaser one = players[team + position] as Chaser;
				if (one.IsKnockedOut()) one = GetOtherChaser(hadBall, one);
				return one;
			}

			if (!(team == null))
			{
				outTeam = team;
				if (oppose)
				{
					if (int.Parse(team.Substring(1, 1)) == 1)
						outTeam = "T2";
					else outTeam = "T1";
				}
			}

			if (outTeam == "") outTeam = "T" + rnd.Next(1, 3);

			if (position == "") position = "C" + rnd.Next(1, 4);

			Chaser result = players[outTeam + position] as Chaser;
			if (result.IsKnockedOut()) result = GetOtherChaser(result);
			return result;
		}

		//handles a knocked out chaser
		//use when getting next sequential chaser and they are knocked out
		public Chaser GetOtherChaser(Chaser one, Chaser knockedOut)
		{
			int oneNum = int.Parse(one.GetKey().Substring(3));
			int twoNum = int.Parse(knockedOut.GetKey().Substring(3));
			string team = one.GetKey().Substring(0, 2);
			string position = "";

			switch (oneNum)
			{
				case 1:
					if (twoNum == 2) position = "C3";
					else position = "C2";
					break;
				case 2:
					if (twoNum == 3) position = "C1";
					else position = "C3";
					break;
				case 3:
					if (twoNum == 1) position = "C2";
					else position = "C1";
					break;
			}
			Chaser result = players[team + position] as Chaser;
			return result;
		}

		//Backup for getting a random player that ends up knocked out
		public Chaser GetOtherChaser(Chaser one)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			string team = one.GetKey().Substring(0, 2);
			string position = "";
			int swap = rnd.Next(1, 4);
			int playerNum = int.Parse(one.GetKey().Substring(3));
			switch (playerNum)
			{
				case 1:
					if (swap == 1) position = "C3";
					else position = "C2";
					break;
				case 2:
					if (swap == 1) position = "C1";
					else position = "C3";
					break;
				case 3:
					if (swap == 1) position = "C2";
					else position = "C1";
					break;
			}
			Chaser result = players[team + position] as Chaser;
			return result;
		}

		//gets opposite Keeper by default
		public Keeper GetKeeper(Chaser chaser, bool oppose = false)
		{
			string team = "";
			bool flip = oppose;
			if (int.Parse(chaser.GetKey().Substring(1, 1)) == 1)
			{
				team = "T2";
			}
			else
			{
				team = "T1";
			}

			if (flip)
			{
				team = chaser.GetKey().Substring(0, 1);
			}
			return players[team + "K"] as Keeper;
		}

		//gets opposite Beater by default
		public Beater GetBeater(Player player, bool oppose = false)
		{
			string team = "";
			bool flip = oppose;
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int swap = rnd.Next(1, 3);

			if (int.Parse(player.GetKey().Substring(1, 1)) == 1)
			{
				team = "T2";
			}
			else
			{
				team = "T1";
			}

			if (flip)
			{
				team = player.GetKey().Substring(0, 2);

				if (int.Parse(player.GetKey().Substring(3)) == 1)
					return players[team + "B2"] as Beater;
				return players[team + "B1"] as Beater;
			}

			string key = team + "B" + swap;
			return players[key] as Beater;
		}

		//gets opposite Seeker by default if called with team
		public Seeker GetSeeker(Player player = null, bool oppose = false)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			string outTeam = "";

			if (player == null)
			{
				outTeam = "T" + rnd.Next(1, 3);
			}
			else
			{
				bool flip = oppose;
				if (int.Parse(player.GetKey().Substring(1, 1)) == 1)
				{
					outTeam = "T2";
				}
				else
				{
					outTeam = "T1";
				}

				if (flip)
				{
					outTeam = player.GetKey().Substring(0, 1);
				}
			}
			return players[outTeam + "S"] as Seeker;

		}

		public bool GoalInterrupt(Keeper keeper)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int chance = rnd.Next(10);

			if (chance == 1)//friendly chaser comes to help
			{
				Chaser defendingChaser = GetChaser(null, keeper.GetKey().Substring(0, 2), false);
				return defendingChaser.DefendGoal();
			}
			if (chance == 2)//opposing beater comes to help
			{
				return false;
			}
			if (chance == 3)//opposing chaser fouls
			{
				return false;
			}
			return false;
		}

		//returns false if play is NOT interrupted
		//returns true if play is interrupted
		public bool PlayInterrupt(Chaser chase)
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int chance = rnd.Next(15);

			if (chance == 1)//chaser interrupt
			{
				Chaser opposingChaser = GetChaser(null, chase.GetKey().Substring(0, 2), true);
				return opposingChaser.Interception(chase);
			}
			if (chance == 2 || chance == 3)//beater interrupt
			{
				return GetBeater(chase).Attack(chase);
			}
			return false;
		}

		public bool ChaserInterrupt(Chaser victim)
		{
			//interception
			//foul
			return false;
		}

		public void Score(Player player, int points)
		{
			System.Console.WriteLine(points + " points to " + player.Team() + "!");

			if (int.Parse(player.GetKey().Substring(1, 1)) == 1)
			{
				team1score += points;
			}
			else
			{
				team2score += points;
			}
			System.Console.WriteLine(team1 + ": " + team1score + ", " + team2 + ": " + team2score);

		}

		public static void RevivePlayer()
		{
			knockedOutPlayers.TrimToSize();
			if (knockedOutPlayers.Count > 0)
			{
				Player player = (Player)knockedOutPlayers.Dequeue();
				player.Revive();
			}
		}

		public void KnockOut(Player player)
		{
			knockedOutPlayers.Enqueue(player);
		}

		public void ReviveChance()
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int revive = rnd.Next(10);

			if (revive == 1) RevivePlayer();
		}

		public void PlayCounter()
		{
			playcount++;
			if (chasing) seekCount++;
			if (!chasing) seekCount = 0;
		}

		public void SeekerChance()
		{
			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);
			int seekChance = rnd.Next(5);
			bool ready = CompareCounts();
			bool head = HeadToHeadCount();

			if (seekChance == 0 && !chasing)
			{
				Seeker one = GetSeeker();
				FirstSight(one);
			}

			if (chasing && !chasing2 && seekChance > 1)
			{
				Seeker two = GetSeeker(isChasing);
				SecondSight(two);
			}

			if (chasing && !chasing2 && ready)
			{
				if (isChasing.Catch())
				{
					GameOver();
				}
				else if (catchAttempts > 1)
				{
					isChasing.LostSnitch();
					catchAttempts = 0;
					chasing = false;
				}
				else
				{
					catchAttempts++;
					if (seekChance > 3)
					{
						Seeker two = GetSeeker(isChasing);
						SecondSight(two);
					}
				}
			}
			else if (chasing && chasing2 && ready && !heading)
			{
				System.Console.WriteLine("Seekers go head to head!");
				heading = true;
			}

			if (chasing && chasing2 && ready && heading && head)
			{
				if (isChasing.HeadToHead(isChasing, isFollowing))
				{
					GameOver();
				}
				else if (catchAttempts > 3)
				{
					isChasing.LostSnitch();
					isFollowing.LostSnitch();
					catchAttempts = 0;
					chasing = false;
					chasing2 = false;
					heading = false; 
				}
				else
				{
					catchAttempts++;
				}
			}
		}

		public bool FirstSight(Seeker person)
		{
			if (person.Search())
			{
				chasing = true;
				isChasing = players[person.GetKey()] as Seeker;
				return true;
			}
			else return false;
		}

		public bool SecondSight(Seeker person)
		{
			if (person.Search(true))
			{
				chasing2 = true;
				isFollowing = players[person.GetKey()] as Seeker;
				return true;
			}
			else return false;
		}

		//returns true if count is high enough for Seeker to try to catch
		public bool CompareCounts()
		{
			if (seekCount > 8)
			{
				return true;
			}
			else return false;
		}

		//returns true if count is high enough for Seekers to go head to head
		public bool HeadToHeadCount()
		{
			if (seekCount > 12)
			{
				return true;
			}
			else return false;
		}

		public void GameOver()
		{
			snitchCaught = true;
		}
	}
}