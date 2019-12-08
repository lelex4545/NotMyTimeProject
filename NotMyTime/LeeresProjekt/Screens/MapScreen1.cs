using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotMyTime.Screens
{
    class MapScreen1 : GameScreen
    {
        private Camera camera;

        Map map;                       //Map
        Player player;                 //Spielcharakter
        public Enemy[] enemyList;
        //Enemy goblin;                  //Spielgegner
        Inventory inventory;          //inventar Objekt
        LootManager lootManager;      //packt items ins inventar
        Loot loot1;                   //Schwert
        Loot loot2;                   //keule
        Gold gold1;
        Gold gold2;


        public MapScreen1()
        {
            loot1 = new Loot(0, 4900, 4050, 50, 100, 4950, 4100, 1.0f, 1.0f, 925, 420);
            loot2 = new Loot(1, 1400, 850, 50, 100, 1450, 900, 0.5f, 1.0f, 930, 425);

            gold1 = new Gold(100, 1250, 850, 50, 50, 1300, 900, 1.0f);
            gold2 = new Gold(150, 1150, 1000, 50, 50, 1200, 1100, 1.0f);

            inventory = new Inventory();
            map = new Map();
            enemyList = new Enemy[20];
            enemyList[0] = new Enemy(new Rectangle(13 * 100, 11 * 100, 100, 100));
            player = new Player(new Rectangle(11 * 100, 7 * 100, 100, 100));
            camera = new Camera();

        }

        public MapScreen1(MapScreen1 old) : this()      //Kopier-Konstruktor
        {
            loot1 = old.loot1;
            loot2 = old.loot2;
            inventory = old.inventory;
            map = old.map;
            enemyList = old.enemyList;
            player = old.player;
            //Content = old.Content;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            Sprite.Content = Content;

            Tiles.Content = Content;


            //goblin = new Enemy(new Rectangle(13 * 100, 11 * 100, 100, 100));



            //parameter: rectangle(x,y,größeX,größeY), X, Y, scale1, scale(inv), X(inv), Y(inv)

            



            loot1.LoadContent(Content, "weapon");
            loot2.LoadContent(Content, "weapon2");

            gold1.LoadContent(Content, "gold");
            gold2.LoadContent(Content, "gold");

            inventory.LoadContent(Content, "Inventar");

            map.generateMap1();

            player.generatePlayer();
            //goblin.generateEnemy("goblin");
            if (enemyList[0] != null) enemyList[0].generateEnemy("goblin");



        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            //Tiles.UnLoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            player.updatePosition(gameTime, map);
            //battle = goblin.moveOne(gameTime, map, player);
            for (int i = 0; enemyList[i] != null; i++)
                enemyList[i].moveOne(gameTime, map, player);
            camera.Follow(player);
            //inventory.openInventory(gameTime);
            //loot collision
            loot1.Collison(player);
            loot2.Collison(player);
            //gold collision
            gold1.Collison(player);
            gold2.Collison(player);
            //font nullen
            gold1.updateFont(gameTime);
            gold2.updateFont(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.Transform);
            base.Draw(spriteBatch);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            //goblin.Draw(spriteBatch);
            for (int i = 0; enemyList[i] != null; i++)
                enemyList[i].Draw(spriteBatch);
            inventory.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);

            loot1.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            loot2.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);

            gold1.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            gold2.Draw(spriteBatch, player.rectangle.X, player.rectangle.Y);
            spriteBatch.End();
        }
    }
}
