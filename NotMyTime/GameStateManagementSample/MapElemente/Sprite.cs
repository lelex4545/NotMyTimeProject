using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class Sprite
    {
        protected Texture2D texture;
        public Rectangle rectangle;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        public void UnLoadContent()
        {
            content.Unload();
        }
    }

    class Player : Sprite
    {
        private float lastMove = 0f;
        private float wait = 0f;
        Texture2D[] left, right, top, bottom;
        public const int size = 50;
        public const int size2 = size*2;
        int help = 0;
        bool changefoot = false;
        bool col = false;

        int add = 0;
        char door = ' ';
        public Player(Rectangle newRectangle)
        {
            this.Rectangle = newRectangle;
            left = new Texture2D[5];
            right = new Texture2D[5];
            top = new Texture2D[3];
            bottom = new Texture2D[3];
        }

        public void generatePlayer()
        {
            texture = right[0] = Content.Load<Texture2D>("figure_right");
            right[1] = Content.Load<Texture2D>("figure_right1");
            right[2] = Content.Load<Texture2D>("figure_right2");
            right[3] = Content.Load<Texture2D>("figure_right3");
            right[4] = Content.Load<Texture2D>("figure_right4");

            left[0] = Content.Load<Texture2D>("figure_left");
            left[1] = Content.Load<Texture2D>("figure_left1");
            left[2] = Content.Load<Texture2D>("figure_left2");
            left[3] = Content.Load<Texture2D>("figure_left3");
            left[4] = Content.Load<Texture2D>("figure_left4");

            top[0] = Content.Load<Texture2D>("figur_top");
            top[1] = Content.Load<Texture2D>("figure_top1");
            top[2] = Content.Load<Texture2D>("figure_top2");

            bottom[0] = Content.Load<Texture2D>("figure_bottom");
            bottom[1] = Content.Load<Texture2D>("figure_bottom1");
            bottom[2] = Content.Load<Texture2D>("figure_bottom2");
        }

        public void updatePosition(GameTime gameTime, Map map)
        {
            wait += (float)gameTime.ElapsedGameTime.TotalSeconds;
            lastMove += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Rectangle tmp = new Rectangle(0, 0, 1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                tmp = new Rectangle(rectangle.X, rectangle.Y + size2, rectangle.Width, rectangle.Height);
                foreach (CollisionTiles tile in map.CollisionTiles)
                    if (RectangleHelper.TouchCheck(tmp, tile.Rectangle, size) && (tile.texture.Name.Equals("tile2") || tile.texture.Name.Equals("tile3") ||
                        tile.texture.Name.Equals("tile6") || tile.texture.Name.Equals("tile7") || tile.texture.Name.Equals("tile12") || tile.texture.Name.Equals("tile14")
                        || tile.texture.Name.Equals("tile15") || tile.texture.Name.Equals("tile16") || tile.texture.Name.Equals("tile17") || tile.texture.Name.Equals("tile19")))
                    {
                        col = true;
                        if (texture == right[0] || texture == top[0] || texture == left[0])
                            texture = bottom[0];
                        
                    }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                tmp = new Rectangle(rectangle.X, rectangle.Y - size2, rectangle.Width, rectangle.Height);
                foreach (CollisionTiles tile in map.CollisionTiles)
                    if (RectangleHelper.TouchCheck(tmp, tile.Rectangle,size) && (tile.texture.Name.Equals("tile2") || tile.texture.Name.Equals("tile3") ||
                        tile.texture.Name.Equals("tile6") || tile.texture.Name.Equals("tile7") || tile.texture.Name.Equals("tile12") || tile.texture.Name.Equals("tile14")
                        || tile.texture.Name.Equals("tile15") || tile.texture.Name.Equals("tile16") || tile.texture.Name.Equals("tile17") || tile.texture.Name.Equals("tile19")))
                    {
                        col = true;
                        if (texture == bottom[0] || texture == right[0] || texture == left[0])
                            texture = top[0];
                    }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

                tmp = new Rectangle(rectangle.X - size2, rectangle.Y, rectangle.Width, rectangle.Height);
                foreach (CollisionTiles tile in map.CollisionTiles)
                    if (RectangleHelper.TouchCheck(tmp, tile.Rectangle,size) && (tile.texture.Name.Equals("tile2") || tile.texture.Name.Equals("tile3") ||
                        tile.texture.Name.Equals("tile6") || tile.texture.Name.Equals("tile7") || tile.texture.Name.Equals("tile12") || tile.texture.Name.Equals("tile14")
                        || tile.texture.Name.Equals("tile15") || tile.texture.Name.Equals("tile16") || tile.texture.Name.Equals("tile17") || tile.texture.Name.Equals("tile19")))
                    {
                        col = true;
                        if (texture == bottom[0] || texture == top[0] || texture == right[0])
                            texture = left[0];
                    }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                tmp = new Rectangle(rectangle.X + size2, rectangle.Y, rectangle.Width, rectangle.Height);
                foreach (CollisionTiles tile in map.CollisionTiles)
                    if (RectangleHelper.TouchCheck(tmp, tile.Rectangle, size) && (tile.texture.Name.Equals("tile2") || tile.texture.Name.Equals("tile3") ||
                        tile.texture.Name.Equals("tile6") || tile.texture.Name.Equals("tile7") || tile.texture.Name.Equals("tile12") || tile.texture.Name.Equals("tile14")
                        || tile.texture.Name.Equals("tile15") || tile.texture.Name.Equals("tile16") || tile.texture.Name.Equals("tile17") || tile.texture.Name.Equals("tile19")))
                    {
                        col = true;
                        if(texture == bottom[0] || texture == top[0] || texture == left[0])
                        texture = right[0];
                    }
            }

            

            if (col == false)
            {
                

                if (wait > 0.015f)
                {
                    if (help == 4)
                    {
                        if (changefoot == false)
                        {
                            if(add<50)
                            {
                                rectangle.X = rectangle.X + 5;
                                add += 5;
                            }
                            
                            texture = right[2];
                            if(add == size)
                            {
                                help = 0;
                                add = 0;
                                changefoot = true;
                            }
                            lastMove = 0;
                            wait = 0;
                            
                        }
                        else
                        {
                            if(add < 50)
                            {
                                rectangle.X = rectangle.X + 5;
                                add += 5;
                            }
                            texture = right[4];
                            if (add == size)
                            {
                                help = 0;
                                add = 0;
                                changefoot = false;
                            }
                            lastMove = 0;
                            wait = 0;  
                        }
                    }
                    else
                    if (help == 3)
                    {
                        if (add < 50)
                        {
                            rectangle.Y = rectangle.Y + 5;
                            add += 5;
                        }
                        texture = bottom[2];
                        if (add == size)
                        {
                            help = 0;
                            add = 0;
                        }
                        lastMove = 0;
                        wait = 0;
                    }
                    else
                    if (help == 2)
                    {
                        if (changefoot == false)
                        {
                            if(add<size)
                            {
                                rectangle.X = rectangle.X - 5;
                                add += 5;
                            }
                            
                            texture = left[2];
                            if(add==size)
                            {
                                help = 0;
                                add = 0;
                                changefoot = true;
                            }
                            lastMove = 0;
                            wait = 0;
                        }
                        else
                        {
                            if(add < size)
                            {
                                rectangle.X = rectangle.X - 5;
                                add += 5;
                            }
                            
                            texture = left[4];
                            if(add == size)
                            {
                                help = 0;
                                add = 0;
                                changefoot = false;
                            }
                            lastMove = 0;
                            wait = 0;
                            
                        }
                    }
                    else
                    if (help == 1)
                    {
                        if(add < size)
                        {
                            rectangle.Y = rectangle.Y - 5;
                            add += 5;
                        }
                        
                        texture = top[2];

                        if(add == 50)
                        {
                            help = 0;
                            add = 0;
                        }
                        lastMove = 0;
                        wait = 0;
                    }
                    
                }

                
                if (lastMove > 0.000003f)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W) || door.Equals('w'))
                    {
                        if (door == 'w' || door == ' ')
                        {


                            if (add < size)
                            {
                                rectangle.Y = rectangle.Y - 5;
                                add += 5;
                            }
                            door = 'w';
                            texture = top[1];
                            lastMove = 0f;
                            if (add == size)
                            {
                                help = 1;
                                add = 0;
                                door = ' ';
                            }

                            wait = 0;
                            return;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A) || door.Equals('a'))
                    {
                        if (door == 'a' || door == ' ')
                        {
                            if (changefoot == false)
                            {
                                if (add < size)
                                {
                                    rectangle.X = rectangle.X - 5;
                                    add += 5;
                                }
                                door = 'a';
                                texture = left[1];
                                if (add == size)
                                {
                                    help = 2;
                                    door = ' ';
                                    add = 0;
                                }
                                lastMove = 0f;
                                wait = 0;
                                return;
                            }
                            else
                            {
                                if (add < size)
                                {
                                    rectangle.X = rectangle.X - 5;
                                    add += 5;
                                }
                                door = 'a';
                                texture = left[3];
                                if (add == size)
                                {
                                    help = 2;
                                    door = ' ';
                                    add = 0;
                                }
                                lastMove = 0f;
                                wait = 0;
                                return;
                            }
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S)|| door.Equals('s'))
                    {
                        if (door == 's' || door == ' ')
                        {
                            if (add < size)
                            {
                                rectangle.Y = rectangle.Y + 5;
                                add += 5;
                            }
                            door = 's';
                            texture = bottom[1];
                            lastMove = 0f;
                            if (add == size)
                            {
                                help = 3;
                                add = 0;
                                door = ' ';
                            }
                            wait = 0;
                            return;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D)|| door.Equals('d'))
                    {
                        if (door == 'd' || door == ' ')
                        {
                            if (changefoot == false)
                            {
                                if (add < size)
                                {
                                    rectangle.X = rectangle.X + 5;
                                    add += 5;
                                }
                                door = 'd';
                                texture = right[1];
                                if (add == size)
                                {
                                    help = 4;
                                    add = 0;
                                    door = ' ';
                                }
                                lastMove = 0f;
                                wait = 0;
                                return;
                            }
                            else
                            {
                                if (add < size)
                                {
                                    rectangle.X = rectangle.X + 5;
                                    add += 5;
                                }
                                door = 'd';
                                texture = right[3];
                                if (add == size)
                                {
                                    help = 4;
                                    add = 0;
                                    door = ' ';
                                }
                                lastMove = 0f;
                                wait = 0;
                                return;
                            }
                        }
                    }
                }
                
            }
            if (lastMove > 0.5f)
            {
                if (texture == right[2] || texture == right[4])
                {
                    texture = right[0];
                }
                if (texture == left[2] || texture == left[4])
                {
                    texture = left[0];
                }
                if (texture == top[2])
                {
                    texture = top[0];
                }
                if (texture == bottom[2])
                {
                    texture = bottom[0];
                }
            }
            col = false;
        }
    }
}
