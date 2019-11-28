using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotMyTime
{
    public class BasicStats
    {
        public int Level { get; set; }
        public int Lifepoints { get; set; }
        public int Manapoints { get; set; }
        public int CurrentLP { get; set; }
        public int CurrentMP { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public BasicStats(int lvl, int lp, int mp, int dmg, int def)
        {
            Level = lvl;
            Lifepoints = lp;
            Manapoints = mp;
            CurrentLP = lp;
            CurrentMP = mp;
            Damage = dmg;
            Defense = def;
        }
    }
    public class Stats : BasicStats
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public Stats(int lvl, int lp, int mp, int dmg, int def, int str, int ag, int intel) : base(lvl, lp, mp, dmg, def)
        {
            Strength = str;
            Agility = ag;
            Intelligence = intel;
        }
    }
}
