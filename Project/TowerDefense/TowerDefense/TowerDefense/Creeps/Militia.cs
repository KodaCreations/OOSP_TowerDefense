using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class Militia : Creep
    {
        public Militia(Vector2 position, Texture2D texture):base(position, texture)
        {
            speed = 100.0f;
            health = 12;
            goldValue = 15;
            color = Color.Yellow;
            originalColor = color;
        }
    }
}
