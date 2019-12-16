using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class Portal : Sprite
    {
        Texture2D[] moving;
        public String name { get; }
        float move;

        public Portal(Rectangle rectangle, String name)
        {
            this.rectangle = rectangle;
            this.name = name;
            moving = new Texture2D[3];
        }

        public void generatePortal()
        {
            texture = moving[0] = Content.Load<Texture2D>(name + "1");
            moving[1] = Content.Load<Texture2D>(name + "2");
            moving[2] = Content.Load<Texture2D>(name + "3");
        }

        public void updatePortal(GameTime gametime, Map map, Player player, ScreenManager screenManager, PlayerIndex? controllingPlayer, MainFighter main, LootManager lootManager)
        {
            move += (float) gametime.ElapsedGameTime.TotalSeconds;

            if (RectangleHelper.TouchCheck(this.rectangle, player.rectangle, 0))
            {
                //screenManager.AddScreen(new SecondMap(), controllingPlayer);
                LoadingScreen.Load(screenManager, true, controllingPlayer, new SecondMap(main,lootManager));
            }

            if (move >= 0.25f)
            {
                if(texture.Equals(moving[0]))
                {
                    texture = moving[1];
                    move = 0f;
                }
                else
                if(texture.Equals(moving[1]))
                {
                    texture = moving[2];
                    move = 0f;
                }
                else
                if(texture.Equals(moving[2]))
                {
                    texture = moving[0];
                    move = 0f;
                }
            }
        }

    }
}
