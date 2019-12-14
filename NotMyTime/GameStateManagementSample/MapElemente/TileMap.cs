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
    class Tiles
    {
        public Texture2D texture;
        private Rectangle rectangle;
        public int i;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }
        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public Tiles()
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

        public static void LoadContent(ContentManager Content)
        {
            Tiles.Content = new ContentManager(Content.ServiceProvider, "Content");
        }
        public static void UnLoadContent()
        {
            Tiles.content.Unload();
        }
        
    }

    class CollisionTiles : Tiles
    {
        
        public CollisionTiles(int i, Rectangle newRectangle)
        {       
                texture = Content.Load<Texture2D>("tile" + i);
                this.Rectangle = newRectangle;
                this.i = i;
            
        }

        public void loadC()
        {
            texture = Content.Load<Texture2D>("tile" + i);
        }
    }
}
