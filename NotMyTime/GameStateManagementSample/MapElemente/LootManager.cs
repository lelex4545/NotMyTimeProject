using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class LootManager
    {
        public ArrayList lootList { get; set; }
        public Loot currentLoot { get; set; }
        MainFighter mainChar;
        public int nr = 0;

        public float lastChange;
        public bool keyBlock;

        public int poX = 0;
        public int poY = 0;
        public int x = 0;
        public int y = 0;
        public float scale2 = 0f;
        //public Texture2D texture;

        //---------------------------------------------------------------
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        //---------------------------------------------------------------- 
        public LootManager(MainFighter mainChar)
        {
            lootList = new ArrayList();
            lootList.Add(new Loot(-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "Battle/emptyLoot"));
            currentLoot = (Loot)lootList[nr];
            this.mainChar = mainChar;
            mainChar.currentWeaponID = currentLoot.id;
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            spriteBatch.Draw(currentLoot.texture, new Vector2(x + currentLoot.poX, y + currentLoot.poY), null, Color.White, 0.0f, new Vector2(currentLoot.texture.Width / 2, currentLoot.texture.Height / 2), currentLoot.scale2, SpriteEffects.None, 0f);
        }
        public void LoadContent(ContentManager content)
        {
            foreach (Loot loot in lootList)
            {
                loot.LoadContent(content);
            }
        }

        public void update(GameTime gameTime)
        {
            lastChange += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyUp(Keys.Q))
                keyBlock = true;

            if (lastChange >= 0.1f && keyBlock)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    if (lootList.Count > 1)
                    {
                        nr = (nr+1) % lootList.Count;
                        currentLoot = (Loot)lootList[nr];
                        mainChar.currentWeaponID = currentLoot.id;
                    }
                    keyBlock = false;
                }
                lastChange = 0f;
            }
        }
    }
}
