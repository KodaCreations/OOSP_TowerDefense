using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class MagicTower : Tower
    {
        public MagicTower(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            reloadTime = 4.0f;
            range = 200f;
            damage = 6;
            cost = 20;
            rotation = 0.0f;
            color = Color.CornflowerBlue;
            name = "Magic Tower";
            special = "Area of Effect,\nSlows creep";
        }
    }
}