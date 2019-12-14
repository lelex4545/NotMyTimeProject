using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Sprite target)
        {
            var position = Matrix.CreateTranslation(-target.rectangle.X - (target.rectangle.Width / 2), -target.rectangle.Y - (target.rectangle.Height / 2), 0);

            var offset = Matrix.CreateTranslation(1920 / 2, 1080 / 2, 0);
            Transform = position * offset;
        }
    }
}
