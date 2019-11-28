using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace NotMyTime
{
    public abstract class Fighter
    {
        public String Name { get; }
        public Texture2D Model { get; set; }
        public SpriteFont StatFont { get; set; }
        public Stats Stats { get; set; }
        public Texture2D Healthbar { get; set; }
        public Texture2D Healthbar2 { get; set; }
        public MagicSkillset Skills { get; set; }
        public Fighter(String name)
        {
            Name = name;
            Skills = new MagicSkillset();
        }
        public string Ausgabe()
        {
            return Name + "   Level: " + Stats.Level;
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
        public void Attack(Fighter f2)
        {
            f2.Stats.CurrentLP = f2.Stats.CurrentLP - (this.AttackCalc() - f2.Stats.Defense / 2);
        }
        public void Magic(Fighter f2, int i)
        {
            this.Skills.chooseMagic(this, f2, i);
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
        public int Exp { get; set; }
        public MainFighter(String name, int lvl, int lp, int mp, int dmg, int def, int str, int ag, int intel) : base(name)
        {
            Stats = new Stats(lvl, lp, mp, dmg, def, str, ag, intel);
            Exp = 0;
        }

        public override int AttackCalc()
        {
            return Stats.Strength + Stats.Damage + Stats.Agility / 2;
        }

        public string Ausgabe()
        {
            return base.Ausgabe() + "   Damage:" + AttackCalc() + "   EXP:" + Exp;
        }

        public void Attack(Fighter enemy, int i)
        {
            if (i > 0) this.Magic(enemy, i);
            else this.Attack(enemy);
        }
    }

    public class EnemyFighter : Fighter
    {
        public int ExpPoints { get; }

        public EnemyFighter(String name, int lvl, int lp, int mp, int dmg, int def, int str, int ag, int intel, int exp) : base(name)
        {
            Stats = new Stats(lvl, lp, mp, dmg, def, str, ag, intel);
            ExpPoints = exp;
        }
        public override int AttackCalc()
        {
            return Stats.Damage + Stats.Strength;
        }

        public void RollAttack(Fighter main)
        {
            Random zufall = new Random();
            if (zufall.Next(0, 5) < 4)
            {
                this.Attack(main);
            }
            else
            {
                Random zufall2 = new Random();
                this.Magic(main, zufall2.Next(1, 4));
            }
        }
    }

}
