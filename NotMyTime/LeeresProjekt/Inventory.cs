using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotMyTime
{



    class Inventory
    {
        private Texture2D texture;
        bool invOpen = false;
        bool keyBlock = true;
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
 //-----------------------------------------------------------------------
        //Inventar drawn
        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            if(texture != null)
                //spriteBatch.Draw(texture, rectangle, Color.White);
                spriteBatch.Draw(texture, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2), null, Color.White, 0.0f, new Vector2(texture.Width/2, texture.Height/2), 1f, SpriteEffects.None, 0f);
        }

        //Constructor
        public Inventory()
        {
            
        }

        //Wenn I gedrückt wird öffnet/schließt sich das Inventar
        public void openInventory(GameTime gameTime)
        {
            lastChange += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyUp(Keys.I))
                keyBlock = true;

            if(keyBlock)
            if (lastChange >= 0.1f)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.I))
                {
                    if (invOpen == false)
                    {
                        texture = Content.Load<Texture2D>("inventory");
                        invOpen = true;
                    }
                    else
                    {
                        texture = null;
                        invOpen = false;
                    }
                        keyBlock = false;
                }
            lastChange = 0f;
            }
        }

        //Einstellungen vom Rectangle
        public void generateInventory(GraphicsDeviceManager graphics)
        {
            //rectangle = new Rectangle(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2, 700, 700);
        }
    }
}
