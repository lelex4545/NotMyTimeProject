using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeeresProjekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Charakter Animation
        Texture2D mainChar;
        Texture2D[] walkLeft;
        Texture2D[] walkRight;
        Texture2D[] walkUp;
        Texture2D[] walkDown;
        //Zeitlogik für die Bewegung
        float elapsedTime;
        float delay = 200f;
        int frames = 0;
        //Stehenbleiben
        KeyboardState oldState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;                  //Fullscreen
            graphics.PreferredBackBufferWidth = 1280;       //Auflösung
            graphics.PreferredBackBufferHeight = 720;

            IsMouseVisible = true;

            Content.RootDirectory = "Content";

            walkLeft = new Texture2D[5];
            walkRight = new Texture2D[5];
            walkUp = new Texture2D[3];
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

            // TODO: use this.Content to load your game content here

            mainChar = Content.Load<Texture2D>("figure_left");

            walkLeft[0] = Content.Load<Texture2D>("figure_left");
            walkLeft[1] = Content.Load<Texture2D>("figure_walk_left1");
            walkLeft[2] = Content.Load<Texture2D>("figure_walk_left2");
            walkLeft[3] = Content.Load<Texture2D>("figure_walk_left3");
            walkLeft[4] = Content.Load<Texture2D>("figure_walk_left4");

            walkRight[0] = Content.Load<Texture2D>("figure_right");
            walkRight[1] = Content.Load<Texture2D>("figure_walk_right1");
            walkRight[2] = Content.Load<Texture2D>("figure_walk_right2");
            walkRight[3] = Content.Load<Texture2D>("figure_walk_right3");
            walkRight[4] = Content.Load<Texture2D>("figure_walk_right4");

            walkUp[0] = Content.Load<Texture2D>("figure_up");
            walkUp[1] = Content.Load<Texture2D>("figure_walk_up1");
            walkUp[2] = Content.Load<Texture2D>("figure_walk_up2");

            /*      ***TEMPLATE FÜR OBEN UND UNTEN BEWEGUNG***
             * 
            walkDown[0] = Content.Load<Texture2D>("figure_down");
            walkDown[1] = Content.Load<Texture2D>("figure_walk_down1");
            walkDown[2] = Content.Load<Texture2D>("figure_walk_down2");
            */
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


            KeyboardState newState = Keyboard.GetState();   //Status der Gedrückten Taste als Variable

            //Zeitlogik damit sich der Charakter nicht zu schnell bewegt

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime >= delay)
            {
                if (frames >= 4)
                    frames = 1;
                else
                    frames++;
                elapsedTime = 0;
            }

            //Animation der Bewegung

            if (newState.IsKeyDown(Keys.Left) == true)
            {
                frames = 1;
                mainChar = walkLeft[frames];
                oldState = newState;
            } else if (oldState.IsKeyDown(Keys.Left))
            {
                mainChar = walkLeft[0];
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) == true)
            {
                frames = 1;
                mainChar = walkRight[frames];
                oldState = newState;
            }
            else if (oldState.IsKeyDown(Keys.Right))
            {
                mainChar = walkRight[0];
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) == true)
            {
                if (frames > 2) frames = 1;
                mainChar = walkUp[frames];
                oldState = newState;
            }
            else if (oldState.IsKeyDown(Keys.Up))
            {
                mainChar = walkUp[0];
            }

            /*  ***TEMPLATE FÜR UNTEN BEWEGUNG***
             *  
            if (Keyboard.GetState().IsKeyDown(Keys.Down) == true)
            {
                mainChar = walkDown[frames];
                oldState = newState;
            }
            else if (oldState.IsKeyDown(Keys.Down))
            {
                mainChar = walkDown[0];
            }
            */

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

            //Zeichne Charakter
            spriteBatch.Begin();
            spriteBatch.Draw(mainChar, new Vector2(GraphicsDevice.Viewport.Width / 2 , GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
