using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotMyTime
{
    class BattleManager
    {
        public ContentManager Content { private set; get; }
        public Vector2 Dimensions { private set; get; }
        private static BattleManager instance;

        BattleScreen currentScreen;
        public static BattleManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new BattleManager();
                return instance;
            }
        }
        public BattleManager()
        {
            Dimensions = new Vector2(1920, 1080);
            currentScreen = new ActualBattleScreen(0, 0);
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }
        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }
        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad0) == true)
            {
                currentScreen.UnloadContent();
                currentScreen = new ActualBattleScreen(0, 0);
                currentScreen.LoadContent();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1) == true)
            {
                currentScreen.UnloadContent();
                currentScreen = new ActualBattleScreen(1, 0);
                currentScreen.LoadContent();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2) == true)
            {
                currentScreen.UnloadContent();
                currentScreen = new ActualBattleScreen(2, 0);
                currentScreen.LoadContent();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad3) == true)
            {
                currentScreen.UnloadContent();
                currentScreen = new ActualBattleScreen(3, 0);
                currentScreen.LoadContent();
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        public bool FightResult(GameTime gameTime, Map map, Player player, Loot l)
        {
            this.Update(gameTime);
            if (!currentScreen.IsEnemyAlive())
            {
                this.UnloadContent();
                map.LoadContent(Content);
                player.generatePlayer();
                //l.LoadContent(Content);
            }
            return currentScreen.IsEnemyAlive();
        }
    }
}
