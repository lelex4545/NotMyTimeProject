﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement
{
    class Map
    {
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        private int width, heigth, size;
        public int Width
        {
            get { return width; }
        }

        public int Heigth
        {
            get { return heigth; }
        }

        public int Size
        {
            get { return size; }
        }
        public Map()
        { }

        public void Generate(int[,] map, int size)
        {
            this.size = size;
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    CollisionTiles.Add(new CollisionTiles(number, new Microsoft.Xna.Framework.Rectangle(x * size, y * size, size, size)));

                    width = (x + 1) * size;
                    heigth = (y + 1) * size;
                }
            
        }
        
        public void Generate2(int[,] map)
        {

        }

        public void LoadContent(ContentManager Content)
        {
            Tiles.LoadContent(Content);
            foreach (CollisionTiles tile in CollisionTiles)
                tile.loadC();
            
        }
        public void UnLoadContent()
        {
            Tiles.UnLoadContent();
        }
        public void generateMap1()
        {
            Generate(new int[,]
             {   {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,1,2,2,2,2,2,2,2,2,2,1,1,1,1,2,2,2,2,2,6,6,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,1,1,1,1,1,1,2,2,2,1,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,1,1,1,1,1,1,2,2,2,6,6,6,6,2,2,6,6,6,6,2,2,2,1,1,1,2,2,1,1,1,1,2,2,1,1,2,1,1,1,1,1,1,1,2,1,1,1,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,1,1,1,4,1,6,6,6,6,6,1,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1,1,1,1,1,1,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,6,6,6,6,6,6,1,1,6,6,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,7,7,7,7,7,7,1,1,7,7,7,7,7,7,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,7,7,7,7,7,1,1,1,1,7,7,7,7,7,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,1,1,1,1,1,1,1,1,1,1,9,1,1,1,6,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,1,6,6,1,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,7,1,1,1,1,1,8,8,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,1,6,6,1,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,7,7,1,1,1,1,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,1,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,1,1,6,6,6,1,1,1,1,1,1,1,7,7,1,1,1,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,1,1,6,6,1,1,1,1,1,1,1,1,1,7,7,1,1,1,1,1,1,1,1,7,7,1,1,1,1,1,1,1,7,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,1,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,1,1,1,6,1,1,1,1,1,1,1,1,1,1,7,7,1,1,1,1,1,1,7,7,1,1,1,1,1,1,1,7,10,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,1,1,1,6,1,1,1,1,1,1,1,1,1,1,1,7,7,1,1,1,1,7,7,7,7,1,1,1,1,1,7,10,10,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,1,1,1,6,1,1,1,1,1,1,1,1,1,1,1,1,7,1,7,7,7,7,1,1,7,1,1,1,1,1,7,10,10,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,1,1,1,6,1,1,9,1,1,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,7,7,7,7,1,1,7,1,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,6,1,6,6,1,1,1,1,1,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,1,7,1,1,7,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,1,1,1,1,1,1,7,7,7,7,7,7,7,7,7,7,1,7,1,1,7,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,6,6,6,6,1,1,6,6,6,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,6,1,1,1,1,9,1,1,1,6,1,1,1,1,1,1,1,7,7,7,7,7,7,7,7,7,1,7,7,1,7,1,1,1,1,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,6,6,1,1,1,1,1,1,1,1,6,6,6,6,6,6,6,1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1,1,6,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,7,7,7,7,7,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,6,6,6,1,1,1,1,1,1,1,1,6,6,6,6,6,6,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1,1,6,1,1,1,1,1,1,1,7,1,10,7,7,7,7,7,7,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,6,6,6,6,1,1,1,1,1,1,1,1,6,6,6,6,1,1,1,1,6,6,6,6,6,1,1,1,1,1,1,1,6,6,6,6,1,1,6,1,1,1,1,1,1,1,7,1,1,7,7,7,7,7,7,6,6,6,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,6,6,6,6,6,1,1,1,1,1,1,1,1,6,6,6,6,1,1,1,1,6,6,1,1,1,1,1,1,1,1,1,1,1,6,6,6,1,6,6,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,6,6,6,6,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,6,6,1,1,1,1,1,1,1,1,1,1,1,1,1,6,6,1,6,6,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,6,6,6,6,1,1,1,1,1,1,1,1,6,6,6,6,6,6,6,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,6,1,1,6,6,6,6,1,1,1,1,7,7,7,7,7,7,7,7,7,6,1,6,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,6,6,6,6,1,1,1,1,1,1,1,1,6,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,6,1,1,1,1,1,1,1,1,1,1,1,1,1,6,1,6,1,1,1,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,6,6,6,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,6,1,1,1,1,6,6,1,1,1,1,1,1,1,1,1,1,1,6,6,1,6,6,6,6,6,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,6,6,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,6,6,6,1,1,1,1,6,6,6,1,1,6,6,6,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,6,6,6,6,6,1,6,6,6,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,6,1,1,1,6,6,6,6,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,1,6,6,6,1,6,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,7,1,7,1,1,1,1,1,1,1,1,6,6,6,6,1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,1,1,6,6,6,1,6,6,7,1,1,1,1,1,1,1,1,1,1,1,7,1,1,1,1,1,1,7,1,1,1,1,1,6,6,6,6,1,6,1,1,1,1,1,1,1,1,6,6,6,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,1,7,6,6,6,1,6,6,1,1,1,1,1,1,1,1,1,1,7,1,1,7,1,1,7,1,1,1,7,1,1,1,1,1,1,1,6,1,6,1,1,1,1,1,1,1,1,6,6,6,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,6,1,6,7,1,1,1,1,1,1,1,1,1,7,1,6,6,6,6,6,6,6,6,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,1,1,9,1,6,6,6,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,1,6,6,1,6,1,7,1,1,1,1,1,1,1,7,1,1,6,1,1,1,1,1,1,6,7,1,1,1,1,1,1,1,1,6,1,6,1,1,1,1,1,1,1,1,6,6,6,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,1,1,1,6,6,6,1,6,7,1,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,6,1,7,1,1,1,1,1,1,1,6,1,6,1,1,1,1,1,1,1,1,1,1,1,1,1,9,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,1,6,6,6,6,1,6,6,1,7,1,7,1,7,1,7,1,7,1,6,1,1,1,9,1,1,6,7,1,1,1,1,1,1,1,6,6,1,6,6,1,1,1,1,1,1,6,6,6,6,6,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,6,1,1,1,1,6,6,6,6,7,1,7,1,7,1,7,1,7,6,1,1,1,1,1,1,6,1,1,7,1,1,1,1,6,6,1,1,1,6,6,1,1,1,1,6,6,6,6,6,6,6,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,6,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1,1,1,1,1,1,6,7,1,1,1,1,1,6,6,1,1,1,1,1,6,6,1,1,6,6,6,6,6,6,6,6,6,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,7,1,9,1,7,1,1,1,7,1,1,6,6,6,6,6,6,6,6,6,6,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,2,2,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,6,6,6,6,6,6,6,6,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,6,6,6,6,6,6,6,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,6,6,6,6,6,6,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,6,6,6,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},

             }, 100);
        }
        public void generateMap2()
        {
            Generate(new int[,]
             {
                 {12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,17,16,12,12,12,12,12,12,11,11,11,11,11,11,11,11,11,11,11,12},
                 {12,11,13,13,13,13,13,13,13,16,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,11,11,11,11,11,11,11,12},
                 {12,11,13,13,13,13,13,13,13,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12,},
                 {12,11,13,13,13,13,13,13,13,16,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,17,11,13,13,13,11,16,11,11,12},
                 {12,11,13,13,13,13,13,13,13,11,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,12,12,12,12,12,12,11,12,12,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,17,11,13,13,13,11,16,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,12,12,12,12,12,12,12,12,12,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,17,11,13,13,13,11,16,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,17,11,13,13,13,11,16,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,17,11,13,13,13,11,16,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,12,12,12,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,12,11,11,12,12,12,12,12,12,11,11,17,11,13,13,13,11,16,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,12,11,11,11,11,11,11,11,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,12,11,11,11,11,11,11,11,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,12,12,11,12,12,12,12,12,12,11,11,12,11,11,17,11,13,13,13,11,16,11,11,12},
                 {12,12,12,12,12,12,12,12,12,12,12,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,12,11,11,17,11,13,13,13,11,16,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,12,12,12,11,11,12,12,12,12,12,12,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,13,13,13,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,12,12,12,12,12,12,12,12,15,11,11,11,11,14,12,12,12,12,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,12,11,11,11,11,11,11,11,11,11,11,11,12},
                 {12,15,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,13,13,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,12,11,11,11,12,12,12,12,12},
                 {12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,13,13,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,12,12,12,12,12,12,12,12,11,11,11,11,11,11,12,12,12,12,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,12,12,12,11,11,12,12,12,11,11,11,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12},
                 {12,11,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12,11,11,12,12,12,12,11,11,11,11,11,11,11,11,12,11,11,11,11,11,11,11,11,11,11,11,11,11,11,12},
                 {12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12,12},
             }, 100);
        }

        public void generateMap3()
        {
            Generate(new int[,]
             {
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,19,18,18,18,18,18,18,19,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,19,18,18,18,18,18,18,19,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,19,18,18,18,18,18,18,19,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,19,18,18,18,18,18,18,19,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,18,19,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,19,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,18,18,19,18,19,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,18,18,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,18,18,18,18,18,18,18,18,18,18,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 {19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19},
                 
                 





             }, 100);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in collisionTiles)
                tile.Draw(spriteBatch);
        }
    }
}
