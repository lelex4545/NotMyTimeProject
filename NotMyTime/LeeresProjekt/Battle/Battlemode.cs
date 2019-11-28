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
        Texture2D menu_button;
        Texture2D magic_button;

        Vector2[] btnPos;
        int btnIndex = 0;
        Vector2 actualBtn;

        bool magicMenu = false;
        Vector2[] magicBtnPos;
        Vector2 actualMagicBtn;

        float elapsedTime;
        float delay = 100f;

        SpriteFont consoleBattle;
        string battlelog = "";

        Texture2D Healthbar;
        Texture2D Healthbar2;
        SpriteFont HealthText;

        MainFighter mainChar;
        EnemyFighter enemy;
        public Battlemode()
        {
            mainChar = new MainFighter("Bruce", 1, 100, 100, 15, 10, 15, 15, 15);
            enemy = new EnemyFighter("Stony", 1, 125, 100, 15, 10, 10, 10, 10, 50);
            btnPos = new Vector2[2];
            btnPos[0] = new Vector2(0, 0);
            btnPos[1] = new Vector2(0, 86);
            actualBtn = btnPos[0];

            magicBtnPos = new Vector2[4];
            magicBtnPos[0] = new Vector2(990, 770);
            magicBtnPos[1] = new Vector2(990, 806);
            magicBtnPos[2] = new Vector2(990, 842);
            magicBtnPos[3] = new Vector2(990, 878);
        }

        public void generateBattle()
        {
            fight_menu = Content.Load<Texture2D>("fight_menu3");

            mainChar.StatFont = Content.Load<SpriteFont>("Arial");

            enemy.StatFont = Content.Load<SpriteFont>("Arial");

            mainChar.Model = Content.Load<Texture2D>("figure_left");

            enemy.Model = Content.Load<Texture2D>("Hohlenmensch_Right");

            menu_button = Content.Load<Texture2D>("menu_select");

            Healthbar = Content.Load<Texture2D>("health_white");

            Healthbar2 = Content.Load<Texture2D>("healthbar_black");

            HealthText = Content.Load<SpriteFont>("Arial");

            magic_button = Content.Load<Texture2D>("menu_select2");

            consoleBattle = Content.Load<SpriteFont>("Arial");
        }

        public void updateBattle(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= delay)
            {
                if (mainChar.Stats.CurrentLP > 0 && enemy.Stats.CurrentLP > 0)
                {
                    if (!magicMenu)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Up) == true)
                        {
                            btnIndex = (int)Mod(btnIndex - 1, 2);
                            actualBtn = btnPos[btnIndex];
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down) == true)
                        {
                            btnIndex = (int)Mod(btnIndex + 1, 2);
                            actualBtn = btnPos[btnIndex];
                        }
                        if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Right)) == true)
                        {
                            if (btnIndex == 0)
                            {
                                Fight(mainChar, enemy, 0);
                            }
                            else if (btnIndex == 1)
                            {
                                magicMenu = true;
                                btnIndex = 0;
                                actualMagicBtn = magicBtnPos[0];
                            }
                        }
                    }
                    else if (magicMenu)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Up) == true)
                        {
                            btnIndex = (int)Mod(btnIndex - 1, 4);
                            actualMagicBtn = magicBtnPos[btnIndex];
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down) == true)
                        {
                            btnIndex = (int)Mod(btnIndex + 1, 4);
                            actualMagicBtn = magicBtnPos[btnIndex];
                        }

                        if ((Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Back)) == true)
                        {
                            btnIndex = 1;
                            actualBtn = btnPos[btnIndex];
                            magicMenu = false;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right) == true)
                        {
                            if (btnIndex == 0)
                                Fight(mainChar, enemy, 1);
                            else if (btnIndex == 1)
                                Fight(mainChar, enemy, 2);
                            else if (btnIndex == 2)
                                Fight(mainChar, enemy, 3);
                            else
                                Fight(mainChar, enemy, 4);
                            magicMenu = false;
                            btnIndex = 0;
                            actualBtn = btnPos[btnIndex];
                        }
                    }
                    enemy.Stats.CurrentLP = (int)MathHelper.Clamp(enemy.Stats.CurrentLP, 0, enemy.Stats.Lifepoints);
                    enemy.Stats.CurrentMP = (int)MathHelper.Clamp(enemy.Stats.CurrentMP, 0, enemy.Stats.Manapoints);
                    mainChar.Stats.CurrentLP = (int)MathHelper.Clamp(mainChar.Stats.CurrentLP, 0, mainChar.Stats.Lifepoints);
                    mainChar.Stats.CurrentMP = (int)MathHelper.Clamp(mainChar.Stats.CurrentMP, 0, mainChar.Stats.Manapoints);
                }
                elapsedTime = 0;
            }
        }

        void Fight(MainFighter main, EnemyFighter enemy, int i)
        {
            int result = main.compareSpeed(enemy);
            if (result == 1)
            {
                main.Attack(enemy, i);
                if (enemy.isAlive())
                    enemy.RollAttack(main);
            }
            else if (result == -1)
            {
                enemy.RollAttack(main);
                if (main.isAlive())
                    main.Attack(enemy, i);
            }
            else
            {
                if (RandomNumber(0, 2) == 0.0)
                {
                    main.Attack(enemy, i);
                    if (enemy.isAlive())
                        enemy.RollAttack(main);
                }
                else
                {
                    enemy.RollAttack(main);
                    if (main.isAlive())
                        main.Attack(enemy, i);
                }
            }
        }

        public void drawBattle(SpriteBatch spriteBatch)
        {
            DrawLayout(spriteBatch);

            if (mainChar.Stats.CurrentLP > 0)
                spriteBatch.Draw(mainChar.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 + 300, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 200), null, Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);

            if (enemy.Stats.CurrentLP > 0)
                spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 400, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 200), Color.White);
        }
        void DrawLayout(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fight_menu, new Vector2(0, 0),
                  null, Color.White);

            if (!magicMenu)
                spriteBatch.Draw(menu_button, actualBtn, null, Color.White);
            else
            {
                spriteBatch.Draw(menu_button, actualBtn, null, Color.Gray);
                spriteBatch.Draw(magic_button, actualMagicBtn, null, Color.White);
                spriteBatch.DrawString(HealthText, "Heal     25MP\n" + "Fire     25MP\n" + "Ice     25MP\n" + "Thunder     25MP\n", new Vector2(1000, 780), Color.AntiqueWhite);
            }
            //Lebensbalken MainChar
            spriteBatch.Draw(Healthbar,
                new Rectangle(990, 202,
                    (int)(Healthbar.Width * ((double)mainChar.Stats.CurrentLP / mainChar.Stats.Lifepoints)), Healthbar.Height),
                new Rectangle(0, 0, Healthbar.Width, Healthbar.Height), Color.Red);

            spriteBatch.Draw(Healthbar2, new Vector2(989, 200),
                  null, Color.White);

            spriteBatch.DrawString(HealthText, mainChar.HealthText(), new Vector2(1350, 206), Color.AntiqueWhite);
            //Vector2 origin1 = HealthText.MeasureString(mainChar.HealthText()) / 2f;
            //spriteBatch.DrawString(HealthText, mainChar.HealthText(), new Vector2(1400, 220), Color.AntiqueWhite, 0f, null, 0f, SpriteEffects.None, 0f);

            //Manabalken MainChar
            spriteBatch.Draw(Healthbar,
                new Rectangle(990, 247,
                    (int)(Healthbar.Width * ((double)mainChar.Stats.CurrentMP / mainChar.Stats.Manapoints)), Healthbar.Height),
                new Rectangle(0, 0, Healthbar.Width, Healthbar.Height), Color.Blue);

            spriteBatch.Draw(Healthbar2, new Vector2(988, 246),
                  null, Color.White);

            spriteBatch.DrawString(HealthText, mainChar.ManaText(), new Vector2(1350, 253), Color.AntiqueWhite);

            //Lebensbalken Enemy
            spriteBatch.Draw(Healthbar,
                new Rectangle(99, 202,
                    (int)(Healthbar.Width * ((double)enemy.Stats.CurrentLP / enemy.Stats.Lifepoints)), Healthbar.Height),
                new Rectangle(0, 0, Healthbar.Width, Healthbar.Height), Color.Red);

            spriteBatch.Draw(Healthbar2, new Vector2(99, 200),
                  null, Color.White);

            spriteBatch.DrawString(HealthText, enemy.HealthText(), new Vector2(460, 206), Color.AntiqueWhite);

            //Manabalken Enemy
            spriteBatch.Draw(Healthbar,
                new Rectangle(99, 247,
                    (int)(Healthbar.Width * ((double)enemy.Stats.CurrentMP / enemy.Stats.Manapoints)), Healthbar.Height),
                new Rectangle(0, 0, Healthbar.Width, Healthbar.Height), Color.Blue);

            spriteBatch.Draw(Healthbar2, new Vector2(99, 246),
                  null, Color.White);

            spriteBatch.DrawString(HealthText, enemy.ManaText(), new Vector2(460, 253), Color.AntiqueWhite);

            //Ausgabe
            spriteBatch.DrawString(mainChar.StatFont, mainChar.Ausgabe(),
                new Vector2(1000, 160), Color.White); ;

            spriteBatch.DrawString(enemy.StatFont, enemy.Ausgabe(),
                new Vector2(109, 160), Color.White);
        }

        public double Mod(double a, double b)
        {
            return a - b * Math.Floor(a / b);
        }
        public int RandomNumber(int a, int b)
        {
            Random zufall = new Random();
            return zufall.Next(a, b);
        }
    }
}