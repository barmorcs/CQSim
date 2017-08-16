using System;
namespace QSim
{
	public class Player
	{
		string fullName;
		string team;
		int reflex;
		int speed;
		int strength;
		int sight;
		int aim;
		int reflexP;
		int speedP;
		int strengthP;
		int sightP;
		int aimP;
		int size;
		int hp;
		bool playsDirty = false;
		string key;
		bool lead = false;
		bool knockedOut = false;

		Play Game = new Play();

		public Player()
		{
		}

		public void Setup(string teamName, string name, int refl, int spd, int str, int si, int coord, int siz, bool fouls, string inKey, bool isLead = false)
		{
			fullName = name;
			reflex = refl;
			speed = spd;
			strength = str;
			sight = si;
			aim = coord;
			size = siz;
			playsDirty = fouls;
			hp = 20;
			key = inKey;
			lead = isLead;
			team = teamName;
			SetPermStats();

		}

		public void SetPermStats()
		{
			reflexP = reflex;
			speedP = speed;
			strengthP = strength;
			sightP = sight;
			aimP = aim;
		}

		public string Name()
		{
			return fullName;
		}

		public string Team()
		{
			return team;
		}

		public int Reflex()
		{
			return reflex;
		}

		public int Speed()
		{
			return speed;
		}

		public int Strength()
		{
			return strength;
		}

		public int Sight()
		{
			return sight;
		}

		public int Aim()
		{
			return aim;
		}

		public int StatsTotal()
		{
			return reflex + speed + strength + sight + aim;
		}

		public int Size()
		{
			return size;
		}

		public int Health()
		{
			return hp;
		}

		public bool PlaysDirty()
		{
			return playsDirty;
		}

		public string GetKey()
		{
			return key;
		}

		public bool IsLead()
		{
			return lead;
		}

		public bool IsKnockedOut()
		{
			return knockedOut;
		}

		public bool Foul()
		{
			return true;
		}

		public bool DefendFoul()
		{
			return true;
		}

		// accepts a positive integer that is how strong the hit was
		// returns true if bludger was avoided
		// returns false if player was hit
		public bool AvoidBludger(int help = 0)
		{
			int threshold = 35;
			int sloth = 45;

			int Seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(Seed);

			int roll = rnd.Next(1, 21);
			int damage = rnd.Next(3, 6);
			int check = roll + this.Speed() + this.Reflex() + this.Strength() - help;
			if (check < threshold || roll == 1)
			{
				int loss = (help/2) + damage;
				if (loss < 1)
					loss = 4;

				hp = hp - loss;

				if (hp <= 0)
				{
					System.Console.WriteLine(this.Name() + " was knocked out by a Bludger!");
					this.knockedOut = true;

					if (this.GetKey().Contains("C")) Game.SetBaller();

					Game.KnockOut(this);

					return false;
				}

				System.Console.WriteLine(this.Name() + " was hit by a Bludger! Lost " + loss + " HP, "
										 + this.Health() + " remaining");
                this.UpdateStats();
				if (this.GetKey().Contains("C"))
					Game.SetBaller();

				return false;
			}
			if (check > sloth || roll==20)
			{
				System.Console.WriteLine(this.Name() + " dodged the Bludger with a Sloth Grip Roll");
				return true;
			}
			else
			{
				System.Console.WriteLine(this.Name() + " dodged the Bludger");
				return true;
			}
		}

		public void Revive()
		{
			reflex = reflexP - 2;
			speed = speedP - 2;
			strength = strengthP - 2;
			sight = sightP - 2;
			aim = aimP - 2;
			hp = 15;
			knockedOut = false;
			System.Console.WriteLine(this.Name() + " has been revived and returned to the game!");
		}

		public void Hurt(int hurt)
		{
			hp = hp - hurt;

			if (hp <= 0)
			{
				System.Console.WriteLine(this.Name() + " was knocked out!");
				this.knockedOut = true;
				if (this.GetKey().Contains("C")) Game.SetBaller();
				Game.KnockOut(this);
			}
			else
			{
				System.Console.WriteLine(this.Name() + " lost " + hurt + " HP, " + this.Health() + " remaining");
				this.UpdateStats();
			}
		}

		public void UpdateStats()
		{
			if (hp <= 5)
			{
				reflex--;
				sight--;
				aim--;
				strength--;
				speed--;
			}
			else if (hp <= 8)
			{
				strength--;
				speed--;
			}
		}
	}
}