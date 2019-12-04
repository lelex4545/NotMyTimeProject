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
        //Inventar drawn         GraphicsDeviceManager graphics        new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2)
        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            if(texture != null)
                spriteBatch.Draw(texture,new Vector2(x + 870, y + 420) , null, Color.White, 0.0f, new Vector2(texture.Width/2, texture.Height/2), 1.3f, SpriteEffects.None, 0f);

        }

        //Constructor
        public Inventory()
        {
            
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        //Wenn I gedrückt wird öffnet/schließt sich das Inventar
        /*   public void openInventory(GameTime gameTime)
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
           }*/

    }
}
