using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotMyTime
{
    public class MagicSkillset
    {
        public void chooseMagic(Fighter f1, Fighter f2, int i)
        {
            if (i == 1)
            {
                Heal(f1);
            }
            else if (i == 2)
            {
                Fire(f1, f2);
            }
            else if (i == 3)
            {
                Ice(f1, f2);
            }
            else if (i == 4)
            {
                Thunder(f1, f2);
            }
            else if (i == 5)
            {
                LuckyShot(f1, f2);
            }
        }
        public void Heal(Fighter f1)
        {
            f1.Stats.CurrentLP += 10 + f1.Stats.Intelligence;
            f1.Stats.CurrentLP = (int)Microsoft.Xna.Framework.MathHelper.Clamp(f1.Stats.CurrentLP, 0, f1.Stats.Lifepoints);
            f1.Stats.CurrentMP -= 25;
        }
        public void Fire(Fighter f1, Fighter f2)
        {
            f2.Stats.CurrentLP = f2.Stats.CurrentLP - (f1.Stats.Intelligence * 3 - f2.Stats.Defense / 4);
            f1.Stats.CurrentMP -= 25;
        }
        public void Ice(Fighter f1, Fighter f2)
        {

        }
        public void Thunder(Fighter f1, Fighter f2)
        {

        }
        public void LuckyShot(Fighter f1, Fighter f2)
        {

        }
        public void Haste(Fighter f1, Fighter f2)
        {

        }
    }
}
