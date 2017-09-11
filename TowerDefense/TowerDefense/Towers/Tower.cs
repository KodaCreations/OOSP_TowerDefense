using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class Tower : Object
    {       
        protected double reloadTime;
        protected float range;
        protected int damage;
        protected int cost;
        protected string name;
        protected string special;
        protected bool isHovered;
        protected MouseState mouseState;

        double timer;
        public bool canFire = true;

        public Tower(Texture2D texture, Vector2 position)
            : base(position, texture)
        {
            timer = reloadTime;
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            if (!canFire)
            {
                timer -= deltaTime;
                if (timer <= 0)
                {
                    canFire = true;
                    timer = reloadTime;
                }
            }
        }

        public void DrawTowerRange(SpriteBatch spriteBatch)
        {
            mouseState = Mouse.GetState();

            if (hitBox.Contains(mouseState.X, mouseState.Y))            
                isHovered = true;           
            else if (!hitBox.Contains(mouseState.X, mouseState.Y))
                isHovered = false;

            Vector2 rangeOrigin = new Vector2(TextureManager.towerRange.Width * 0.5f, TextureManager.towerRange.Height * 0.5f);
            float rangeScale = (range / TextureManager.towerRange.Width) * 2.0f;
 
            if (isHovered)
                spriteBatch.Draw(TextureManager.towerRange, position, null, Color.White, 0, rangeOrigin, rangeScale, spriteEffect, 1f);
        }

        public Color SetColor { set { color = value; } }
        public string Name() { return name; }
        public int Damage() { return damage; }
        public int Cost() { return cost; }
        public float Range() { return range; }
        public double ReloadTime() { return reloadTime; }
        public string Special() { return special; }
    }
}
