using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NotMyTime.Screens
{
    public class GameOverScreen : GameScreen
    {
        Texture2D endScreen;
        public GameOverScreen() { }

        public override void LoadContent()
        {
            base.LoadContent();
            endScreen = Content.Load<Texture2D>("Battle/deathScreen");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            base.Draw(spriteBatch);
            spriteBatch.Draw(endScreen, new Vector2(), Color.White);
            spriteBatch.End();
        }
    }
}
