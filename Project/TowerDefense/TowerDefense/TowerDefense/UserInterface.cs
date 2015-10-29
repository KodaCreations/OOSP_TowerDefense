using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class UserInterface
    {
        MouseState mouseState, oldmouseState;
        Vector2 tooltipPosition, cannonPosition, magicPosition, goldPosition, livesPosition, timerPosition;
        CannonTower cannonTower;
        MagicTower magicTower;
        CreepManager creepManager;
        public bool canPlaceCannonTower = false;
        public bool canPlaceMagicTower = false;
        internal static int gold;

        public UserInterface(ref CreepManager creepManager)
        {
            this.creepManager = creepManager;
            gold = 50;

            tooltipPosition = new Vector2(1260, 642);
            cannonPosition = new Vector2(1318, 300);
            magicPosition = new Vector2(1482, 300);
            goldPosition = new Vector2(1260, 145);
            livesPosition = new Vector2(1420, 145);
            timerPosition = new Vector2(1260, 40);

            cannonTower = new CannonTower(TextureManager.towerTexture, cannonPosition);
            magicTower = new MagicTower(TextureManager.towerTexture, magicPosition);

        }

        public void Update()
        {
            oldmouseState = mouseState;
            mouseState = Mouse.GetState();

            // Pick up tower
            if (cannonTower.GetHitBox().Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && oldmouseState.LeftButton == ButtonState.Released)
                canPlaceCannonTower = true;
            if (magicTower.GetHitBox().Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && oldmouseState.LeftButton == ButtonState.Released)
                canPlaceMagicTower = true;

            // Release tower
            if (canPlaceCannonTower && mouseState.RightButton == ButtonState.Pressed && oldmouseState.RightButton == ButtonState.Released)
                canPlaceCannonTower = false;
            if (canPlaceMagicTower && mouseState.RightButton == ButtonState.Pressed && oldmouseState.RightButton == ButtonState.Released)
                canPlaceMagicTower = false;

        }

        private void DrawTooltip(SpriteBatch spriteBatch, Tower tower)
        {
            spriteBatch.DrawString(TextureManager.tooltipFont,
                tower.Name() +
                "\nDamage: " + tower.Damage() +
                "\nPrice: " + tower.Cost() +
                "\nRange: " + tower.Range() +
                "\nReloadtime: " + tower.ReloadTime() + "s" +
                "\nSpecial: " + tower.Special(),
                tooltipPosition, Color.Black);
        }

        private void DrawWaveName(SpriteBatch spriteBatch, string waveName)
        {
            timerPosition = new Vector2(1400 - TextureManager.timerFont.MeasureString(waveName).X * 0.5f, 40);
            spriteBatch.DrawString(TextureManager.timerFont, waveName, timerPosition, Color.Black);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            cannonTower.Draw(spriteBatch);
            magicTower.Draw(spriteBatch);

            // Displays wave name or countdown to next wave
            if (creepManager.IsGameWon())
                DrawWaveName(spriteBatch, "Game won!");
            else if (creepManager.life <= 0)
                DrawWaveName(spriteBatch, "Game lost!");
            else if (creepManager.IsWaveCountdown())
            {
                timerPosition = new Vector2(1400 - TextureManager.timerFont.MeasureString("Next wave in: " + (int)(creepManager.GetWaveTimer() + 1)).X * 0.5f, 40);
                spriteBatch.DrawString(TextureManager.timerFont, "Next wave in: " + (int)(creepManager.GetWaveTimer() + 1), timerPosition, Color.Black);
            }
            else if (creepManager.IsFirstWave())
                DrawWaveName(spriteBatch, "First wave!");
            else if (creepManager.IsSecondWave())
                DrawWaveName(spriteBatch, "Second wave!");
            else if (creepManager.IsThirdWave())
                DrawWaveName(spriteBatch, "Final wave!");


            // Displays gold, lives and tower names
            spriteBatch.DrawString(TextureManager.hudFont, "Gold: " + gold, goldPosition, Color.Black);
            spriteBatch.DrawString(TextureManager.hudFont, "Lives: " + creepManager.life, livesPosition, Color.Black);
            spriteBatch.DrawString(TextureManager.nameFont, cannonTower.Name(), new Vector2(cannonPosition.X - TextureManager.nameFont.MeasureString(cannonTower.Name()).X * 0.5f, cannonPosition.Y + 32), Color.Black);
            spriteBatch.DrawString(TextureManager.nameFont, magicTower.Name(), new Vector2(magicPosition.X - TextureManager.nameFont.MeasureString(magicTower.Name()).X * 0.5f, magicPosition.Y + 32), Color.Black);

            // Displays tower tooltip on hover
            if (cannonTower.GetHitBox().Contains(mouseState.X, mouseState.Y) && canPlaceCannonTower == false)
                DrawTooltip(spriteBatch, cannonTower);
            else if (magicTower.GetHitBox().Contains(mouseState.X, mouseState.Y) && canPlaceMagicTower == false)
                DrawTooltip(spriteBatch, magicTower);
        }
    }
}
