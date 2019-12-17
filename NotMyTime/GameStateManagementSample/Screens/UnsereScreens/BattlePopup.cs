using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class BattlePopup : GameScreen
    {
        #region Fields
        private ContentManager content;
        private SpriteFont gameFont;
        private float pauseAlpha;
        int gold = 0;
        int exp = 0;
        private Texture2D popup;

        int fadeValue = 150;
        int fadeIncrement = 3;
        double fadeDelay = .04;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public BattlePopup(int gold, int exp)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(1);
            //IsPopup = true;
            this.gold = gold;
            this.exp = exp;
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("Arial");

            popup = content.Load<Texture2D>("Battle/PopUpScreen");

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
                fadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                if (fadeDelay <= 0)
                {
                    fadeDelay = .04;
                    fadeValue += fadeIncrement * 3;
                    if (fadeValue >= 255 || fadeValue <= 100)
                    {
                        fadeIncrement *= -1;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
                {
                    ExitScreen();
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
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);

            spriteBatch.Draw(popup, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2, spriteBatch.GraphicsDevice.Viewport.Height / 2), null, Color.White,
                0f, new Vector2(popup.Width / 2, popup.Height / 2), 1f, SpriteEffects.None, 0f);

            spriteBatch.DrawString(gameFont, "You recieved: " + gold + " Gold", new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 100, spriteBatch.GraphicsDevice.Viewport.Height / 2 - 40), Color.White);
            spriteBatch.DrawString(gameFont, "You recieved: " + exp + " EXP ", new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 100, spriteBatch.GraphicsDevice.Viewport.Height / 2), Color.White);

            spriteBatch.DrawString(gameFont, "Press Enter to Continue", new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 105, spriteBatch.GraphicsDevice.Viewport.Height / 2 + 70),
                new Color(Color.White, MathHelper.Clamp(fadeValue, 100, 255)));

            spriteBatch.End();
        }

        #endregion Update and Draw
    }
}
