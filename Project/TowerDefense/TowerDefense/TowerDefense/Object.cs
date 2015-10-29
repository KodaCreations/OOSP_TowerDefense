using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    abstract class Object
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 origin;
        protected Rectangle hitBox;
        protected Color color;
        protected float rotation;
        protected float scale;
        protected SpriteEffects spriteEffect;

        public Object(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
            origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            scale = 1.0f;
            spriteEffect = SpriteEffects.None;
            color = Color.White;
            hitBox = new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, texture.Width, texture.Height); 
        }

        public virtual void Update(double deltaTime)
        { 
            hitBox = new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, texture.Width, texture.Height); 
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, spriteEffect, 0);
        }

        public Rectangle GetHitBox() { return hitBox; }
        public Texture2D GetTexture() { return texture; }
        public Vector2 GetPosition() { return position; }
        public Vector2 GetOrigin() { return origin; }
        public int GetWidth() { return texture.Width; }
        public int GetHeight() { return texture.Height; }
    }
}
