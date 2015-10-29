using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class Lord:Creep
    {
        public Lord(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
            speed = 100.0f;
            health = 50;
            goldValue = 150;
            color = Color.Purple;
            originalColor = color;
        }
    }
}
