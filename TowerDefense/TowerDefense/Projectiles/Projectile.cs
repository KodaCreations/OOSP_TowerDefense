using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    abstract class Projectile:Object
    {
        protected Creep targetCreep;
        protected Vector2 direction;
        protected float speed;
        protected bool targetHit;
        protected int damage;
        protected float radius;
        protected double slowedTimer;
        protected float slowedModifier;
        protected Vector2 targetPosition;
        

        public Projectile(Vector2 position, Texture2D texture, Creep targetCreep, int damage):base(position, texture)
        {
            this.targetCreep = targetCreep;
            this.damage = damage;
            
        }

        public override void Update(double deltaTime)
        {            
            position += direction * speed;

            if (Vector2.Distance(position, targetPosition) <= speed)
            {
                targetHit = true;
                targetCreep.TakeDamage(damage);
            }

            base.Update(deltaTime);
        }

        public bool TargetHit() { return targetHit; }
        public float GetSpeed() { return speed; }
        public int GetDamage() { return damage; }
        public float GetRadius() { return radius; }
        public double GetSlowedTimer() { return slowedTimer; }
        public float GetSlowedModifier() { return slowedModifier; }
        public Creep TargetCreep() { return targetCreep; }
    }
}
