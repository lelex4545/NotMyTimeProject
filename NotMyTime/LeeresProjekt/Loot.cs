using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotMyTime
{
    class Loot
    {
        public static int status;
        public Texture2D texture;
        public bool pickup;

        //skalierung
        private int posX;
        private int posY;
        private float scale;
        //inventar skalierung
        public float scale2;
        public int poX;
        public int poY;

        float lastChange;

        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        private static ContentManager content;

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public bool Collided { get; private set; }

        //---------------------------------------------
        //Constructor
        public Loot(int x, int y, int scaleX, int scaleY, int posX, int posY, float scale, float scale2, int poX, int poY)
        {
            this.Rectangle = new Rectangle(x, y, scaleX, scaleY);
            this.posX = posX;
            this.posY = posY;
            this.scale = scale;

            this.scale2 = scale2;
            this.poX = poX;
            this.poY = poY;

            pickup = false;
            status = 0;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        //Draw Loot
        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            if (Collided == false && !pickup && status < 2)
            {
                spriteBatch.Draw(texture, new Vector2(posX, posY), null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            }
            else if (status == 2 && pickup == true)
            {
                spriteBatch.Draw(texture, new Vector2(posX + 900, posY + 900), null, Color.Transparent, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            }
            else if (status < 2)
            {
                spriteBatch.Draw(texture, new Vector2(x + poX, y + poY), null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), scale2, SpriteEffects.None, 0f);
                rectangle.Offset(900, 50);
                pickup = true;
            }

        }

        //Loot soll verschwinden und im Inventar auftauchen
        public bool Collison(Sprite target, GameTime gameTime)
        {
            bool intersects = rectangle.Intersects(target.rectangle);
            Collided = intersects;
            lastChange += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Collided == true && lastChange > 3.0f)
                status++;
            return intersects;
        }
    }
}
