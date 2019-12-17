using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class GUI
    {
        private Texture2D texture;
        private Texture2D currentHealth;
        private Texture2D healthbar;
        private Texture2D currentMana;
        private Texture2D manabar;
        private Texture2D currentexp;
        private Texture2D expbar;
        private SpriteFont font;
        private SpriteFont fontBig;
        private SpriteFont fontSmall;

        public GUI() {  }

        private static ContentManager content;

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("GUI");
            healthbar = content.Load<Texture2D>("gui_balken2");
            currentHealth = content.Load<Texture2D>("gui_balken1");
            manabar = content.Load<Texture2D>("gui_balken2");
            currentMana = content.Load<Texture2D>("gui_balken1");
            currentexp = content.Load<Texture2D>("gui_exp2");
            expbar = content.Load<Texture2D>("gui_exp1");
            font = content.Load<SpriteFont>("Arial");
            fontBig = content.Load<SpriteFont>("ArialBig");
            fontSmall = content.Load<SpriteFont>("ArialSmall");
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            spriteBatch.Draw(texture, new Vector2(x - 900, y - 477), null, Color.White, 0.0f, new Vector2(0,0), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(currentHealth,
                new Rectangle(x - 891, y - 370,
                    (int)(currentHealth.Width * ((double)GameScreen.mainChar.Stats.CurrentLP / GameScreen.mainChar.Stats.Lifepoints)), currentHealth.Height),
                new Rectangle(0, 0, currentHealth.Width, currentHealth.Height), Color.Red);
            spriteBatch.Draw(healthbar, new Vector2(x - 892, y - 370), Color.Red);

            spriteBatch.Draw(currentMana,
                new Rectangle(x - 890, y - 335,
                    (int)(currentMana.Width * ((double)GameScreen.mainChar.Stats.CurrentMP / GameScreen.mainChar.Stats.Manapoints)), currentMana.Height),
                new Rectangle(0, 0, currentMana.Width, currentMana.Height), Color.Blue);
            spriteBatch.Draw(manabar, new Vector2(x - 891, y - 336), Color.White);

            spriteBatch.Draw(currentexp,
                new Rectangle(x - 890, y - 390,
                    (int)(currentexp.Width * ((double)GameScreen.mainChar.Level.GetExp() / GameScreen.mainChar.Level.GetNeededExp())), currentexp.Height),
                new Rectangle(0, 0, currentexp.Width, currentexp.Height), Color.Green);
            spriteBatch.Draw(expbar, new Vector2(x - 891, y - 391), Color.White);

            spriteBatch.DrawString(font, "HP: " + GameScreen.mainChar.Stats.CurrentLP + "/" + GameScreen.mainChar.Stats.Lifepoints, new Vector2(x - 885, y - 365), Color.White);
            spriteBatch.DrawString(font, "MP: " + GameScreen.mainChar.Stats.CurrentMP + "/" + GameScreen.mainChar.Stats.Manapoints, new Vector2(x - 885, y - 330), Color.White);
            spriteBatch.DrawString(font, "EXP: " + GameScreen.mainChar.Level.GetExp() + "/" + GameScreen.mainChar.Level.GetNeededExp(), new Vector2(x - 885, y - 388), 
                Color.White, 0f, new Vector2(), 0.6f, SpriteEffects.None, 0f);

            spriteBatch.DrawString(fontBig, "Bruce   ", new Vector2(x - 770, y - 465), Color.White);
            spriteBatch.DrawString(font, "Lvl: " + GameScreen.mainChar.Level.GetLevel(), new Vector2(x - 770, y - 435), Color.AntiqueWhite);
        }

        
    }
}
