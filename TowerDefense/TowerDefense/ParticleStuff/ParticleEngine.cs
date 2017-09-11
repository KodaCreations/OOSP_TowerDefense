using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        public List<Particle> particles;
        private List<Texture2D> textures;
        Color color;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        public void Update()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();

                if (particles[i].TimeToLive <= 0)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }

        public Particle GenerateNewParticle(float sizeModifier, int baseTimeToLive, Color firstColor, Color secondColor, Color thirdColor)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(1f * (float)(random.NextDouble() * 2 - 1), 1f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            float size = (float)random.NextDouble() * sizeModifier;
            int ttl = baseTimeToLive + random.Next(40);

            int pickColor = random.Next(3);
            if (pickColor == 0)
                color = firstColor;
            else if (pickColor == 1)
                color = secondColor;
            else if (pickColor == 2)
                color = thirdColor;

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }

        }
    }
}
