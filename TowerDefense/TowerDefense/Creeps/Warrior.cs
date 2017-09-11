using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class Warrior : Creep
    {
        public Warrior(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
            speed = 130.0f;
            health = 20;
            goldValue = 25;
            color = Color.Orange;
            originalColor = color;
        }
    }
}
