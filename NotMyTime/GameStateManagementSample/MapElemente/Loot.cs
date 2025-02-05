﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class Loot
    {
        public static int status;     //aktuelle Waffe
        public int id;
        public Texture2D texture;
        public bool pickup;

        //skalierung
        private int posX;
        private int posY;
        private float scale;
        //inventar skalierung
        public float scale2;
        public int poX;
        public int poY;
        public string assetName { get; set; }

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

        public bool Collided { get; private set; }

        //---------------------------------------------
        //Constructor
        public Loot(int id, int x, int y, int scaleX, int scaleY, int posX, int posY, float scale, float scale2, int poX, int poY, string assetname)
        {
            this.id = id;
            this.Rectangle = new Rectangle(x, y, scaleX, scaleY);
            this.posX = posX;
            this.posY = posY;
            this.scale = scale;

            this.scale2 = scale2;
            this.poX = poX;
            this.poY = poY;

            pickup = false;
            status = 0;

            this.assetName = assetname;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        //Draw Loot
        public void Draw(SpriteBatch spriteBatch, int x, int y, LootManager lootManager)
        {
            //Gegenstand auf dem Spielfeld
            if (!Collided && !pickup)   //wenn nicht kollidiert und nicht aufgehoben
            {
                spriteBatch.Draw(texture, new Vector2(posX, posY), null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            }
            //Gegenstand aus dem inventar entfernen wenn ein anderer Gegenstand eingesammelt wird
           /* else if (id != status  && pickup == true)   //wenn die Waffe nicht die aktuelle ist und Gegenstand aufgehoben ist
            {
                texture = null;
            }*/
            //Gegendstand im Inventar anzeigen wenn er eingesammel wurde
            else if(Collided && !pickup) //wenn kollidiert und aufgehoben
            {
                //spriteBatch.Draw(texture, new Vector2(x + poX, y + poY), null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height / 2), scale2, SpriteEffects.None, 0f);
                rectangle = Rectangle.Empty;
                lootManager.lootList.Add(this);
                //status = id;
                pickup = true;
            }
        }

        //Loot soll verschwinden und im Inventar auftauchen
        public bool Collison(Sprite target)
        {
            bool intersects = rectangle.Intersects(target.rectangle);
            Collided = intersects;

            return intersects;
        }
    }
}
