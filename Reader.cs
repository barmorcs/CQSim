using System;
using System.Collections.Generic;
namespace QSim
{
	public class Reader
	{
		static Dictionary<string, Player> players = new Dictionary<string, Player>();
		public static string team1 = "";
		public static string team2 = "";

		public Reader()
		{
		}

		public static Dictionary<string, Player> GetPlayers()
		{
			Play Game = new Play();
			Player T1K = new Keeper();
			Player T1C1 = new Chaser();
			Player T1C2 = new Chaser();
			Player T1C3 = new Chaser();
			Player T1B1 = new Beater();
			Player T1B2 = new Beater();
			Player T1S = new Seeker();

			Player T2K = new Keeper();
			Player T2C1 = new Chaser();
			Player T2C2 = new Chaser();
			Player T2C3 = new Chaser();
			Player T2B1 = new Beater();
			Player T2B2 = new Beater();
			Player T2S = new Seeker();

			System.IO.StreamReader inTeam1 = new System.IO.StreamReader("/Users/Camille/Projects/QSim/QSim/teams/Gryffindor.txt");
			System.IO.StreamReader inTeam2 = new System.IO.StreamReader("/Users/Camille/Projects/QSim/QSim/teams/Slytherin.txt");

			team1 = inTeam1.ReadLine();
			T1K.Setup(team1, inTeam1.ReadLine(), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), bool.Parse(inTeam1.ReadLine()), "T1K");
			players.Add("T1K", T1K);
			T1S.Setup(team1, inTeam1.ReadLine(), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), bool.Parse(inTeam1.ReadLine()), "T1S");
			players.Add("T1S", T1S);
			T1C1.Setup(team1, inTeam1.ReadLine(), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), bool.Parse(inTeam1.ReadLine()), "T1C1", true);
			players.Add("T1C1", T1C1);
			T1C2.Setup(team1, inTeam1.ReadLine(), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), bool.Parse(inTeam1.ReadLine()), "T1C2");
			players.Add("T1C2", T1C2);
			T1C3.Setup(team1, inTeam1.ReadLine(), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), bool.Parse(inTeam1.ReadLine()), "T1C3");
			players.Add("T1C3", T1C3);
			T1B1.Setup(team1, inTeam1.ReadLine(), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), bool.Parse(inTeam1.ReadLine()), "T1B1", true);
			players.Add("T1B1", T1B1);
			T1B2.Setup(team1, inTeam1.ReadLine(), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), int.Parse(inTeam1.ReadLine()), bool.Parse(inTeam1.ReadLine()), "T1B2");
			players.Add("T1B2", T1B2);

			team2 = inTeam2.ReadLine();
			T2K.Setup(team2, inTeam2.ReadLine(), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), bool.Parse(inTeam2.ReadLine()), "T2K");
			players.Add("T2K", T2K);
			T2S.Setup(team2, inTeam2.ReadLine(), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), bool.Parse(inTeam2.ReadLine()), "T2S");
			players.Add("T2S", T2S);
			T2C1.Setup(team2, inTeam2.ReadLine(), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), bool.Parse(inTeam2.ReadLine()), "T2C1", true);
			players.Add("T2C1", T2C1);
			T2C2.Setup(team2, inTeam2.ReadLine(), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), bool.Parse(inTeam2.ReadLine()), "T2C2");
			players.Add("T2C2", T2C2);
			T2C3.Setup(team2, inTeam2.ReadLine(), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), bool.Parse(inTeam2.ReadLine()), "T2C3");
			players.Add("T2C3", T2C3);
			T2B1.Setup(team2, inTeam2.ReadLine(), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), bool.Parse(inTeam2.ReadLine()), "T2B1", true);
			players.Add("T2B1", T2B1);
			T2B2.Setup(team2, inTeam2.ReadLine(), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), int.Parse(inTeam2.ReadLine()), bool.Parse(inTeam2.ReadLine()), "T2B2");
			players.Add("T2B2", T2B2);

			return players;
		}

		public static string GetTeam1()
		{
			return team1;
		}

		public static string GetTeam2()
		{
			return team2;
		}
	}
}