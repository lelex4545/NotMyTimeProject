﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameStateManagement
{
    class Enemy : Sprite
    {
        private float lastMove = 0f;
        private float wait = 0f;
        private float chill = 0f;

        Texture2D[] left, right, top, bottom;
        private String name;
        public const int size = 50;
        public const int size2 = size * 2;
        private int which = 0;
        private int help = 0;
        private bool col = false;
        private static Random rand;
        static Rectangle[] usedPosition;
        public Enemy(Rectangle newRectangle)
        {
            if (rand == null)
                rand = new Random();
            if (usedPosition == null)
                usedPosition = new Rectangle[20];
            //this.rectangle = newRectangle;
            left = new Texture2D[3];
            right = new Texture2D[3];
            bottom = new Texture2D[3];
            top = new Texture2D[3];

        }
        public Enemy(Vector2[] newRectangle)
        {
            if (rand == null)
                rand = new Random();
            if (usedPosition == null)
                usedPosition = new Rectangle[20];
            //this.rectangle = newRectangle;
            this.rectangle= spawnPosition(newRectangle);
            left = new Texture2D[3];
            right = new Texture2D[3];
            bottom = new Texture2D[3];
            top = new Texture2D[3];
            
        }
        public void generateEnemy(String name)
        {
            this.name = name;
            right[0] = Content.Load<Texture2D>(name + "_right0");
            right[1] = Content.Load<Texture2D>(name + "_right1");
            right[2] = Content.Load<Texture2D>(name + "_right2");

            left[0] = Content.Load<Texture2D>(name + "_left0");
            left[1] = Content.Load<Texture2D>(name + "_left1");
            left[2] = Content.Load<Texture2D>(name + "_left2");

            top[0] = Content.Load<Texture2D>(name + "_top0");
            top[1] = Content.Load<Texture2D>(name + "_top1");
            top[2] = Content.Load<Texture2D>(name + "_top2");

            texture = bottom[0] = Content.Load<Texture2D>(name + "_bottom0");
            bottom[1] = Content.Load<Texture2D>(name + "_bottom1");
            bottom[2] = Content.Load<Texture2D>(name + "_bottom2");
        }

        public Rectangle spawnPosition(Vector2[] room)
        {
            bool reloop = false;
            Rectangle tmp;
            do
            {
                reloop = false;
                tmp = new Rectangle(rand.Next((int)room[0].X, (int)room[1].X + 1) * 100, rand.Next((int)room[0].Y, (int)room[1].Y + 1) * 100, 100, 100);
                for (int i = 0; i< usedPosition.Length; i++)
                    if (usedPosition[i].Equals(tmp))
                        reloop = true;
            } while (reloop);
            return tmp;
        }

        public void moveOne(GameTime gameTime, Map map, Player player, ScreenManager screenManager, PlayerIndex? controllingPlayer, Enemy[] enemyList, int i)
        {
            wait += (float)gameTime.ElapsedGameTime.TotalSeconds;
            lastMove += (float)gameTime.ElapsedGameTime.TotalSeconds;
            chill += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Rectangle tmp;
            if (which == 0)
            {
                tmp = new Rectangle(rectangle.X, rectangle.Y + size2, rectangle.Width, rectangle.Height);
                foreach (CollisionTiles tile in map.CollisionTiles)
                {
                    if (RectangleHelper.TouchCheck(tmp, tile.Rectangle, size) && (tile.texture.Name.Equals("tile2") || tile.texture.Name.Equals("tile3") ||
                        tile.texture.Name.Equals("tile6") || tile.texture.Name.Equals("tile7") || tile.texture.Name.Equals("tile12") || tile.texture.Name.Equals("tile14")
                        || tile.texture.Name.Equals("tile15") || tile.texture.Name.Equals("tile16") || tile.texture.Name.Equals("tile17") || tile.texture.Name.Equals("tile19")))
                    {
                        col = true;
                        which = 1;
                    }
                }
                if (RectangleHelper.TouchCheck(this.rectangle, player.rectangle, 0))
                {
                    screenManager.AddScreen(new BattleScreen(enemyList, i), controllingPlayer);
                }
            }
            if (which == 1)
            {
                tmp = new Rectangle(rectangle.X - size2, rectangle.Y, rectangle.Width, rectangle.Height);
                foreach (CollisionTiles tile in map.CollisionTiles)
                {
                    if (RectangleHelper.TouchCheck(tmp, tile.Rectangle, size) && (tile.texture.Name.Equals("tile2") || tile.texture.Name.Equals("tile3") ||
                        tile.texture.Name.Equals("tile6") || tile.texture.Name.Equals("tile7") || tile.texture.Name.Equals("tile12") || tile.texture.Name.Equals("tile14")
                        || tile.texture.Name.Equals("tile15") || tile.texture.Name.Equals("tile16") || tile.texture.Name.Equals("tile17") || tile.texture.Name.Equals("tile19")))
                    {
                        col = true;
                        which = 2;
                    }
                }
                if (RectangleHelper.TouchCheck(this.rectangle, player.rectangle, 0))
                {
                    screenManager.AddScreen(new BattleScreen(enemyList, i), controllingPlayer);
                }
            }
            if (which == 2)
            {
                tmp = new Rectangle(rectangle.X, rectangle.Y - size2, rectangle.Width, rectangle.Height);
                foreach (CollisionTiles tile in map.CollisionTiles)
                {
                    if (RectangleHelper.TouchCheck(tmp, tile.Rectangle, size) && (tile.texture.Name.Equals("tile2") || tile.texture.Name.Equals("tile3") ||
                        tile.texture.Name.Equals("tile6") || tile.texture.Name.Equals("tile7") || tile.texture.Name.Equals("tile12") || tile.texture.Name.Equals("tile14")
                        || tile.texture.Name.Equals("tile15") || tile.texture.Name.Equals("tile16") || tile.texture.Name.Equals("tile17") || tile.texture.Name.Equals("tile19")))
                    {
                        col = true;
                        which = 3;
                    }
                }
                if (RectangleHelper.TouchCheck(this.rectangle, player.rectangle, 0))
                {
                    screenManager.AddScreen(new BattleScreen(enemyList, i), controllingPlayer);
                }
            }
            if (which == 3)
            {
                tmp = new Rectangle(rectangle.X + size2, rectangle.Y, rectangle.Width, rectangle.Height);
                foreach (CollisionTiles tile in map.CollisionTiles)
                {
                    if (RectangleHelper.TouchCheck(tmp, tile.Rectangle, size) && (tile.texture.Name.Equals("tile2") || tile.texture.Name.Equals("tile3") ||
                        tile.texture.Name.Equals("tile6") || tile.texture.Name.Equals("tile7") || tile.texture.Name.Equals("tile12") || tile.texture.Name.Equals("tile14") 
                        || tile.texture.Name.Equals("tile15") || tile.texture.Name.Equals("tile16") || tile.texture.Name.Equals("tile17") || tile.texture.Name.Equals("tile19")))
                    {
                        col = true;
                        which = 0;
                    }
                }
                if (RectangleHelper.TouchCheck(this.rectangle, player.rectangle, 0))
                {
                    screenManager.AddScreen(new BattleScreen(enemyList, i), controllingPlayer);
                }
            }

            if (col == false)
            {
                if (wait >= 0.15f)
                {
                    if (help == 1)
                    {
                        rectangle.Y = rectangle.Y + size;
                        texture = bottom[2];
                        which = 1;
                        help = 0;
                        wait = 0f;
                    }
                    if (help == 2)
                    {
                        rectangle.X = rectangle.X - size;
                        texture = left[2];
                        which = 2;
                        help = 0;
                        wait = 0f;
                    }
                    if (help == 3)
                    {
                        rectangle.Y = rectangle.Y - size;
                        texture = top[2];
                        which = 3;
                        help = 0;
                        wait = 0f;
                    }
                    if (help == 4)
                    {

                        rectangle.X = rectangle.X + size;
                        texture = right[2];
                        which = 0;
                        help = 0;
                        wait = 0f;
                    }
                }
                if (chill >= 1f)
                {
                    if (lastMove >= 0.3f)
                    {
                        if (which == 0)
                        {
                            texture = bottom[1];
                            tmp = new Rectangle(rectangle.X, rectangle.Y + size2, rectangle.Width, rectangle.Height);
                            if (RectangleHelper.TouchCheck(tmp, player.rectangle, 0))
                            {
                                screenManager.AddScreen(new BattleScreen(enemyList, i), controllingPlayer);
                            }
                            rectangle.Y = rectangle.Y + size;
   
                            help = 1;
                            lastMove = 0f;
                            wait = 0f;
                            chill = 0f;
                        }
                        if (which == 1)
                        {
                            tmp = new Rectangle(rectangle.X - size2, rectangle.Y, rectangle.Width, rectangle.Height);
                            if (RectangleHelper.TouchCheck(tmp, player.rectangle, 0))
                            {
                                screenManager.AddScreen(new BattleScreen(enemyList, i), controllingPlayer);
                            }
                            rectangle.X = rectangle.X - size;
                            texture = left[1];
                            help = 2;
                            lastMove = 0f;
                            wait = 0f;
                            chill = 0f;
                        }
                        if (which == 2)
                        {
                            tmp = new Rectangle(rectangle.X, rectangle.Y - size2, rectangle.Width, rectangle.Height);
                            if (RectangleHelper.TouchCheck(tmp, player.rectangle, 0))
                            {
                                screenManager.AddScreen(new BattleScreen(enemyList, i), controllingPlayer);
                            }
                            rectangle.Y = rectangle.Y - size;
                            texture = top[1];
                            help = 3;
                            lastMove = 0f;
                            wait = 0f;
                            chill = 0f;
                        }
                        if (which == 3)
                        {
                            tmp = new Rectangle(rectangle.X + size2, rectangle.Y, rectangle.Width, rectangle.Height);
                            if (RectangleHelper.TouchCheck(tmp, player.rectangle, 0))
                            {
                                screenManager.AddScreen(new BattleScreen(enemyList, i), controllingPlayer);
                            }
                            rectangle.X = rectangle.X + size;
                            texture = right[1];
                            help = 4;
                            lastMove = 0f;
                            wait = 0f;
                            chill = 0f;
                        }
                    }
                }
            }
            col = false;
            //return new CollisionChecker(false, this);
        }

        public string GetName()
        {
            return this.name;
        }
    }
}
