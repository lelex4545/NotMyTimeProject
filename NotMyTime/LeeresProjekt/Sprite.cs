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
    class Sprite
    {
        protected Texture2D texture;
        protected Rectangle rectangle;


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
    }

    class Player : Sprite
    {
        private float lastMove = 0f;
        private float wait = 0f;
        Texture2D[] left, right, top;
        public const int size = 50;
        int help = 0;
        bool changefoot = false;
        public Player(Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("figure_right");
            this.Rectangle = newRectangle;
            left = new Texture2D[5];
            right = new Texture2D[5];
            top = new Texture2D[3];
        }

        public void generatePlayer()
        {
            right[0] = Content.Load<Texture2D>("figure_right");
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
        }

        public void updatePosition(GameTime gameTime)
        {
            wait += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (wait > 0.2f)
            {
                if (help == 4)
                {
                    if (changefoot == false)
                    {
                        rectangle.X = rectangle.X + size;
                        texture = right[2];
                        help = 0;
                        wait = 0;
                        changefoot = true;
                    }
                    else
                    {
                        rectangle.X = rectangle.X + size;
                        texture = right[4];
                        help = 0;
                        wait = 0;
                        changefoot = false;
                    }
                }
                else
                if (help == 3)
                {

                }
                else
                if (help == 2)
                {
                    if (changefoot == false)
                    {
                        rectangle.X = rectangle.X - size;
                        texture = left[2];
                        help = 0;
                        wait = 0;
                        changefoot = true;
                    }
                    else
                    {
                        rectangle.X = rectangle.X - size;
                        texture = left[4];
                        help = 0;
                        wait = 0;
                        changefoot = false;
                    }
                }
                else
                if (help == 1)
                {
                    rectangle.Y = rectangle.Y - size;
                    texture = top[2];
                    help = 0;
                    wait = 0;
                }
            }

            lastMove += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lastMove > 0.4f)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    rectangle.Y = rectangle.Y - size;
                    texture = top[1];
                    lastMove = 0f;
                    help = 1;
                    wait = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (changefoot == false)
                    {
                        rectangle.X = rectangle.X - size;
                        texture = left[1];
                        lastMove = 0f;
                        help = 2;
                        wait = 0;
                    }
                    else
                    {
                        rectangle.X = rectangle.X - size;
                        texture = left[3];
                        lastMove = 0f;
                        help = 2;
                        wait = 0;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    rectangle.Y = rectangle.Y + size;

                    lastMove = 0f;
                    help = 3;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (changefoot == false)
                    {
                        rectangle.X = rectangle.X + size;
                        texture = right[1];
                        lastMove = 0f;
                        help = 4;
                        wait = 0;
                    }
                    else
                    {
                        rectangle.X = rectangle.X + size;
                        texture = right[3];
                        lastMove = 0f;
                        help = 4;
                        wait = 0;
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
            }
        }
    }
}
