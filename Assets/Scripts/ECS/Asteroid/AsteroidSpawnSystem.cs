using CoreECS;
using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace AsteroidECS
{
    public partial struct AsteroidSpawnSystem : ISystem
    {
        private double _lastSpawned;
        private int _wave;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _wave = 1;
            state.RequireForUpdate<GameManagerECS>();
            state.RequireForUpdate<CoreECS.Execute.AsteroidSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            GameManagerECS gameManager = SystemAPI.GetSingleton<GameManagerECS>();

            if (SystemAPI.Time.ElapsedTime > (_lastSpawned + gameManager.AsteroidSpawnPause))
            {
            //What is faster, reading the variable from the gameManager or saving it as a temp instance for the duration of the loop?
            float scale = gameManager.AsteroidSize;

            int spawnNum = gameManager.AsteroidCount * (int)(gameManager.SpawnCycleModifier * _wave);

            //Could randomize a random seed since this code only runs once per spawn
            var rand = new Random(1337);

            float xField = gameManager.ScreenSize.x * 2;

                for(int i = 0; i < spawnNum; i++)
                {
                    Entity asteroid = state.EntityManager.Instantiate(gameManager.AsteroidPrefab);

                    state.EntityManager.SetComponentData(asteroid, new LocalTransform
                    {
                        Position = new float3
                        {
                            x = 17 - (rand.NextFloat() * xField),
                            y = gameManager.AsteroidYOffset + (gameManager.AsteroidSpawnBoxY * rand.NextFloat()),
                            z = 0
                        },
                    Scale = scale,
                    Rotation = quaternion.identity
                    });
                }
            _wave++;
            _lastSpawned = SystemAPI.Time.ElapsedTime;

            }
        }
    }
}
