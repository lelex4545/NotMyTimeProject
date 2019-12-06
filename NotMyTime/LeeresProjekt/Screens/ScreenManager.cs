using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NotMyTime.Screens
{
    class ScreenManager
    {
        public ContentManager Content { private set; get; }
        private static ScreenManager instance;

        GameScreen currentScreen;
        GameScreen oldScreen;
        DeathChecker battle;
        Enemy currentEnemy;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }
        public ScreenManager()
        {
            currentScreen = new MapScreen1();
            battle = new DeathChecker(false, 0);
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
            if (battle.isFighting)
            {
                BattleScreen bs = (BattleScreen)currentScreen;
                battle = bs.IsEnemyAlive();
                if (!battle.isFighting)
                {
                    currentScreen.UnloadContent();
                    currentScreen = oldScreen;
                    Type type = currentScreen.GetType();
                    if (type.Name == "MapScreen1")
                    {
                        MapScreen1 temp = (MapScreen1)currentScreen;
                        for (int i = 0; temp.enemyList[i] != null; i++)
                            if (temp.enemyList[i] == currentEnemy)
                                temp.enemyList[i] = null;
                    }
                    else if (type.Name == "MapScreen2")
                    {

                    }
                    else if (type.Name == "MapScreen3")
                    {

                    }
                    currentScreen.LoadContent();
                }
            }

            currentScreen.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }
        public void Collision(ContentManager Content, int enemyType, Enemy enemy)
        {
            currentEnemy = enemy;
            oldScreen = new MapScreen1((MapScreen1)currentScreen);
            currentScreen.UnloadContent();
            currentScreen = new BattleScreen(enemyType, 0);
            currentScreen.LoadContent();
            battle.isFighting = true;
        }
    }
}
