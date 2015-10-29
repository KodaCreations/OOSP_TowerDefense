using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class FrostOrb : Projectile
    {           
        ParticleEngine particleEngine;

        public FrostOrb(Vector2 position, Texture2D texture, Creep targetCreep, int damage)
            : base(position, texture, targetCreep, damage)
        {
            speed = 5.0f;
            color = Color.White;
            radius = 128;
            slowedTimer = 4.0f;
            slowedModifier = 0.5f;

            targetPosition = targetCreep.GetPosition();
            direction = targetCreep.GetPosition() - position;
            direction.Normalize();

            List<Texture2D> frost = new List<Texture2D>();
            frost.Add(TextureManager.p_FrostSmall);
            frost.Add(TextureManager.p_FrostLarge);
            particleEngine = new ParticleEngine(frost, new Vector2(800, 480));            
        }

        public void ParticleEffect()
        {
            particleEngine.EmitterLocation = new Vector2(position.X, position.Y);

            for (int i = 0; i < 1; i++)
            {
                particleEngine.particles.Add(particleEngine.GenerateNewParticle(1.0f, 20, Color.White, Color.CornflowerBlue, Color.Aqua));
            }

            for (int particle = 0; particle < particleEngine.particles.Count; particle++)
            {
                particleEngine.particles[particle].Update();
                if (particleEngine.particles[particle].TimeToLive <= 0)
                {
                    particleEngine.particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public override void Update(double deltaTime)
        {
            ParticleEffect();
            base.Update(deltaTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            particleEngine.Draw(spriteBatch);
            
        }

        
    }
}
