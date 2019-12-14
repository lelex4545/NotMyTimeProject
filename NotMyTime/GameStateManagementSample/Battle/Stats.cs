using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    public class Stats
    {
        public int Lifepoints { get; set; }
        public int Manapoints { get; set; }
        public int CurrentLP { get; set; }
        public int CurrentMP { get; set; }
        public int Damage { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public Stats(int lp, int mp, int dmg, int str, int ag, int intel)
        {
            Lifepoints = lp;
            Manapoints = mp;
            CurrentLP = lp;
            CurrentMP = mp;
            Damage = dmg;
            Strength = str;
            Agility = ag;
            Intelligence = intel;
        }
        public void LevelUp()
        {
            Lifepoints += 10;
            Manapoints += 10;
            CurrentLP = Lifepoints;
            CurrentMP = Manapoints;
            Damage += 2;
            Strength += 1;
            Agility += 1;
            Intelligence += 1;
        }
    }
}
