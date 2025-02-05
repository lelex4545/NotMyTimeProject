﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class Boss : Sprite
    {
        Texture2D[] moving;
        public String name { get; }
        float move;
        bool down;
        int index;
        Rectangle[] bigRectangle;
        public bool IsAlive {set;get;}

        public Boss(Rectangle rectangle,String name)
        {
            if (rectangle.Height == 200)
            {
                bigRectangle = new Rectangle[4];
                loadBigRectangle(rectangle);
            }  
            this.rectangle = rectangle;
            this.name = name;
            moving = new Texture2D[3];
            index = 1;
            down = false;
            IsAlive = true;
        }

        public void generateBoss()
        {
            moving[0] = Content.Load<Texture2D>(name + "1");
            texture = moving[0];
            moving[1] = Content.Load<Texture2D>(name + "2");
            moving[2] = Content.Load<Texture2D>(name + "3");
        }
        public void generateSpecial()
        {
            texture = Content.Load<Texture2D>(name);
        }

        public void updateSpecial(GameTime gametime, Map map, Player player, ScreenManager screenManager, PlayerIndex? controllingPlayer, Boss boss)
        {
            
                if (RectangleHelper.TouchCheck(this.rectangle, player.rectangle, 0))
                {
                    screenManager.AddScreen(new BattleScreen(boss), controllingPlayer);
                }
            
        }
        public void updateBoss(GameTime gametime, Map map, Player player, ScreenManager screenManager, PlayerIndex? controllingPlayer, Boss boss)
        {
            move += (float)gametime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < 4; i++)
            {
                if (RectangleHelper.TouchCheck(this.bigRectangle[i],player.rectangle,0))
                {
                    screenManager.AddScreen(new BattleScreen(boss), controllingPlayer);
                }
            }
            if(move>=0.25f)
            {
                if (index == 2)
                    down = true;
                if(index == 0)
                    down = false;

                if (down == true)
                    index--;
                if (down == false)
                    index++;

                texture = moving[index];
                move = 0f;            }

        }
        public void loadBigRectangle(Rectangle rectangle)
        {
            bigRectangle[0] = rectangle;
            bigRectangle[1] = new Rectangle(rectangle.X + 100, rectangle.Y, 100, 100);
            bigRectangle[2] = new Rectangle(rectangle.X, rectangle.Y + 100, 100, 100);
            bigRectangle[3] = new Rectangle(rectangle.X + 100, rectangle.Y + 100, 100, 100);
        }

    }
}
