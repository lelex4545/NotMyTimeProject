using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class CollisionChecker
    {
        public bool Collision { get; set; }
        public Enemy Enemy { get; set; }
        public CollisionChecker(bool collision, Enemy enemy)
        {
            this.Collision = collision;
            this.Enemy = enemy;
        }
    }
}
