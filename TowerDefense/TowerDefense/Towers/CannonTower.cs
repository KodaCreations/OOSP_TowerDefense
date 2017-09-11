using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class CannonTower : Tower
    {
        public CannonTower(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            reloadTime = 2.0f;
            range = 350f;
            damage = 3;
            cost = 10;
            rotation = 0.0f;
            color = Color.White;
            name = "Cannon Tower";
            special = "N/A";
        }
    }
}
