using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class TowerManager
    {
        GraphicsDevice graphics;
        List<Tower> towers = new List<Tower>();
        ProjectileManager projectileManager;
        CreepManager creepManager;
        UserInterface userInterface;
        RenderTarget2D backgroundLayer;
        MouseState mouseState;
        ParticleEngine particleEngine;

        public TowerManager(ref CreepManager creepManager, GraphicsDevice graphics)
        {
            this.graphics = graphics;
            backgroundLayer = new RenderTarget2D(graphics, 1600, 920);
            userInterface = new UserInterface(ref creepManager);

            this.creepManager = creepManager;
            projectileManager = new ProjectileManager(ref creepManager);

            List<Texture2D> smokeOpaque = new List<Texture2D>();
            smokeOpaque.Add(TextureManager.p_Smoke1_O);
            smokeOpaque.Add(TextureManager.p_Smoke2_O);
            smokeOpaque.Add(TextureManager.p_Smoke3_O);
            particleEngine = new ParticleEngine(smokeOpaque, new Vector2(800, 480));
        }

        bool CanPlace(Object o)
        {
            try
            {
                Color[] pixels = new Color[o.GetWidth() * o.GetHeight()];
                Color[] pixels2 = new Color[o.GetWidth() * o.GetHeight()];
                o.GetTexture().GetData<Color>(pixels2);
                TextureManager.pathTexture.GetData(0, o.GetHitBox(), pixels, 0, pixels.Length);

                for (int i = 0; i < pixels.Length; i++)
                {
                    if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                        return false;
                }
                backgroundLayer.GetData(0, o.GetHitBox(), pixels, 0, pixels.Length);
                for (int i = 0; i < pixels.Length; i++)
                {
                    if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                        return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void TowerAttack(double deltaTime)
        {
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].Update(deltaTime);

                if (towers[i].canFire)
                {
                    for (int j = 0; j < creepManager.creepWave.Count; j++)
                    {
                        if (Vector2.Distance(creepManager.creepWave[j].GetPosition(), towers[i].GetPosition()) <= towers[i].Range())
                        {
                            if (towers[i] is CannonTower)
                            {
                                CannonBall c = new CannonBall(towers[i].GetPosition(), TextureManager.projectileTexture, creepManager.creepWave[j], towers[i].Damage());
                                projectileManager.projectiles.Add(c);

                                particleEngine.EmitterLocation = new Vector2(towers[i].GetPosition().X, towers[i].GetPosition().Y);
                                for (int z = 0; z < 3; z++)
                                {
                                    particleEngine.particles.Add(particleEngine.GenerateNewParticle(1.0f, 20, Color.White, Color.White, Color.White));
                                }

                                towers[i].canFire = false;
                                break;
                            }

                            if (towers[i] is MagicTower)
                            {
                                FrostOrb f = new FrostOrb(towers[i].GetPosition(), TextureManager.frostOrb, creepManager.creepWave[j], towers[i].Damage());
                                projectileManager.projectiles.Add(f);
                                towers[i].canFire = false;
                                break;
                            }
                            
                        }
                    }
                }
            }
        }

        private void DrawRenderTargetLayer(GraphicsDevice device)
        {
            SpriteBatch spriteBatch = new SpriteBatch(device);
            device.SetRenderTarget(backgroundLayer);
            device.Clear(Color.Transparent);

            spriteBatch.Begin();

            foreach (Tower t in towers)
            {
                t.Draw(spriteBatch);
            }

            spriteBatch.End();
            device.SetRenderTarget(null);
        }

        public void Update(double deltaTime)
        {
            mouseState = Mouse.GetState();
            projectileManager.Update(deltaTime);
            userInterface.Update();
            particleEngine.Update();
            TowerAttack(deltaTime);

            if (mouseState.LeftButton == ButtonState.Pressed && userInterface.canPlaceCannonTower == true)
            {
                DrawRenderTargetLayer(graphics);
                CannonTower t = new CannonTower(TextureManager.towerTexture, new Vector2(mouseState.X, mouseState.Y));
                if (CanPlace(t) && UserInterface.gold >= t.Cost())
                {
                    userInterface.canPlaceCannonTower = false;
                    towers.Add(t);
                    UserInterface.gold -= t.Cost();
                }
            }
            if (mouseState.LeftButton == ButtonState.Pressed && userInterface.canPlaceMagicTower == true)
            {
                DrawRenderTargetLayer(graphics);
                MagicTower t = new MagicTower(TextureManager.towerTexture, new Vector2(mouseState.X, mouseState.Y));
                if (CanPlace(t) && UserInterface.gold >= t.Cost())
                {
                    userInterface.canPlaceMagicTower = false;
                    towers.Add(t);
                    UserInterface.gold -= t.Cost();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (userInterface.canPlaceCannonTower || userInterface.canPlaceMagicTower)
            {
                Tower tower = new Tower(TextureManager.towerTexture, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

                if (CanPlace(tower))
                    tower.SetColor = Color.Green;
                else
                    tower.SetColor = Color.Red;

                tower.Draw(spriteBatch);
            }

            foreach (Tower t in towers)
            {
                t.Draw(spriteBatch);
                t.DrawTowerRange(spriteBatch);
            }

            
            creepManager.Draw(spriteBatch);
            projectileManager.Draw(spriteBatch);
            particleEngine.Draw(spriteBatch);
            userInterface.Draw(spriteBatch);
        }
    }
}
