using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement
{
    class GameOverScreen : GameScreen
    {
        private ContentManager content;
        private SpriteFont gameFont;
        private Random random = new Random();
        private float pauseAlpha;

        int mAlphaValue = 1;
        int mFadeIncrement = 3;
        double mFadeDelay = .035;

        Texture2D endScreen;
        SpriteFont font;

        public GameOverScreen() 
        {
            TransitionOnTime = TimeSpan.FromSeconds(2);
            TransitionOffTime = TimeSpan.FromSeconds(1);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");

            endScreen = content.Load<Texture2D>("Battle/deathScreen");
            font = content.Load<SpriteFont>("Arial");

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }
        public override void UnloadContent()
        {
            content.Unload();
        }
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
                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                {
                    LoadingScreen.Load(ScreenManager, false, ControllingPlayer, new BackgroundScreen(), new MainMenuScreen());
                }

                mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                //If the Fade delays has dropped below zero, then it is time to 
                //fade in/fade out the image a little bit more.
                if (mFadeDelay <= 0)
                {
                    //Reset the Fade delay
                    mFadeDelay = .035;

                    //Increment/Decrement the fade value for the image
                    mAlphaValue += mFadeIncrement*3;

                    //If the AlphaValue is equal or above the max Alpha value or
                    //has dropped below or equal to the min Alpha value, then 
                    //reverse the fade
                    if (mAlphaValue >= 255 || mAlphaValue <= 0)
                    {
                        mFadeIncrement *= -1;
                    }
                }
            }
        }

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

        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);

            spriteBatch.Draw(endScreen, new Vector2(), Color.White);

            spriteBatch.DrawString(font, "Press any Key to Continue",
                new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 200, spriteBatch.GraphicsDevice.Viewport.Height / 2 + 300),
                new Color(Color.AntiqueWhite, MathHelper.Clamp(mAlphaValue, 0, 255)));

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}
