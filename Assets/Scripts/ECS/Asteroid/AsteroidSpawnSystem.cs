using CoreECS;
using System.Linq.Expressions;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.UIElements;

namespace AsteroidECS
{
    public partial struct AsteroidSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameManagerECS>();

            state.RequireForUpdate<ExecuteAsteroidSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            GameManagerECS gameManager = SystemAPI.GetSingleton<GameManagerECS>();

            float scale = gameManager.AsteroidSize;

            for(int i = 0; i <4; i++) {
                Entity asteroid = state.EntityManager.Instantiate(gameManager.AsteroidPrefab);

                state.EntityManager.SetComponentData(asteroid, new LocalTransform
                {
                    Position = new float3(0, 0, 0),
                    Scale = scale,
                    Rotation = quaternion.identity
                });
            }
            
        }
    }
}
