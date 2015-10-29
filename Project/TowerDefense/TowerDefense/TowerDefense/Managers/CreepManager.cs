using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    class CreepManager
    {
        enum CreepWave { Countdown, Militia, Warrior, Knight };
        CreepWave currentCreepWave = CreepWave.Countdown;

        public List<Creep> creepWave = new List<Creep>();
        SimplePath path;

        public int life;
        private bool firstWave, firstWaveFinished;
        private bool secondWave, secondWaveFinished;
        private bool thirdWave, thirdWaveFinished;
        private bool waveCountdown;
        private bool waveEnded;
        private bool gameWon;
        private int waveCount = 0;
        private double creepSpawnInterval = 1.0f;
        private double creepSpawnIntervalReset;
        private double waveSpawnTimer = 5.0f;
        private double waveSpawnTimerReset;


        public CreepManager(ref SimplePath path)
        {
            this.path = path;
            creepSpawnIntervalReset = creepSpawnInterval;
            waveSpawnTimerReset = waveSpawnTimer;
            life = 1;
        }

        private void CreateWave(double deltaTime, Creep creep, Creep boss)
        {
            if(waveCount <=9)
            {
                creepSpawnInterval -= deltaTime;
                if (waveCount <= 7 && creepSpawnInterval <= 0)
                {
                    creepWave.Add(creep);
                    creepSpawnInterval = creepSpawnIntervalReset;
                    waveCount += 1;
                }
                else if (waveCount >= 8 && creepSpawnInterval <= 0)
                {
                    creepWave.Add(boss);
                    creepSpawnInterval = creepSpawnIntervalReset;
                    waveCount += 1;
                }                
            }                

            if (waveCount == 10 && creepWave.Count == 0)
            {
                currentCreepWave = CreepWave.Countdown;
                waveCount = 0;
                waveEnded = true;
            }
        }

        private void WaveCountdown(double deltaTime)
        {
            waveCountdown = true;
            waveSpawnTimer -= deltaTime;
            if (waveSpawnTimer <= 0)
            {
                if (!firstWaveFinished)
                {
                    currentCreepWave = CreepWave.Militia;
                    firstWave = true;
                    waveCountdown = false;
                }
                else if (!secondWaveFinished)
                {
                    currentCreepWave = CreepWave.Warrior;
                    secondWave = true;
                    waveCountdown = false;
                }
                else if (!thirdWaveFinished)
                {
                    currentCreepWave = CreepWave.Knight;
                    thirdWave = true;
                    waveCountdown = false;
                }
                waveSpawnTimer = waveSpawnTimerReset;
            }
        }

        public void Update(double deltaTime)
        {
            switch (currentCreepWave)
            {
                case CreepWave.Countdown:
                    WaveCountdown(deltaTime);
                    break;

                case CreepWave.Militia:
                    Militia c1 = new Militia(Vector2.Zero, TextureManager.creepTexture);
                    Warrior b1 = new Warrior(Vector2.Zero, TextureManager.creepTexture);
                    CreateWave(deltaTime, c1, b1);
                    if(waveEnded == true)
                    {
                        firstWave = false;
                        firstWaveFinished = true;
                        waveEnded = false;
                    }
                    break;

                case CreepWave.Warrior:
                    Warrior c2 = new Warrior(Vector2.Zero, TextureManager.creepTexture);
                    Knight b2 = new Knight(Vector2.Zero, TextureManager.creepTexture);
                    CreateWave(deltaTime, c2, b2);
                    if(waveEnded == true)
                    {
                        secondWave = false;
                        secondWaveFinished = true;
                        waveEnded = false;
                    }
                    break;

                case CreepWave.Knight:
                    Knight c3 = new Knight(Vector2.Zero, TextureManager.creepTexture);
                    Lord b3 = new Lord(Vector2.Zero, TextureManager.creepTexture);
                    CreateWave(deltaTime, c3, b3);
                    if (waveEnded == true)
                    {
                        thirdWave = false;
                        thirdWaveFinished = true;
                        waveEnded = false;
                        gameWon = true;
                    }
                    break;
            }            

            foreach (Creep c in creepWave)
            {
                c.Update(deltaTime, path);

                if (c.texPos >= path.endT)
                {
                    creepWave.Remove(c);
                    life--;
                    break;
                }
            }
            creepWave.RemoveAll(x => x.isDead);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Creep c in creepWave)
            {
                c.Draw(spriteBatch);
            }
        }

        public bool IsFirstWave() { return firstWave; }
        public bool IsSecondWave() { return secondWave; }
        public bool IsThirdWave() { return thirdWave; }
        public bool IsGameWon() { return gameWon; }
        public bool IsWaveCountdown() { return waveCountdown; }
        public double GetWaveTimer() { return waveSpawnTimer; }
    }
}
