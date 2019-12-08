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
    class Gold
    {
        //Gold Anzeige
        private SpriteFont font;
        private SpriteFont font1;
        private static int score = 0;

        //Gold loot
        private Texture2D texture;
        private int value;
        private int posX;
        private int posY;
        private float scale;
        private float currentTime;
        private bool fontUpdate = false;
        private bool pickup = false;
        //--------------------------------------------------
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
        //---------------------------------------------------

        public Gold(int value, int x, int y, int scaleX, int scaleY, int posX, int posY, float scale)
        {
            this.value = value;
            this.Rectangle = new Rectangle(x, y, scaleX, scaleY);
            this.posX = posX;
            this.posY = posY;
            this.scale = scale;
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
            font = content.Load<SpriteFont>("Arial1");
            font1 = content.Load<SpriteFont>("Arial2");
        }

        public void updateFont(GameTime gameTime)
        {
            if(pickup)
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= 1.0f && pickup)
            {
                fontUpdate = true;
                currentTime = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            spriteBatch.DrawString(font, "Gold: " + score, new Vector2(x + 758, y + 440), Color.Gold);

            if (Collided == false && !pickup)
            {
                spriteBatch.Draw(texture, new Vector2(posX, posY), null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            }
            else 
            {
                texture = null;
                rectangle = Rectangle.Empty;
                if(!fontUpdate)
                    spriteBatch.DrawString(font1, "+" + value, new Vector2(posX-25, posY-25), Color.Black);
                if (pickup == false)
                    score += value;
                pickup = true;
            }
        }

        public bool Collison(Sprite target)
        {
            bool intersects = rectangle.Intersects(target.rectangle);
            Collided = intersects;

            return intersects;
        }
    }
}