﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NotMyTime
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenHeight;
        public static int ScreenWidth;
        private Camera camera;

        Map map;                       //Map
        Player player;                 //Spielcharakter
        Enemy goblin;                  //Spielgegner
        Inventory inventory;          //inventar Objekt
        LootManager lootManager;      //packt items ins inventar
        Loot loot1;                   //Schwert
        Loot loot2;                   //keule
<<<<<<< HEAD
        bool battle;                  // Falls Kampf
=======
        Gold gold1;
        Gold gold2;
>>>>>>> fd606052ffeb24737d0be722ae810ea52e68c81d

        public static MainFighter mainChar;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;                  //Fullscreen
            graphics.PreferredBackBufferWidth = 1920;       //Auflösung
            graphics.PreferredBackBufferHeight = 1080;

            IsMouseVisible = true;

            mainChar = new MainFighter("Bruce", 100, 100, 50, 15, 15, 15);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Map und Spieler initialisieren + Content des "Players" verbinden
            map = new Map();
            Sprite.Content = Content;
            player = new Player(new Rectangle(11 * 100, 7 * 100, 100, 100));
            goblin = new Enemy(new Rectangle(13 * 100, 11 * 100, 100, 100));
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;
            inventory = new Inventory();

            //parameter: rectangle(x,y,größeX,größeY), X, Y, scale1, scale(inv), X(inv), Y(inv)
            loot1 = new Loot(0, 1250, 1050, 50, 100, 1300, 1100, 1.0f, 1.0f, 925, 420);
            loot2 = new Loot(1, 1050, 1350, 50, 100, 1100, 1400, 0.5f, 1.0f, 930, 425);
<<<<<<< HEAD
            battle = false;
=======

            gold1 = new Gold(250, 1050, 1200, 50, 50, 1100, 1250, 1.0f);
            gold2 = new Gold(250, 1250, 850, 50, 50, 1300, 900, 1.0f);
            //battlemode = new Battlemode();
            //Battlemode.Content = Content;
>>>>>>> fd606052ffeb24737d0be722ae810ea52e68c81d

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Tiles.Content = Content;
            
            Inventory.Content = Content;

            loot1.LoadContent(Content, GraphicsDevice, "weapon");
            loot2.LoadContent(Content, GraphicsDevice, "weapon2");

            gold1.LoadContent(Content, GraphicsDevice, "gold");
            gold2.LoadContent(Content, GraphicsDevice, "gold");

            inventory.LoadContent(Content, GraphicsDevice, "Inventar");

            map.generateMap1();
            
            player.generatePlayer();
            goblin.generateEnemy("goblin");
            camera = new Camera();






            BattleManager.Instance.LoadContent(Content);
            BattleManager.Instance.UnloadContent();



            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here


            BattleManager.Instance.UnloadContent();
            Tiles.UnLoadContent();

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
<<<<<<< HEAD
            if (battle == false)
            {
                player.updatePosition(gameTime, map);
                battle = goblin.moveOne(gameTime, map, player);
                camera.Follow(player);
                //inventory.openInventory(gameTime);
                //loot collision
                loot1.Collison(player);
                loot2.Collison(player);
            }
            if(battle == true)
                BattleManager.Instance.Update(gameTime);
            
            /*if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                map.LoadContent(Content);
                player.generatePlayer();
                goblin.generateEnemy("goblin");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                map.UnLoadContent();
                player.UnLoadContent();
                goblin.UnLoadContent();
            }*/
=======

            player.updatePosition(gameTime, map);
            goblin.moveOne(gameTime, map);
            camera.Follow(player);
            //inventory.openInventory(gameTime);
            //loot collision
            loot1.Collison(player);
            loot2.Collison(player);
            //gold collision
            gold1.Collison(player);
            gold2.Collison(player);
            //battlemode.updateMovement(gameTime);

            //BattleManager.Instance.Update(gameTime);
>>>>>>> fd606052ffeb24737d0be722ae810ea52e68c81d

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Zeichne Sprites
            spriteBatch.Begin(transformMatrix: camera.Transform);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            goblin.Draw(spriteBatch);
            inventory.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);

            loot1.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            loot2.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
<<<<<<< HEAD
            
            BattleManager.Instance.Draw(spriteBatch);
=======

            gold1.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            gold2.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            //battlemode.drawBattle(spriteBatch);
            //BattleManager.Instance.Draw(spriteBatch);
>>>>>>> fd606052ffeb24737d0be722ae810ea52e68c81d

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
