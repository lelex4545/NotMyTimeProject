using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NotMyTime.Screens
{
    public class BattleScreen : GameScreen
    {
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

        SpriteFont font;
        string battlelog = "";

        Texture2D Healthbar;
        Texture2D Healthbar2;

        MainFighter mainChar;
        EnemyFighter enemy;
        string enemyName;
        int weaponType = 0;

        public BattleScreen() : this("") { }

        public BattleScreen(string enemyName)
        {
            btnPos = new Vector2[2];
            btnPos[0] = new Vector2(0, 0);
            btnPos[1] = new Vector2(0, 86);
            actualBtn = btnPos[0];

            magicBtnPos = new Vector2[4];
            magicBtnPos[0] = new Vector2(990, 770);
            magicBtnPos[1] = new Vector2(990, 806);
            magicBtnPos[2] = new Vector2(990, 841);
            magicBtnPos[3] = new Vector2(990, 876);

            this.mainChar = ScreenManager.mainChar;
            this.enemyName = enemyName;
            LoadEnemy();
        }
        public override void LoadContent()
        {
            base.LoadContent();

            fight_menu = Content.Load<Texture2D>("Battle/fight_menu3");

            menu_button = Content.Load<Texture2D>("Battle/menu_select");

            magic_button = Content.Load<Texture2D>("Battle/menu_select2");

            Healthbar = Content.Load<Texture2D>("Battle/health_white");

            Healthbar2 = Content.Load<Texture2D>("Battle/healthbar_black");

            font = Content.Load<SpriteFont>("Arial");

            //TO DO Interface überarbeiten
            mainChar.StatFont = Content.Load<SpriteFont>("Arial");

            enemy.StatFont = Content.Load<SpriteFont>("Arial");


            //TO DO Model mit korrekter Waffe laden
            LoadMainCharTexture();

            //TO DO Richtige Auswahl des Gegners mit if schleifen
            LoadEnemyTexture();

        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= delay)
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
                            //TO DO es sollten nur Fähigkeiten Auswählbar sein, wofür der Char genügend Mana hat -> nicht mögliche Fähigkeiten ausgrauen
                            if (mainChar.Stats.CurrentMP >= 25)
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            base.Draw(spriteBatch);
            DrawLayout(spriteBatch);

            if (mainChar.Stats.CurrentLP > 0)
                spriteBatch.Draw(mainChar.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 + 250, spriteBatch.GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(), 1.2f, SpriteEffects.None, 0f);

            if (enemy.Stats.CurrentLP > 0)
                DrawEnemy(spriteBatch);
            spriteBatch.End();
        }

        void Fight(MainFighter main, EnemyFighter enemy, int i)
        {
            //TO DO es sollten nur Fähigkeiten Auswählbar sein, wofür der Char genügend Mana hat -> nicht mögliche Fähigkeiten ausgrauen
            int result = main.compareSpeed(enemy);
            int a = 0, b = 0;
            if (result == 1)
            {
                a = main.Attack(enemy, i);
                if (enemy.isAlive())
                    b = enemy.RollAttack(main);
                battlelog = "Hero Attack: " + a + "\n" + "Enemy Attack: " + b + "\n";
            }
            else if (result == -1)
            {
                b = enemy.RollAttack(main);
                if (main.isAlive())
                    a = main.Attack(enemy, i);
                battlelog = "Enemy Attack: " + b + "\n" + "Hero Attack: " + a + "\n";
            }
            else
            {
                if (RandomNumber(0, 2) == 0.0)
                {
                    a = main.Attack(enemy, i);
                    if (enemy.isAlive())
                        b = enemy.RollAttack(main);
                    battlelog = "Hero Attack: " + a + "\n" + "Enemy Attack: " + b + "\n";
                }
                else
                {
                    b = enemy.RollAttack(main);
                    if (main.isAlive())
                        a = main.Attack(enemy, i);
                    battlelog = "Enemy Attack: " + b + "\n" + "Hero Attack: " + a + "\n";
                }
            }
            if (!enemy.isAlive()) main.Level.SetExp(enemy);
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
                if (mainChar.Stats.CurrentMP >= 25)
                {
                    spriteBatch.DrawString(font, "Heal               25MP", new Vector2(1000, 780), Color.AntiqueWhite);
                    spriteBatch.DrawString(font, "Fire                 25MP", new Vector2(1000, 816), Color.AntiqueWhite);
                    spriteBatch.DrawString(font, "Ice                    25MP", new Vector2(1000, 851), Color.AntiqueWhite);
                    spriteBatch.DrawString(font, "Thunder    25MP", new Vector2(1000, 886), Color.AntiqueWhite);
                }
                else
                {
                    spriteBatch.DrawString(font, "Heal               25MP", new Vector2(1000, 780), Color.Gray);
                    spriteBatch.DrawString(font, "Fire                 25MP", new Vector2(1000, 816), Color.Gray);
                    spriteBatch.DrawString(font, "Ice                    25MP", new Vector2(1000, 851), Color.Gray);
                    spriteBatch.DrawString(font, "Thunder    25MP", new Vector2(1000, 886), Color.Gray);
                }
            }

            spriteBatch.DrawString(font, battlelog, new Vector2(0, 0), Color.White);

            //Lebensbalken MainChar
            spriteBatch.Draw(Healthbar,
                new Rectangle(990, 202,
                    (int)(Healthbar.Width * ((double)mainChar.Stats.CurrentLP / mainChar.Stats.Lifepoints)), Healthbar.Height),
                new Rectangle(0, 0, Healthbar.Width, Healthbar.Height), Color.Red);

            spriteBatch.Draw(Healthbar2, new Vector2(989, 200),
                  null, Color.White);

            //TO DO Lebenstexte zentriert darstellen
            spriteBatch.DrawString(font, mainChar.HealthText(), new Vector2(1350, 206), Color.AntiqueWhite);
            //Vector2 origin1 = font.MeasureString(mainChar.HealthText()) / 2f;
            //spriteBatch.DrawString(font, mainChar.HealthText(), new Vector2(1400, 220), Color.AntiqueWhite, 0f, origin1, 0f, SpriteEffects.None, 0f);

            //Manabalken MainChar
            spriteBatch.Draw(Healthbar,
                new Rectangle(990, 247,
                    (int)(Healthbar.Width * ((double)mainChar.Stats.CurrentMP / mainChar.Stats.Manapoints)), Healthbar.Height),
                new Rectangle(0, 0, Healthbar.Width, Healthbar.Height), Color.Blue);

            spriteBatch.Draw(Healthbar2, new Vector2(988, 246),
                  null, Color.White);

            spriteBatch.DrawString(font, mainChar.ManaText(), new Vector2(1350, 253), Color.AntiqueWhite);

            //Lebensbalken Enemy
            spriteBatch.Draw(Healthbar,
                new Rectangle(99, 202,
                    (int)(Healthbar.Width * ((double)enemy.Stats.CurrentLP / enemy.Stats.Lifepoints)), Healthbar.Height),
                new Rectangle(0, 0, Healthbar.Width, Healthbar.Height), Color.Red);

            spriteBatch.Draw(Healthbar2, new Vector2(99, 200),
                  null, Color.White);

            spriteBatch.DrawString(font, enemy.HealthText(), new Vector2(460, 206), Color.AntiqueWhite);

            //Manabalken Enemy
            spriteBatch.Draw(Healthbar,
                new Rectangle(99, 247,
                    (int)(Healthbar.Width * ((double)enemy.Stats.CurrentMP / enemy.Stats.Manapoints)), Healthbar.Height),
                new Rectangle(0, 0, Healthbar.Width, Healthbar.Height), Color.Blue);

            spriteBatch.Draw(Healthbar2, new Vector2(99, 246),
                  null, Color.White);

            spriteBatch.DrawString(font, enemy.ManaText(), new Vector2(460, 253), Color.AntiqueWhite);

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

        public void LoadEnemy()
        {
            switch (enemyName)
            {
                case "goblin":
                    enemy = new EnemyFighter("Goblin", 1, 50, 50, 5, 5, 5, 5, 25);
                    break;
                case "1":
                    enemy = new EnemyFighter("Draggy", 4, 250, 100, 15, 10, 10, 10, 115);
                    break;
                case "2":
                    enemy = new EnemyFighter("StoneBoy", 7, 700, 500, 50, 80, 10, 10, 350);
                    break;
                case "3":
                    enemy = new EnemyFighter("Dark Queen", 10, 1000, 1000, 100, 50, 50, 50, 1000);
                    break;
                default:
                    enemy = new EnemyFighter("Stony", 1, 100, 100, 10, 10, 10, 10, 50);
                    break;
            }
        }
        public void LoadEnemyTexture()
        {
            switch (enemyName)
            {
                case "goblin":
                    enemy.Model = Content.Load<Texture2D>("goblin_right0");
                    break;
                case "1":
                    enemy.Model = Content.Load<Texture2D>("Boss_Dragon_Huanglong");
                    break;
                case "2":
                    enemy.Model = Content.Load<Texture2D>("Boss_Runic_Stone");
                    break;
                case "3":
                    enemy.Model = Content.Load<Texture2D>("Boss_Dark_Queen");
                    break;
                default:
                    enemy.Model = Content.Load<Texture2D>("Hohlenmensch_Right");
                    break;
            }
        }
        public void DrawEnemy(SpriteBatch spriteBatch)
        {
            switch (enemyName)
            {
                case "goblin":
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 400, spriteBatch.GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(), 1.2f, SpriteEffects.None, 0f);
                    break;
                case "1":
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 400, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 250), null, Color.White, 0f, new Vector2(), 0.75f, SpriteEffects.None, 0f);
                    break;
                case "2":
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 400, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 250), null, Color.White, 0f, new Vector2(), 0.75f, SpriteEffects.None, 0f);
                    break;
                case "3":
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 500, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 150), null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;
                default:
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 400, spriteBatch.GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(), 1.2f, SpriteEffects.None, 0f);
                    break;
            }
        }
        public void LoadMainCharTexture()
        {
            switch (weaponType)
            {
                default:
                    mainChar.Model = Content.Load<Texture2D>("figure_left");
                    break;
            }
        }

        public bool IsEnemyAlive()
        {
            return enemy.isAlive();
        }

    }

}
