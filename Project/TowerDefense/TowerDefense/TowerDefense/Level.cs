using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class Level
    {
        public SimplePath path;
        public CreepManager creepManager;
        TowerManager towerManager;
        private bool gameOver;

        public Level(GraphicsDevice graphics)
        {
            path = new SimplePath(graphics);
            path.Clean();

            path.AddPoint(new Vector2(225, -64));
            path.AddPoint(new Vector2(190, 535));
            path.AddPoint(new Vector2(240, 700));
            path.AddPoint(new Vector2(350, 760));
            path.AddPoint(new Vector2(555, 745));
            path.AddPoint(new Vector2(635, 645));
            path.AddPoint(new Vector2(620, 470));
            path.AddPoint(new Vector2(605, 290));
            path.AddPoint(new Vector2(655, 200));
            path.AddPoint(new Vector2(780, 155));
            path.AddPoint(new Vector2(935, 195));
            path.AddPoint(new Vector2(990, 345));
            path.AddPoint(new Vector2(960, 984));
            
            creepManager = new CreepManager(ref path);
            towerManager = new TowerManager(ref creepManager, graphics);
        }

        public void Update(double deltaTime)
        {
            if (creepManager.life <= 0 || creepManager.IsGameWon())
            {
                gameOver = true;
            }
            if (!gameOver)
            {
                creepManager.Update(deltaTime);
                towerManager.Update(deltaTime);
            }     
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.background, Vector2.Zero, Color.White);
            spriteBatch.Draw(TextureManager.pathTexture, Vector2.Zero, Color.White);            
            creepManager.Draw(spriteBatch);
            towerManager.Draw(spriteBatch);

            //path.Draw(spriteBatch);
            //path.DrawPoints(spriteBatch);            
        }
    }
}
