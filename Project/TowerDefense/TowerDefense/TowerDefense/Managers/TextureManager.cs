using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    static class TextureManager
    {
        public static Texture2D towerTexture { get; private set; }
        public static Texture2D projectileTexture { get; private set; }
        public static Texture2D creepTexture { get; private set; }
        public static Texture2D background { get; private set; }
        public static Texture2D pathTexture { get; private set; }
        public static Texture2D frostOrb { get; private set; }
        public static Texture2D towerRange { get; private set; }

        // Particle Textures
        public static Texture2D p_Smoke1_S { get; private set; }
        public static Texture2D p_Smoke1_O { get; private set; }
        public static Texture2D p_Smoke2_S { get; private set; }
        public static Texture2D p_Smoke2_O { get; private set; }
        public static Texture2D p_Smoke3_S { get; private set; }
        public static Texture2D p_Smoke3_O { get; private set; }
        public static Texture2D p_FrostSmall { get; private set; }
        public static Texture2D p_FrostLarge { get; private set; }
        public static Texture2D p_Fragment1 { get; private set; }
        public static Texture2D p_Fragment2 { get; private set; }

        // Fonts
        public static SpriteFont tooltipFont { get; private set; }
        public static SpriteFont hudFont { get; private set; }
        public static SpriteFont timerFont { get; private set; }
        public static SpriteFont nameFont { get; private set; }



        public static void LoadContent(ContentManager content)
        {
            towerTexture = content.Load<Texture2D>("Textures/BaseTower");
            projectileTexture = content.Load<Texture2D>("Textures/BaseProjectile");
            creepTexture = content.Load<Texture2D>("Textures/BaseCreep");
            frostOrb = content.Load<Texture2D>("Textures/FrostOrb");
            towerRange = content.Load<Texture2D>("Textures/TowerRange");
            background = content.Load<Texture2D>("Background");
            pathTexture = content.Load<Texture2D>("Path");            

            // Particle Textures
            p_Smoke1_O = content.Load<Texture2D>("Particles/Smoke/SmokeParticle1_Opaque"); 
            p_Smoke1_S = content.Load<Texture2D>("Particles/Smoke/SmokeParticle1_Solid"); 
            p_Smoke2_O = content.Load<Texture2D>("Particles/Smoke/SmokeParticle2_Opaque");
            p_Smoke2_S = content.Load<Texture2D>("Particles/Smoke/SmokeParticle2_Solid"); 
            p_Smoke3_O = content.Load<Texture2D>("Particles/Smoke/SmokeParticle3_Opaque");
            p_Smoke3_S = content.Load<Texture2D>("Particles/Smoke/SmokeParticle3_Solid");
            p_FrostSmall = content.Load<Texture2D>("Particles/FrostParticleSmall");
            p_FrostLarge = content.Load<Texture2D>("Particles/FrostParticleLarge");
            p_Fragment1 = content.Load<Texture2D>("Particles/CannonFragment1");
            p_Fragment2 = content.Load<Texture2D>("Particles/CannonFragment2"); 
               
            // Fonts
            tooltipFont = content.Load<SpriteFont>("tooltipFont");
            hudFont = content.Load<SpriteFont>("hudFont");
            timerFont = content.Load<SpriteFont>("timerFont");
            nameFont = content.Load<SpriteFont>("nameFont");
        }
    }
}
