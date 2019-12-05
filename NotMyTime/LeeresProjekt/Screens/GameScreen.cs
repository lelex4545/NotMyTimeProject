using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NotMyTime.Screens
{
    public class GameScreen
    {
        protected ContentManager Content;

        public Type Type;
        public GameScreen()
        {
            Type = this.GetType();
        }
        public virtual void Initialize()
        {

        }
        public virtual void LoadContent()
        {
            Content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");
        }
        public virtual void UnloadContent()
        {
            Content.Unload();
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
