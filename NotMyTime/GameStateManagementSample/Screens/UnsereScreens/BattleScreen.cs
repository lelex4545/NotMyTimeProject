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
        string debugger = "";

        Texture2D Healthbar;    //Lebensanzeige
        Texture2D Healthbar2;

        EnemyFighter enemy;
        string enemyName;
        int weaponType = 0;

        Enemy[] enemyList;      //Liste der Gegner -> Toter Gegner wird aus dem Spiel entfernt
        int enemyIndex;
        bool isBoss = false;
        Boss boss;

        /*     Animationsvariablen      */
        Vector2 MainStandinPosition;
        Vector2 MainActualPosition;
        Vector2 EnemyStandinPosition;
        Vector2 EnemyActualPosition;
        bool hasEntered = false;
        bool attackAnimationIsPlaying = false;
        bool firstAttackAnimation = true;
        bool attackHit = false;
        int choosenAttack;
        int randomAttackPercentage = 0;
        int randomFight;// = 0;

        /*   RANDOM NUMBER GENERATOR       */
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

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

        public BattleScreen(string enemy) : this()
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

            this.enemyName = enemy;

            // TODO: MainChar wird mitgegeben + WeaponType wird mitgegeben
            this.weaponType = mainChar.currentWeaponID;

            MainStandinPosition = new Vector2(1216, 540);
            MainActualPosition = new Vector2(1960, 540);

            LoadEnemy();
        }

        public BattleScreen(Enemy[] enemyList, int i) : this(enemyList[i].GetName())
        {
            this.enemyList = enemyList;
            this.enemyIndex = i;
        }
        public BattleScreen(Boss boss) : this(boss.name)
        {
            isBoss = true;
            this.boss = boss;
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
                if (!hasEntered)        //Die Kämpfer werden von außen nach innen eingeblendet
                {
                    if (EnemyActualPosition.X != EnemyStandinPosition.X && MainActualPosition.X != MainStandinPosition.X)
                    {
                        MainActualPosition.X -= 24;
                        EnemyActualPosition.X += 20;
                    }
                    else hasEntered = true;
                }
                else if (attackAnimationIsPlaying == false)         //Menü einblenden + Keine Kampfanimation
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
                                    randomAttackPercentage = RandomNumber(1, 101);
                                    if (mainChar.compareSpeed(enemy) == 0) randomFight = RandomNumber(0, 2);
                                    choosenAttack = btnIndex;
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
                                    randomAttackPercentage = RandomNumber(1, 101);
                                    if (mainChar.compareSpeed(enemy) == 0) randomFight = RandomNumber(0, 2);
                                    choosenAttack = btnIndex + 1;
                                    magicMenu = false;
                                    btnIndex = 0;
                                    actualBtn = btnPos[btnIndex];
                                }
                            }
                        }
                        if(randomFight!=null)debugger = randomFight + "";
                        elapsedTime = 0;
                    }
                }
                else if (attackAnimationIsPlaying)
                {
                    Fight(mainChar, enemy, choosenAttack);
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
                //spriteBatch.Draw(mainChar.Model, MainActualPosition, null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                DrawMainChar(spriteBatch);

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
            if (result == 1)
            {
                HeroAttacksFirst(main, enemy, i);
            }
            else if (result == -1)
            {
                EnemyAttacksFirst(main, enemy, i);
            }
            else
            {
                if (randomFight == 0)
                {
                    HeroAttacksFirst(main, enemy, i);
                }
                else
                {
                    EnemyAttacksFirst(main, enemy, i);
                }
            }
            enemy.Stats.CurrentLP = (int)MathHelper.Clamp(enemy.Stats.CurrentLP, 0, enemy.Stats.Lifepoints);
            enemy.Stats.CurrentMP = (int)MathHelper.Clamp(enemy.Stats.CurrentMP, 0, enemy.Stats.Manapoints);
            mainChar.Stats.CurrentLP = (int)MathHelper.Clamp(mainChar.Stats.CurrentLP, 0, mainChar.Stats.Lifepoints);
            mainChar.Stats.CurrentMP = (int)MathHelper.Clamp(mainChar.Stats.CurrentMP, 0, mainChar.Stats.Manapoints);
        }

        void HeroAttacksFirst(MainFighter main, EnemyFighter enemy, int i)
        {
            if (firstAttackAnimation)
            {
                if (i == 0) HeroNormalAttack(main, enemy, i, true);
                else HeroMagicAttack(main, enemy, i, true);
            }
            else
            {
                if (enemy.isAlive())
                {
                    if (randomAttackPercentage < 80)
                        EnemyNormalAttack(main, enemy, false);
                    else
                        EnemyMagicAttack(main, enemy, false);
                }
                else
                {
                    main.Level.SetExp(enemy);
                    if (isBoss) boss.IsAlive = false;
                    else enemyList[enemyIndex] = null;
                    ExitScreen();
                }
            }
        }
        void EnemyAttacksFirst(MainFighter main, EnemyFighter enemy, int i)
        {
            if (firstAttackAnimation)
            {
                if (randomAttackPercentage < 80)
                    EnemyNormalAttack(main, enemy, true);
                else
                    EnemyMagicAttack(main, enemy, true);
            }
            else
            {
                if (i == 0) HeroNormalAttack(main, enemy, i, false);
                else HeroMagicAttack(main, enemy, i, false);
            }
            if (!enemy.isAlive() && !attackAnimationIsPlaying)
            {
                main.Level.SetExp(enemy);
                if (isBoss) boss.IsAlive = false;
                else enemyList[enemyIndex] = null;
                ExitScreen();
            }
        }

        void HeroNormalAttack(MainFighter main, EnemyFighter enemy, int i, bool first)
        {
            if (!attackHit)
            {
                if (MainActualPosition.X >= EnemyStandinPosition.X)
                {
                    MainActualPosition.X -= 25;
                }
                else
                {
                    main.Attack(enemy, i);
                    attackHit = true;
                }
            }
            else
            {
                if (MainActualPosition.X <= MainStandinPosition.X)
                {
                    MainActualPosition.X += 25;
                }
                else
                {
                    if (first)
                    {
                        firstAttackAnimation = false;
                        attackHit = false;
                    }
                    else
                    {
                        attackAnimationIsPlaying = false;
                        firstAttackAnimation = true;
                        attackHit = false;
                    }
                }
            }
        }

        void HeroMagicAttack(MainFighter main, EnemyFighter enemy, int i, bool first)
        {
            if (!attackHit)
            {
                if (MainActualPosition.X >= 960)
                {
                    MainActualPosition.X -= 8;
                }
                else
                {
                    main.Attack(enemy, i);
                    attackHit = true;
                }
            }
            else
            {
                if (MainActualPosition.X <= MainStandinPosition.X)
                {
                    MainActualPosition.X += 8;
                }
                else
                {
                    if (first)
                    {
                        firstAttackAnimation = false;
                        attackHit = false;
                    }
                    else
                    {
                        attackAnimationIsPlaying = false;
                        firstAttackAnimation = true;
                        attackHit = false;
                    }
                }
            }
        }

        void EnemyNormalAttack(MainFighter main, EnemyFighter enemy, bool first)
        {
            if (!attackHit)
            {
                if (EnemyActualPosition.X <= MainStandinPosition.X)
                {
                    EnemyActualPosition.X += 25;
                }
                else
                {
                    enemy.Attack(main);
                    attackHit = true;
                    
                }
            }
            else
            {
                if (EnemyActualPosition.X >= EnemyStandinPosition.X)
                {
                    EnemyActualPosition.X -= 25;
                }
                else
                {
                    if (first)
                    {
                        firstAttackAnimation = false;
                        attackHit = false;
                    }
                    else
                    {
                        attackAnimationIsPlaying = false;
                        firstAttackAnimation = true;
                        attackHit = false;
                    }
                }
            }
        }

        void EnemyMagicAttack(MainFighter main, EnemyFighter enemy, bool first)
        {
            if (!attackHit)
            {
                if (EnemyActualPosition.X <= EnemyStandinPosition.X + 200)
                {
                    EnemyActualPosition.X += 8;
                }
                else
                {
                    enemy.Magic(main, RandomNumber(2, 4));
                    attackHit = true;
                }
            }
            else
            {
                if (EnemyActualPosition.X >= EnemyStandinPosition.X)
                {
                    EnemyActualPosition.X -= 8;
                }
                else
                {
                    if (first)
                    {
                        firstAttackAnimation = false;
                        attackHit = false;
                    }
                    else
                    {
                        attackAnimationIsPlaying = false;
                        firstAttackAnimation = true;
                        attackHit = false;
                    }
                }
            }
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

            spriteBatch.DrawString(font, debugger, new Vector2(0, 0), Color.White);

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
        #endregion Other Methods

        #region Loading Correct Asets
        public void LoadEnemy()
        {
            switch (enemyName)
            {
                case "goblin":
                    enemy = new EnemyFighter("Goblin", 1, 50, 50, 5, 5, 15, 5, 25);
                    EnemyStandinPosition = new Vector2(560, 520);
                    EnemyActualPosition = new Vector2(-40, 520);
                    break;
                case "ripper":
                    enemy = new EnemyFighter("Ripper", 1, 50, 50, 5, 5, 15, 5, 25);
                    EnemyStandinPosition = new Vector2(560, 520);
                    EnemyActualPosition = new Vector2(-40, 520);
                    break;
                case "knight":
                    enemy = new EnemyFighter("Knight", 1, 50, 50, 5, 5, 15, 5, 25);
                    EnemyStandinPosition = new Vector2(560, 560);
                    EnemyActualPosition = new Vector2(-40, 560);
                    break;
                case "knightmaster":
                    enemy = new EnemyFighter("Knightmaster", 1, 50, 50, 5, 5, 15, 5, 25);
                    EnemyStandinPosition = new Vector2(560, 560);
                    EnemyActualPosition = new Vector2(-40, 560);
                    break;
                case "demon1":
                    enemy = new EnemyFighter("Demon", 1, 50, 50, 5, 5, 15, 5, 25);
                    EnemyStandinPosition = new Vector2(560, 560);
                    EnemyActualPosition = new Vector2(-40, 560);
                    break;
                case "demon":
                    enemy = new EnemyFighter("Demon", 1, 50, 50, 5, 5, 15, 5, 25);
                    EnemyStandinPosition = new Vector2(560, 560);
                    EnemyActualPosition = new Vector2(-40, 560);
                    break;
                case "ddragon":
                    enemy = new EnemyFighter("Draggy", 4, 100, 100, 1, 1, 10, 10, 115);
                    EnemyStandinPosition = new Vector2(560, 290);
                    EnemyActualPosition = new Vector2(-40, 290);
                    break;
                case "runic":
                    enemy = new EnemyFighter("StoneBoy", 7, 700, 500, 50, 80, 10, 10, 350);
                    EnemyStandinPosition = new Vector2(560, 290);
                    EnemyActualPosition = new Vector2(-40, 290);
                    break;
                case "queen":
                    enemy = new EnemyFighter("Dark Queen", 10, 1000, 1000, 100, 50, 50, 50, 1000);
                    EnemyStandinPosition = new Vector2(460, 390);
                    EnemyActualPosition = new Vector2(-140, 390);
                    break;
                default:
                    enemy = new EnemyFighter("Stony", 1, 100, 100, 10, 10, 10, 10, 50);
                    EnemyStandinPosition = new Vector2(560, 560);
                    EnemyActualPosition = new Vector2(-40, 560);
                    break;
            }
        }
        public void LoadEnemyTexture()
        {
            switch (enemyName)
            {
                case "goblin":
                    enemy.Model = content.Load<Texture2D>("Battle/Enemy/goblin");
                    break;
                case "ripper":
                    enemy.Model = content.Load<Texture2D>("Battle/Enemy/ripper");
                    break;
                case "knight":
                    enemy.Model = content.Load<Texture2D>("Battle/Enemy/knight");
                    break;
                case "knightmaster":
                    enemy.Model = content.Load<Texture2D>("Battle/Enemy/knightmaster");
                    break;
                case "demon1":
                    enemy.Model = content.Load<Texture2D>("Battle/Enemy/demon1");
                    break;
                case "demon":
                    enemy.Model = content.Load<Texture2D>("Battle/Enemy/demon2");
                    break;
                case "ddragon":
                    enemy.Model = content.Load<Texture2D>("Battle/Enemy/Boss_Dragon_Huanglong");
                    break;
                case "runic":
                    enemy.Model = content.Load<Texture2D>("Battle/Enemy/Boss_Runic_Stone");
                    break;
                case "queen":
                    enemy.Model = content.Load<Texture2D>("Battle/Enemy/Boss_Dark_Queen");
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
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;
                case "ripper":
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;
                case "knight":
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;
                case "knightmaster":
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;
                case "demon1":
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;
                case "demon":
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;
                case "ddragon":
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 0.75f, SpriteEffects.None, 0f);
                    break;
                case "runic":
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 0.75f, SpriteEffects.None, 0f);
                    break;
                case "queen":
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;
                default:
                    spriteBatch.Draw(enemy.Model, EnemyActualPosition, null, Color.White, 0f, new Vector2(), 1.2f, SpriteEffects.None, 0f);
                    break;
            }
        }
        public void LoadMainCharTexture()
        {
            switch (weaponType)
            {
                case 0: //Speer
                    mainChar.Model = content.Load<Texture2D>("Battle/figureweapon1");   
                    MainStandinPosition.Y -= 50;
                    MainActualPosition.Y -= 50;
                    MainStandinPosition.X -= 50;
                    MainActualPosition.X -= 50;
                    break;
                case 1: //Keule
                    mainChar.Model = content.Load<Texture2D>("Battle/figureweapon0");   
                    break;
                default: //Hand
                    mainChar.Model = content.Load<Texture2D>("figure_left");            
                    break;
            }
        }
        public void DrawMainChar(SpriteBatch spriteBatch)
        {
            debugger = weaponType + "";
            switch (weaponType)
            {
                case 0: //Speer
                    spriteBatch.Draw(mainChar.Model, MainActualPosition, null, Color.White, 0f, new Vector2(), 0.15f, SpriteEffects.None, 0f);
                    break;
                case 1: //Keule
                    spriteBatch.Draw(mainChar.Model, MainActualPosition, null, Color.White, 0f, new Vector2(), 0.16f, SpriteEffects.None, 0f);
                    break;
                default: //Hand
                    spriteBatch.Draw(mainChar.Model, MainActualPosition, null, Color.White, 0f, new Vector2(), 1.2f, SpriteEffects.None, 0f);
                    break;
            }
        }

        #endregion Loading Correct Assets
    }
}