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
        public Loot(Rectangle newRectangle)
        {
            this.Rectangle = newRectangle;

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
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, new Vector2(200, 400), null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
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
