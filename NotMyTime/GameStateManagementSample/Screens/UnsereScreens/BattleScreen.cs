#region File Description

//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

#endregion Using Statements

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    internal class BattleScreen : GameScreen
    {
        #region Fields

        private ContentManager content;
        private SpriteFont gameFont;

        Texture2D fight_menu;   //Menüelmenete
        Texture2D menu_button;
        Texture2D magic_button;

        Vector2[] btnPos;
        int btnIndex = 0;
        Vector2 actualBtn;

        bool magicMenu = false;
        Vector2[] magicBtnPos;
        Vector2 actualMagicBtn;

        float elapsedTime;  //Zeitlogik für die Tasten
        float delay = 150f;

        SpriteFont font;
        string battlelog = "";

        Texture2D Healthbar;    //Lebensanzeige
        Texture2D Healthbar2;

        MainFighter mainChar;   //Charaktere
        EnemyFighter enemy;
        string enemyName;
        int weaponType = 0;

        Enemy[] enemyList;      //Liste der Gegner -> Toter Gegner wird aus dem Spiel entfernt
        int enemyIndex;

        /*     Animationsvariablen      */
        bool hasEntered = false;
        int enemyEnteringPositionX = 1000;
        int heroEnteringPositionX = 1000;

        bool attackAnimationIsPlaying = false;

        private float pauseAlpha;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public BattleScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(1);
        }

        public BattleScreen(Enemy enemy) : this()
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

            this.mainChar = FirstMap.mainChar;
            this.enemyName = enemy.GetName();
            LoadEnemy();
        }

        public BattleScreen(Enemy[] enemyList, int i) : this(enemyList[i])
        {
            this.enemyList = enemyList;
            this.enemyIndex = i;
        }
        
        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");


            fight_menu = content.Load<Texture2D>("Battle/fight_menu3");

            menu_button = content.Load<Texture2D>("Battle/menu_select");

            magic_button = content.Load<Texture2D>("Battle/menu_select2");

            Healthbar = content.Load<Texture2D>("Battle/health_white");

            Healthbar2 = content.Load<Texture2D>("Battle/healthbar_black");

            font = content.Load<SpriteFont>("Arial");

            //Song song = Content.Load<Song>("Battle_Theme_01");
            //MediaPlayer.Play(song);

            //TO DO Interface überarbeiten
            mainChar.StatFont = content.Load<SpriteFont>("Arial");

            enemy.StatFont = content.Load<SpriteFont>("Arial");


            //TO DO Model mit korrekter Waffe laden
            LoadMainCharTexture();

            //TO DO Richtige Auswahl des Gegners mit if schleifen
            LoadEnemyTexture();



            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            //Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }

        #endregion Initialization

        #region Update and Draw

        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                if (!hasEntered)
                {
                    if (enemyEnteringPositionX > 400) enemyEnteringPositionX -= 20;
                    if (heroEnteringPositionX > 260) heroEnteringPositionX -= 24;
                    if (enemyEnteringPositionX <= 400 && heroEnteringPositionX <= 250) hasEntered = true;
                }
                else if (attackAnimationIsPlaying == false)
                {
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
                                    attackAnimationIsPlaying = true;
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
                            if ((Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Right)) == true)
                            {
                                //TO DO es sollten nur Fähigkeiten Auswählbar sein, wofür der Char genügend Mana hat -> nicht mögliche Fähigkeiten ausgrauen
                                if (mainChar.Stats.CurrentMP >= 25)
                                {
                                    attackAnimationIsPlaying = true;
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
                        }
                        elapsedTime = 0;
                    }
                } else if (attackAnimationIsPlaying)
                {
                    ExitScreen();
                }
                
                if (!enemy.isAlive())
                {
                    enemyList[enemyIndex] = null;
                    ExitScreen();
                }
                if (!mainChar.isAlive())
                {
                    LoadingScreen.Load(ScreenManager, false, ControllingPlayer, new GameOverScreen());
                }

            }
        }

        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];


            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }

        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            DrawLayout(spriteBatch);

            if (mainChar.Stats.CurrentLP > 0)
                spriteBatch.Draw(mainChar.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 + heroEnteringPositionX, spriteBatch.GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);

            if (enemy.Stats.CurrentLP > 0)
                DrawEnemy(spriteBatch);

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        #endregion Update and Draw

        #region Other Methods
        void Fight(MainFighter main, EnemyFighter enemy, int i)
        {
            //TO DO es sollten nur Fähigkeiten Auswählbar sein, wofür der Char genügend Mana hat -> nicht mögliche Fähigkeiten ausgrauen + nicht klickbar machen
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
            enemy.Stats.CurrentLP = (int)MathHelper.Clamp(enemy.Stats.CurrentLP, 0, enemy.Stats.Lifepoints);
            enemy.Stats.CurrentMP = (int)MathHelper.Clamp(enemy.Stats.CurrentMP, 0, enemy.Stats.Manapoints);
            mainChar.Stats.CurrentLP = (int)MathHelper.Clamp(mainChar.Stats.CurrentLP, 0, mainChar.Stats.Lifepoints);
            mainChar.Stats.CurrentMP = (int)MathHelper.Clamp(mainChar.Stats.CurrentMP, 0, mainChar.Stats.Manapoints);
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
        #endregion Other Methods

        #region Loading Correct Asets
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
                    enemy.Model = content.Load<Texture2D>("goblin_right0");
                    break;
                case "dragon":
                    enemy.Model = content.Load<Texture2D>("Boss_Dragon_Huanglong");
                    break;
                case "runic":
                    enemy.Model = content.Load<Texture2D>("Boss_Runic_Stone");
                    break;
                case "queen":
                    enemy.Model = content.Load<Texture2D>("Boss_Dark_Queen");
                    break;
                default:
                    enemy.Model = content.Load<Texture2D>("Hohlenmensch_Right");
                    break;
            }
        }
        public void DrawEnemy(SpriteBatch spriteBatch)
        {
            switch (enemyName)
            {
                case "goblin":
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - enemyEnteringPositionX, spriteBatch.GraphicsDevice.Viewport.Height / 2 + 20), null, Color.White, 0f, new Vector2(), 1.5f, SpriteEffects.None, 0f);
                    break;
                case "dragon":
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - enemyEnteringPositionX, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 250), null, Color.White, 0f, new Vector2(), 0.75f, SpriteEffects.None, 0f);
                    break;
                case "runic":
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - enemyEnteringPositionX, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 250), null, Color.White, 0f, new Vector2(), 0.75f, SpriteEffects.None, 0f);
                    break;
                case "queen":
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 500, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 150), null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;
                default:
                    spriteBatch.Draw(enemy.Model, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - enemyEnteringPositionX, spriteBatch.GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(), 1.2f, SpriteEffects.None, 0f);
                    break;
            }
        }
        public void LoadMainCharTexture()
        {
            switch (weaponType)
            {
                default:
                    mainChar.Model = content.Load<Texture2D>("figure_left");
                    break;
            }
        }

        #endregion Loading Correct Assets
    }
}