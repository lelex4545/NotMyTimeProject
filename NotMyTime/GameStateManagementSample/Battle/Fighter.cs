using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GameStateManagement
{
    public abstract class Fighter
    {
        public Texture2D Model { get; set; }
        public SpriteFont StatFont { get; set; }
        public string Name { get; }
        public Stats Stats { get; set; }
        public MagicSkillset Skills { get; set; }
        public Fighter(String name)
        {
            Name = name;
            Skills = new MagicSkillset();
        }
        public string Ausgabe()
        {
            return Name + "    ";
        }
        public string HealthText()
        {
            return this.Stats.CurrentLP + "/" + this.Stats.Lifepoints;
        }
        public string ManaText()
        {
            return this.Stats.CurrentMP + "/" + this.Stats.Manapoints;
        }
        public abstract int AttackCalc();
        public int Attack(Fighter f2)
        {
            f2.Stats.CurrentLP = f2.Stats.CurrentLP - (this.AttackCalc());
            return this.AttackCalc();
        }
        public int Magic(Fighter f2, int i)
        {
            return this.Skills.ChooseMagic(this, f2, i);
        }
        public int compareSpeed(Fighter f2)
        {
            return (this.Stats.Agility > f2.Stats.Agility) ? 1 : ((this.Stats.Agility < f2.Stats.Agility) ? -1 : 0);
        }
        public bool isAlive()
        {
            return this.Stats.CurrentLP > 0 ? true : false;
        }
    }

    public class MainFighter : Fighter
    {
        public Levelsystem Level;
        public MainFighter(String name, int lp, int mp, int dmg, int str, int ag, int intel) : base(name)
        {
            Stats = new Stats(lp, mp, dmg, str, ag, intel);
            Level = new Levelsystem(this);
        }
        public override int AttackCalc()
        {
            return Stats.Strength + Stats.Damage + Stats.Agility / 2;
        }

        public new string Ausgabe()
        {
            return base.Ausgabe() + "Level: " + Level.GetLevel() + "   Damage: " + AttackCalc() + "   EXP: " + Level.GetExp() + "/" + Level.GetNeededExp();
        }

        public int Attack(Fighter enemy, int i)
        {
            if (i > 0) return this.Magic(enemy, i);
            else return this.Attack(enemy);
        }
    }

    public class EnemyFighter : Fighter
    {
        public int GivenExp { get; }
        public int Level { get; }

        public EnemyFighter(String name, int lvl, int lp, int mp, int dmg, int str, int ag, int intel, int exp) : base(name)
        {
            Stats = new Stats(lp, mp, dmg, str, ag, intel);
            Level = lvl;
            GivenExp = exp;
        }
        public override int AttackCalc()
        {
            return Stats.Damage + Stats.Strength;
        }
        public new string Ausgabe()
        {
            return base.Ausgabe() + "Level: " + Level;
        }
    }

}
