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
        Inventory inventory;          //inventar Objekt
        Loot loot1;                    //Schwert 1

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
            player = new Player(new Rectangle(6 * 100, 4 * 100, 100, 100));
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;
            inventory = new Inventory();
            loot1 = new Loot(new Rectangle(150, 300, 100, 100));

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
            //Loot.Content = Content;
            loot1.LoadContent(Content, GraphicsDevice, "weapon");

             map.Generate(new int[,]
             {
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                 {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                 {2,2,2,2,2,2,1,1,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,1,1,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,1,1,2,2,2,2,2,2,2},

             }, 100);
            
            player.generatePlayer();
            camera = new Camera();
            inventory.LoadContent(Content, GraphicsDevice, "Inventar");

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
            camera.Follow(player);
            //inventory.openInventory(gameTime);
            //loot collision
            loot1.Collison(player);
            /*if (loot1.Collided)
            {
                loot1.texture = null;
            }*/

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
            inventory.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            loot1.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);

            //battlemode.drawBattle(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
