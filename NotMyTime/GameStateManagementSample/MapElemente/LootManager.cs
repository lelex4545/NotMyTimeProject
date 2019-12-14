using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class LootManager
    {
        public int poX = 0;
        public int poY = 0;
        public int x = 0;
        public int y = 0;
        public float scale2 = 0f;
        public Texture2D texture;
        //---------------------------------------------------------------
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

        //---------------------------------------------------------------- Klasse nicht eingebaut -> müll

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice, String assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           // if (texture != null)
                spriteBatch.Draw(texture, new Vector2(x + poX, y + poY), null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), scale2, SpriteEffects.None, 0f);

        }

        public void updateInv(ContentManager content, GraphicsDevice graphicsDevice, String assetName)
        {
            texture = content.Load<Texture2D>(assetName);

        }

        public void updatePos(int x, int y, int poX, int poY, float scale2)
        {
            this.x = x;
            this.y = y;
            this.poX = poX;
            this.poY = poY;
            this.scale2 = scale2;
        }

    }
}
