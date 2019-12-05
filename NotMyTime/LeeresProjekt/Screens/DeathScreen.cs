using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotMyTime.Screens
{
    public class DeathChecker
    {
        public bool isFighting { get; set; }
        public int number { get; set; }
        public DeathChecker(bool isFighting, int number)
        {
            this.isFighting = isFighting;
            this.number = number;
        }
    }
}
