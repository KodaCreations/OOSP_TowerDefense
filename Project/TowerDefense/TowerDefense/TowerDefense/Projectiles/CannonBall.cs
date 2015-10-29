using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class CannonBall:Projectile
    {     
        public CannonBall(Vector2 position, Texture2D texture, Creep targetCreep, int damage):base(position, texture, targetCreep, damage)
        {
            speed = 10.0f;
            color = Color.DarkGray;
        }

        public override void Update(double deltaTime)
        {
            targetPosition = targetCreep.GetPosition();
            direction = targetCreep.GetPosition() - position;
            direction.Normalize();
            base.Update(deltaTime);
        }
    }
}
