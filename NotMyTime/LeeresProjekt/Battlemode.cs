using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NotMyTime
{
    class Battlemode
    {
        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        
        Texture2D fight_menu;
        Texture2D fight_button;
        Vector2[] btnPos;
        int btnIndex = 0;
        Vector2 actualBtn;

        Fighter mainChar;
        Fighter enemy1;

        static int sleep = 50;

        public Battlemode()
        {
            mainChar = new MainFighter("Bruce", 1, 100, 100, 15);
            enemy1 = new EnemyFighter("Stony", 1, 50, 50, 10);
            btnPos = new Vector2[3];
            btnPos[0] = new Vector2(-400, -42);
            btnPos[1] = new Vector2(-400, 45);
            btnPos[2] = new Vector2(-400, 125);
            actualBtn = btnPos[0];
        }

        public void generateBattle()
        {
            fight_menu = Content.Load<Texture2D>("fight_menu");

            mainChar.Lifepoints = Content.Load<SpriteFont>("Arial");

            enemy1.Lifepoints = Content.Load<SpriteFont>("Arial");

            mainChar.Model = Content.Load<Texture2D>("figure_left");

            enemy1.Model = Content.Load<Texture2D>("Hohlenmensch_Right");

            fight_button = Content.Load<Texture2D>("auswahlblock");
        }

        public void updateMovement(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) == true)
            {
                btnIndex = (btnIndex - 1) % 3;
                if (btnIndex < 0) btnIndex = 2;
                actualBtn = btnPos[btnIndex];
                System.Threading.Thread.Sleep(sleep);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) == true)
            {
                btnIndex = (btnIndex + 1) % 3;
                actualBtn = btnPos[btnIndex];
                System.Threading.Thread.Sleep(sleep);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {
                if (btnIndex == 0)
                {
                    enemy1.CurrentHealth -= mainChar.BaseDamage;
                    mainChar.CurrentHealth -= enemy1.BaseDamage;
                    System.Threading.Thread.Sleep(sleep + 15);

                }
                else if (btnIndex == 1)
                {

                }
                else if (btnIndex == 2)
                {

                }
            }
        }

        public void drawBattle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fight_menu, new Vector2(0, 100),
                null, Color.White);

            spriteBatch.Draw(fight_button, actualBtn, null, Color.White);

            spriteBatch.Draw(mainChar.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 + 300, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 200), Color.White);

            spriteBatch.Draw(enemy1.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 400, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 200), Color.White);

            spriteBatch.DrawString(mainChar.Lifepoints, mainChar.ausgabe(),
                new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 50, spriteBatch.GraphicsDevice.Viewport.Height / 2 + 65), Color.White); ;

            spriteBatch.DrawString(enemy1.Lifepoints, enemy1.ausgabe(),
                new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 50, spriteBatch.GraphicsDevice.Viewport.Height / 2 + 145), Color.White);
        }
    }
}