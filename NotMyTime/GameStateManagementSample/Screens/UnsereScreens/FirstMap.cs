﻿#region Using Statements

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
    internal class FirstMap : GameScreen
    {
        #region Fields

        private ContentManager content;
        private SpriteFont gameFont;


        private Random random = new Random();

        public static MainFighter mainChar;
        Map map;                       //Map
        Player player;                 //Spielcharakter
        public Enemy[] enemyList;      //Spielgegner
        Inventory inventory;          //inventar Objekt
        LootManager lootManager;      //packt items ins inventar
        Loot loot1;                   //Schwert
        Loot loot2;                   //keule
        Gold gold1;
        Gold gold2;
        private Camera camera;

        CollisionChecker collisionChecker;

        private float pauseAlpha;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstMap()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            loot1 = new Loot(0, 4900, 4050, 50, 100, 4950, 4100, 1.0f, 1.0f, 925, 420);
            loot2 = new Loot(1, 1400, 850, 50, 100, 1450, 900, 0.5f, 1.0f, 930, 425);

            gold1 = new Gold(100, 1250, 850, 50, 50, 1300, 900, 1.0f);
            gold2 = new Gold(150, 1150, 1000, 50, 50, 1200, 1100, 1.0f);

            inventory = new Inventory();
            map = new Map();
            enemyList = new Enemy[20];
            enemyList[0] = new Enemy(new Rectangle(13 * 100, 11 * 100, 100, 100));
            player = new Player(new Rectangle(11 * 100, 7 * 100, 100, 100));
            camera = new Camera();
            mainChar = new MainFighter("Bruce", 10, 100, 15, 10, 10, 10);
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");

            Sprite.Content = content;

            Tiles.Content = content;

            loot1.LoadContent(content, "weapon");
            loot2.LoadContent(content, "weapon2");

            gold1.LoadContent(content, "gold");
            gold2.LoadContent(content, "gold");

            inventory.LoadContent(content, "Inventar");

            map.generateMap1();

            player.generatePlayer();

            if (enemyList[0] != null) enemyList[0].generateEnemy("goblin");

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

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
                player.updatePosition(gameTime, map);

                for (int i = 0; enemyList[i] != null; i++)
                    collisionChecker = enemyList[i].moveOne(gameTime, map, player);    //Prüft, ob collision stattgefunden hat

                if (collisionChecker.Collision == true)  //Wenn Collision eintritt, dann starte den Kampfmenu und checke ob der Gegner tot ist
                {
                    BattleScreen battlescreen = new BattleScreen(collisionChecker);
                    //GameScreen[] screensToLoad = { this, battlescreen };                          //Langsamer Kampfbeginn
                    //LoadingScreen.Load(ScreenManager, false, ControllingPlayer, screensToLoad);   //Langsamer Kampfbeginn
                    ScreenManager.AddScreen(battlescreen, ControllingPlayer); //Kampfmenu
                    for (int i = 0; enemyList[i] != null; i++)  //Entferne Enemy aus dem Spiel, wenn es stirbt
                        if (enemyList[i] == collisionChecker.Enemy)
                            enemyList[i] = null;
                }

                camera.Follow(player);
                //inventory.openInventory(gameTime);
                //loot collision
                loot1.Collison(player);
                loot2.Collison(player);
                //gold collision
                gold1.Collison(player);
                gold2.Collison(player);
                //font nullen
                gold1.updateFont(gameTime);
                gold2.updateFont(gameTime);



                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)
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


            spriteBatch.Begin(transformMatrix: camera.Transform);

            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            for (int i = 0; enemyList[i] != null; i++)
                enemyList[i].Draw(spriteBatch);
            inventory.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);

            loot1.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            loot2.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);

            gold1.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            gold2.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        #endregion Update and Draw
    }
}