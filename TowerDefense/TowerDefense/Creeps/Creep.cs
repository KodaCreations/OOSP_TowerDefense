using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class Creep : Object
    {
        protected double speed;
        protected int health;
        protected int goldValue;
        protected Vector2 oldPosition;
        protected Color originalColor;

        protected double slowedTimer;
        protected float slowedModifier;

        public bool isDead;
        public float texPos;
        

        public Creep(Vector2 position, Texture2D texture)
            : base(position, texture)
        {            
            texPos = 0;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        private void LookAt(Vector2 position, Vector2 oldPosition)
        {
            Vector2 direction = position - oldPosition;
            rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        public void Update(double deltaTime, SimplePath path)
        {
            oldPosition = position;

            if (slowedTimer <= 0)
            {
                texPos += (float)(speed * deltaTime);
                color = originalColor;
            }                
            else
            {
                texPos += (float)(speed * deltaTime) * slowedModifier;
                color = Color.CornflowerBlue;
            }
                

            slowedTimer -= deltaTime;

            position = path.GetPos(texPos);

            if (health <= 0)
            {
                UserInterface.gold += goldValue;
                isDead = true;
            }

            LookAt(position, oldPosition);

            base.Update(deltaTime);
        }

        public double SetSlowedTimer { set { slowedTimer = value; } }
        public float SetSlowedModifier { set { slowedModifier = value; } }
    }
}
