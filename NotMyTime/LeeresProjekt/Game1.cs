using Microsoft.Xna.Framework;
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

        Battlemode battlemode;       //Battlemode

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;                  //Fullscreen
            graphics.PreferredBackBufferWidth = 1920;       //Auflösung
            graphics.PreferredBackBufferHeight = 1080;

            IsMouseVisible = true;

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

            //battlemode = new Battlemode();
            //Battlemode.Content = Content;

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

            inventory.LoadContent(Content, GraphicsDevice, "Inventar");

            map.generateMap1();
            
            player.generatePlayer();
            goblin.generateEnemy("goblin");
            camera = new Camera();

            //battlemode.generateBattle();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            player.updatePosition(gameTime, map);
            goblin.moveOne(gameTime, map);
            camera.Follow(player);
            //inventory.openInventory(gameTime);
            //loot collision
            loot1.Collison(player);
            loot2.Collison(player);

            //battlemode.updateMovement(gameTime);

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

            //battlemode.drawBattle(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
