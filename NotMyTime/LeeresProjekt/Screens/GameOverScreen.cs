using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NotMyTime.Screens
{
    public class GameOverScreen : GameScreen
    {
        Texture2D endScreen;
        SpriteFont font;
        public GameOverScreen() { }

        public override void LoadContent()
        {
            base.LoadContent();
            endScreen = Content.Load<Texture2D>("Battle/deathScreen");
            font = Content.Load<SpriteFont>("Arial");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            /*
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                ScreenManager.Instance.Restart();
            }
            */
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ScreenManager.Instance.RestartGame();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            base.Draw(spriteBatch);
            spriteBatch.Draw(endScreen, new Vector2(), Color.White);
            spriteBatch.DrawString(font, "Press any Key to Continue", 
                new Vector2(spriteBatch.GraphicsDevice.Viewport.Width/2 -200,spriteBatch.GraphicsDevice.Viewport.Height/2 + 400), Color.Gray);
            spriteBatch.End();
        }
    }
}
