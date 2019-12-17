using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class HealingAnimation : GameScreen
    {
        #region Fields
        private ContentManager content;
        private SpriteFont gameFont;

        Texture2D animationTexture;
        Rectangle[,] animationContainer;
        Rectangle currentAnimation;
        int a = 0;
        int b = 0;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 150;

        Vector2 targetPosition;

        bool animationOver = false;
        string debugger = "";
        #endregion Fields

        #region Initialization
        /// <summary>
        /// Constructor.
        /// </summary>
        public HealingAnimation(Vector2 targetPosition)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(1);
            IsPopup = true;

            this.targetPosition = targetPosition;

            animationContainer = new Rectangle[3, 5];
            int x = 192;
            int y = 192;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    animationContainer[i, j] = new Rectangle(new Point(x * i, y * j), new Point(192));
                }
            }
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("Arial");

            animationTexture = content.Load<Texture2D>("Battle/Animations/Cure2");

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

            if (IsActive)
            {
                //elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame -= millisecondsPerFrame;
                    currentAnimation = animationContainer[a, b++];
                    if (b == 5)
                    {
                        a++;
                        b = 0;
                    }
                    if(a == 3)
                    {
                        animationOver = true;
                    }
                }
            }
            if (animationOver)
            {
                ExitScreen();
            }
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            if(!animationOver)
                spriteBatch.Draw(animationTexture, targetPosition, currentAnimation, Color.White, 0f, new Vector2(), 1.5f, SpriteEffects.None, 0f);

            spriteBatch.End();
        }

        #endregion Update and Draw
    }
}
