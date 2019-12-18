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
    internal class SecondMap : GameScreen
    {
        #region Fields
        private ContentManager content;
        private SpriteFont gameFont;
        private Random random = new Random();
        private float pauseAlpha;
        public LootManager lootManager;


        Map map;                       //Map
        Vector2[][] randomPositions;
        Player player;                 //Spielcharakter
        public Enemy[] enemyList;      //Spielgegner
        public Boss boss;
        public Boss boss1;
        public Portal portal;
        Inventory inventory;

        Loot loot3;
        Loot loot4;

        private Camera camera;
        GUI gui;



        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecondMap()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            inventory = new Inventory();
            map = new Map();
            
            randomPositions = new Vector2[6][];

            for (int i = 0; i < 6; i++)
                randomPositions[i] = new Vector2[2];

            randomPositions[0][0] = new Vector2(14, 1);
            randomPositions[0][1] = new Vector2(24, 12);
            randomPositions[1][0] = new Vector2(2, 24);
            randomPositions[1][1] = new Vector2(9, 34);
            randomPositions[2][0] = new Vector2(11, 32);
            randomPositions[2][1] = new Vector2(28, 34);
            randomPositions[3][0] = new Vector2(36, 23);
            randomPositions[3][1] = new Vector2(43, 34);
            randomPositions[4][0] = new Vector2(26, 1);
            randomPositions[4][1] = new Vector2(33, 12);
            randomPositions[5][0] = new Vector2(47, 29);
            randomPositions[5][1] = new Vector2(58, 34);
            enemyList = new Enemy[20];

            enemyList[0] = new Enemy(randomPositions[0]);
            enemyList[1] = new Enemy(randomPositions[0]);
            enemyList[2] = new Enemy(randomPositions[0]);
            enemyList[3] = new Enemy(randomPositions[0]);
            enemyList[4] = new Enemy(randomPositions[0]);
            enemyList[5] = new Enemy(randomPositions[1]);
            enemyList[6] = new Enemy(randomPositions[1]);
            enemyList[7] = new Enemy(randomPositions[1]);
            enemyList[8] = new Enemy(randomPositions[2]);
            enemyList[9] = new Enemy(randomPositions[2]);
            enemyList[10] = new Enemy(randomPositions[3]);
            enemyList[11] = new Enemy(randomPositions[3]);
            enemyList[12] = new Enemy(randomPositions[3]);
            enemyList[13] = new Enemy(randomPositions[4]);
            enemyList[14] = new Enemy(randomPositions[4]);
            enemyList[15] = new Enemy(randomPositions[4]);
            enemyList[16] = new Enemy(randomPositions[5]);
            enemyList[17] = new Enemy(randomPositions[5]);
            enemyList[18] = new Enemy(randomPositions[5]);

            boss = new Boss(new Rectangle(5200, 100, 200, 200), "runicgolem");
            boss1 = new Boss(new Rectangle(5300, 1400, 100, 100), "knightchief");
            portal = new Portal(new Rectangle(5200, 100, 100, 100), "portalred");

            loot3 = new Loot(0, 4900, 4050, 50, 100, 4950, 4100, 1.0f, 1.0f, 925, 410, "weapon3");
            loot4 = new Loot(0, 4900, 4050, 50, 100, 4950, 4100, 1.0f, 1.0f, 925, 410, "weapon4");

            player = new Player(new Rectangle(2 * 100, 1 * 100, 100, 100));
            camera = new Camera();
            if (mainChar == null) mainChar = new MainFighter("Bruce", 200, 200, 25, 20, 10, 20);
            mainChar.currentWorldID = 2;
            mainChar.Stats.CurrentLP = mainChar.Stats.Lifepoints;
            mainChar.Stats.CurrentMP = mainChar.Stats.Manapoints;
            if (lootManager == null) lootManager = new LootManager();
            gui = new GUI();
        }
        public SecondMap(LootManager lootManager) : this()
        {
            this.lootManager = lootManager;
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
            lootManager.LoadContent(content);
            inventory.LoadContent(content, "Inventar");
            map.generateMap2();
            gui.LoadContent(content);
            player.generatePlayer();
            //raum1
            enemyList[0].generateEnemy("knight");
            enemyList[1].generateEnemy("knight");
            enemyList[2].generateEnemy("knight");
            enemyList[3].generateEnemy("knight");
            enemyList[4].generateEnemy("knight");
            //raum2
            enemyList[5].generateEnemy("knight");
            enemyList[6].generateEnemy("knight");
            enemyList[7].generateEnemy("knight");
            //raum3
            enemyList[8].generateEnemy("knight");
            enemyList[9].generateEnemy("knight");

            enemyList[10].generateEnemy("knight");
            enemyList[11].generateEnemy("knight");
            enemyList[12].generateEnemy("knightmaster");
            //raum5
            enemyList[13].generateEnemy("knight");
            enemyList[14].generateEnemy("knight");
            enemyList[15].generateEnemy("knightmaster");
            //raum6 vor bossraum
            enemyList[16].generateEnemy("knightmaster");
            enemyList[17].generateEnemy("knightmaster");
            enemyList[18].generateEnemy("knightmaster");

            boss.generateBoss();
            boss1.generateSpecial();
            portal.generatePortal();

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
                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)
                player.updatePosition(gameTime, map);
                camera.Follow(player);
                for (int i = 0; i < enemyList.Length; i++)
                    if (enemyList[i] != null) enemyList[i].moveOne(gameTime, map, player, ScreenManager, ControllingPlayer, enemyList, i);

                if (boss != null && !boss.IsAlive) boss = null;
                if (boss != null) boss.updateBoss(gameTime, map, player, ScreenManager, ControllingPlayer, boss);
                if (boss == null)
                    portal.updatePortal(gameTime, map, player, ScreenManager, ControllingPlayer, lootManager);

                if (boss1 != null && !boss1.IsAlive) boss1 = null;
                if (boss1 != null) boss1.updateSpecial(gameTime, map, player, ScreenManager, ControllingPlayer, boss1);

                lootManager.Update(gameTime);

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
            else
            {


            }
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            

            spriteBatch.Begin(transformMatrix: camera.Transform);

            map.Draw(spriteBatch);
            player.Draw(spriteBatch);

            for (int i = 0; i < enemyList.Length; i++)
                if (enemyList[i] != null) enemyList[i].Draw(spriteBatch);


            if (boss != null) boss.Draw(spriteBatch);
            if (boss1 != null) boss1.Draw(spriteBatch);

            if (boss == null)
                portal.Draw(spriteBatch);

            inventory.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            lootManager.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            gui.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
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