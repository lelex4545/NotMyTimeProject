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
        public Texture2D texture;
        public bool pickup;
        //skalierung
        private int posX;
        private int posY;
        private float scale;
        //inventar skalierung
        private float scale2;
        private int poX;
        private int poY;

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
        public Loot(Rectangle newRectangle, int posX, int posY, float scale, float scale2, int poX, int poY)
        {
            this.Rectangle = newRectangle;
            this.posX = posX;
            this.posY = posY;
            this.scale = scale;

            this.scale2 = scale2;
            this.poX = poX;
            this.poY = poY;

            pickup = false;
        }

        //getTexuture
        /*public Texture2D getTexture()
        {
            return texture;
        } */

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        //Draw Loot
        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            //if (texture != null)
            if (Collided == false && !pickup)
                spriteBatch.Draw(texture, new Vector2(posX, posY), null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            else
            {
                spriteBatch.Draw(texture, new Vector2(x + poX, y + poY), null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), scale2, SpriteEffects.None, 0f);
                pickup = true;
            }
        }

        //Loot soll verschwinden und im Inventar auftauchen
        public bool Collison(Sprite target)
        {
            bool intersects = rectangle.Intersects(target.rectangle);

            Collided = intersects;
            return intersects;
        }
    }
}
