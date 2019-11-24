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
    public class Fighter
    {
        public String Name { get; }
        public int MaxHealth { get; }
        public int CurrentHealth { get; set; }
        public int MaxMana { get; set; }
        public int CurrentMana { get; set; }
        public int Level { get; set; }
        public int BaseDamage { get; set; }
        public Texture2D Model { get; set; }
        public SpriteFont Lifepoints { get; set; }
        public SpriteFont Manapoints { get; set; }

        public Fighter(String name, int level, int maxHealth, int maxMana, int baseDamage)
        {
            Name = name;
            Level = level;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            MaxMana = maxMana;
            CurrentMana = maxMana;
            BaseDamage = baseDamage;
        }

        public string ausgabe()
        {
            return Name + "   Level: " + Level + "   HP: " + CurrentHealth + "/"+ MaxHealth + "   Mana: " + CurrentMana + "/" + MaxMana;
        }
    }

    public class MainFighter : Fighter
    {
        public int Experience { get; set; }
        public MainFighter(String name, int level, int maxHealth, int maxMana, int baseDamage) : base(name, level, maxHealth, maxMana, baseDamage)
        {

        }
    }

    public class EnemyFighter : Fighter
    {
        public int expPoints { get; set; }
        public EnemyFighter(String name, int level, int maxHealth, int maxMana, int baseDamage) : base(name, level, maxHealth, maxMana, baseDamage)
        {

        }
    }

}
