using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class Knight:Creep
    {
        public Knight(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
            speed = 170.0f;
            health = 32;
            goldValue = 60;
            color = Color.Red;
            originalColor = color;
        }
    }
}
