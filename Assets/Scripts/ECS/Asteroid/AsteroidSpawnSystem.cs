using CoreECS;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace AsteroidECS
{
    public partial struct AsteroidSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameManagerECS>();

            state.RequireForUpdate<CoreECS.Execute.AsteroidSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //TempRunOnce
            state.Enabled = false;

            GameManagerECS gameManager = SystemAPI.GetSingleton<GameManagerECS>();

            float scale = gameManager.AsteroidSize;

            for(int i = 0; i <4; i++) {
                Entity asteroid = state.EntityManager.Instantiate(gameManager.AsteroidPrefab);

                state.EntityManager.SetComponentData(asteroid, new LocalTransform
                {
                    Position = new float3(i+5, i+5, 0),
                    Scale = scale,
                    Rotation = quaternion.identity
                });
            }
            
        }
    }
}
