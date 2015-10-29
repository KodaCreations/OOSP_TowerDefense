using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class Manager
    {
        Level Level;
        

        public Manager(ContentManager content, GraphicsDevice graphics)
        {
            TextureManager.LoadContent(content);
            Level = new Level(graphics);
        }

        public void Update(double deltaTime)
        {            
            Level.Update(deltaTime);            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Level.Draw(spriteBatch);
        }

    }
}
