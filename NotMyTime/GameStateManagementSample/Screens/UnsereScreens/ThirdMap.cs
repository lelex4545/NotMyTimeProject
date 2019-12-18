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
    internal class ThirdMap : GameScreen
    {
        #region Fields
        private ContentManager content;
        private SpriteFont gameFont;
        private Random random = new Random();
        private float pauseAlpha;

        Map map;
        Player player;
        private Camera camera;
        Inventory inventory;
        public LootManager lootManager;
        Vector2[][] randomPositions;
        public Enemy[] enemyList;      //Spielgegner
        public Boss boss;
        public Portal portal;
        public Loot loot5;
        Gold gold1;

        GUI gui;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public ThirdMap()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            inventory = new Inventory();
            map = new Map();
            player = new Player(new Rectangle(11 * 100, 118 * 100, 100, 100));
            loot5 = new Loot(4, 1500, 6400, 50, 100, 1550, 6450, 0.35f, 0.60f, 925, 410, "weapon5");
            gold1 = new Gold(25, -1000, -1000, 50, 50, -1000, -1000, 1.0f);
            camera = new Camera();
            if (mainChar == null) mainChar = new MainFighter("Bruce", 200, 200, 1000, 20, 10, 20);
            mainChar.currentWorldID = 3;
            mainChar.Stats.Lifepoints = 600;
            mainChar.Stats.Manapoints = 600;
            mainChar.Stats.CurrentLP = mainChar.Stats.Lifepoints;
            mainChar.Stats.CurrentMP = mainChar.Stats.Manapoints;
            if (lootManager == null) lootManager = new LootManager();

            randomPositions = new Vector2[5][];

            for (int i = 0; i < 5; i++)
                randomPositions[i] = new Vector2[2];

            randomPositions[0][0] = new Vector2(11, 113);
            randomPositions[0][1] = new Vector2(20, 117);
            randomPositions[1][0] = new Vector2(10, 98);
            randomPositions[1][1] = new Vector2(21, 109);
            randomPositions[2][0] = new Vector2(10, 87);
            randomPositions[2][1] = new Vector2(16, 93);
            randomPositions[3][0] = new Vector2(13, 52);
            randomPositions[3][1] = new Vector2(18, 65);
            randomPositions[4][0] = new Vector2(17, 29);
            randomPositions[4][1] = new Vector2(21, 36);
            

            enemyList = new Enemy[20];
            enemyList[0] = new Enemy(randomPositions[0]);
            enemyList[1] = new Enemy(randomPositions[0]);
            enemyList[2] = new Enemy(randomPositions[1]);
            enemyList[3] = new Enemy(randomPositions[1]);
            enemyList[4] = new Enemy(randomPositions[1]);
            enemyList[5] = new Enemy(randomPositions[1]);
            enemyList[6] = new Enemy(randomPositions[2]);
            enemyList[7] = new Enemy(randomPositions[2]);
            enemyList[8] = new Enemy(randomPositions[3]);
            enemyList[9] = new Enemy(randomPositions[3]);
            enemyList[10] = new Enemy(randomPositions[3]);
            enemyList[11] = new Enemy(randomPositions[3]);
            enemyList[12] = new Enemy(randomPositions[4]);
            enemyList[13] = new Enemy(randomPositions[4]);
            

            boss = new Boss(new Rectangle(1500, 600, 200, 200), "deathbringer");
            portal = new Portal(new Rectangle(1500, 600, 100, 100), "portalblue");

            gui = new GUI();
        }

        public ThirdMap(LootManager lootManager) : this()
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

            loot5.LoadContent(content);
            gold1.LoadContent(content, "gold");
            lootManager.LoadContent(content);
            inventory.LoadContent(content, "Inventar");
            map.generateMap3();
            player.generatePlayer();

            gui.LoadContent(content);
            //raum1
            enemyList[0].generateEnemy("skeleton");
            enemyList[1].generateEnemy("skeleton");
            //raum2
            enemyList[2].generateEnemy("skeleton");
            enemyList[3].generateEnemy("skeleton");
            enemyList[4].generateEnemy("skeleton");
            enemyList[5].generateEnemy("skeleton");

            //raum3
            enemyList[6].generateEnemy("skeleton");
            enemyList[7].generateEnemy("evilknight");

            //raum4
            enemyList[8].generateEnemy("evilknight");
            enemyList[9].generateEnemy("evilknight");
            enemyList[10].generateEnemy("evilknight");
            enemyList[11].generateEnemy("evilknight");

            //raum5
            enemyList[12].generateEnemy("evilknight");
            enemyList[13].generateEnemy("evilknight");

            boss.generateBoss();
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
                    portal.updatePortal(gameTime, map, player, ScreenManager, ControllingPlayer, lootManager,2);

                loot5.Collison(player);
                gold1.Collison(player);
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
                                               Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(transformMatrix: camera.Transform);

            map.Draw(spriteBatch);
            player.Draw(spriteBatch);


            for (int i = 0; i < enemyList.Length; i++)
                if (enemyList[i] != null) enemyList[i].Draw(spriteBatch);

            if (boss != null) boss.Draw(spriteBatch);
            if (boss == null)
                portal.Draw(spriteBatch);

            inventory.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            loot5.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y, lootManager);
            lootManager.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            gui.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            gold1.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);

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