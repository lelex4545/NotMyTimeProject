using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    public class MagicSkillset
    {
        public int ChooseMagic(Fighter f1, Fighter f2, int i)
        {
            if (i == 1)
            {
                return Heal(f1);
            }
            else if (i == 2)
            {
                return Fire(f1, f2);
            }
            else if (i == 3)
            {
                return Ice(f1, f2);
            }
            else if (i == 4)
            {
                return Thunder(f1, f2);
            }
            else
            {
                return LuckyShot(f1, f2);
            }
        }
        public int Heal(Fighter f1)
        {
            f1.Stats.CurrentLP += f1.Stats.Intelligence*2;
            f1.Stats.CurrentLP = (int)Microsoft.Xna.Framework.MathHelper.Clamp(f1.Stats.CurrentLP, 0, f1.Stats.Lifepoints);
            f1.Stats.CurrentMP -= 50;
            return f1.Stats.Intelligence * 2;
        }
        public int Fire(Fighter f1, Fighter f2)
        {
            f2.Stats.CurrentLP = f2.Stats.CurrentLP - (f1.Stats.Intelligence * 3);
            f1.Stats.CurrentMP -= 50;
            return f1.Stats.Intelligence * 3;
        }
        public int Ice(Fighter f1, Fighter f2)
        {
            f2.Stats.CurrentLP = f2.Stats.CurrentLP - (f1.Stats.Intelligence * 2);
            f1.Stats.CurrentMP -= 25;
            return f1.Stats.Intelligence * 2;
        }
        public int Thunder(Fighter f1, Fighter f2)
        {
            f2.Stats.CurrentLP = f2.Stats.CurrentLP - (f1.Stats.Intelligence * 2);
            f1.Stats.CurrentMP -= 25;
            return f1.Stats.Intelligence * 2;
        }
        public int LuckyShot(Fighter f1, Fighter f2)
        {
            return 1;
        }
    }
}
