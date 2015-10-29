using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class ProjectileManager
    {
        public List<Projectile> projectiles = new List<Projectile>();

        protected ParticleEngine hitFrostParticleEngine;
        protected ParticleEngine hitCannonParticleEngine;

        private CreepManager creepManager;


        public ProjectileManager(ref CreepManager creepManager)
        {
            this.creepManager = creepManager;

            List<Texture2D> hitFrostParticles = new List<Texture2D>();
            hitFrostParticles.Add(TextureManager.p_FrostSmall);
            hitFrostParticles.Add(TextureManager.p_FrostLarge);
            hitFrostParticleEngine = new ParticleEngine(hitFrostParticles, new Vector2(800, 480));

            List<Texture2D> hitCannonParticles = new List<Texture2D>();
            hitCannonParticles.Add(TextureManager.p_Fragment1);
            hitCannonParticles.Add(TextureManager.p_Fragment2);
            hitCannonParticleEngine = new ParticleEngine(hitCannonParticles, new Vector2(800, 480));
        }

        private void OnHitEffect(ParticleEngine particleEngine, Vector2 position, int nrOfParticles, float scale, int timeToLive, Color color1, Color color2, Color color3)
        {
            particleEngine.EmitterLocation = new Vector2(position.X, position.Y);

            for (int i = 0; i < nrOfParticles; i++)
            {
                particleEngine.particles.Add(particleEngine.GenerateNewParticle(scale, timeToLive, color1, color2, color3));
            }
        }

        public void Update(double deltaTime)
        {
            foreach (Projectile p in projectiles)
            {
                p.Update(deltaTime);
                if (p.TargetHit())
                {
                    if (p is FrostOrb)
                    {
                        OnHitEffect(hitFrostParticleEngine, p.GetPosition(), 15, 1.0f, 40, Color.White, Color.CornflowerBlue, Color.Aqua);
                        foreach (Creep c in creepManager.creepWave)
                        {                            
                            if(Vector2.Distance(p.GetPosition(), c.GetPosition()) <= p.GetRadius())
                            {
                                c.SetSlowedTimer = p.GetSlowedTimer();
                                c.SetSlowedModifier = p.GetSlowedModifier();
                                c.TakeDamage(p.GetDamage());
                            }
                        }
                    }
                    else if (p is CannonBall)
                    {
                        OnHitEffect(hitCannonParticleEngine, p.GetPosition(), 5, 0.5f, 0, Color.White, Color.White, Color.White);
                    }
                    
                }
            }
            projectiles.RemoveAll(x => x.TargetHit());
            hitFrostParticleEngine.Update();
            hitCannonParticleEngine.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Projectile p in projectiles)
            {
                p.Draw(spriteBatch);
            }
            hitFrostParticleEngine.Draw(spriteBatch);
            hitCannonParticleEngine.Draw(spriteBatch);
        }
    }
}