using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotMyTime
{
    public class Levelsystem
    {
        private int level;
        private int currentExp;
        private int neededExp;
        private MainFighter mf;

        public Levelsystem(MainFighter mf)
        {
            level = 1;
            currentExp = 0;
            neededExp = 100;
            this.mf = mf;
        }
        public int GetLevel()
        {
            return level;
        }
        public int GetNeededExp()
        {
            return neededExp;
        }
        public int GetExp()
        {
            return currentExp;
        }

        public void SetExp(EnemyFighter ef)
        {
            if (level < 10)
            {
                currentExp += ef.GivenExp;
                if (currentExp >= neededExp)
                {
                    level++;
                    mf.Stats.LevelUp();
                    currentExp = currentExp - neededExp;
                    neededExp = 100 * level;
                }
            }
        }
    }
}
