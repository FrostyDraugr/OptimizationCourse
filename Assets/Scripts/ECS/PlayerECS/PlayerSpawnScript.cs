using CoreECS;
using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using System.Linq.Expressions;
using Unity.Mathematics;

namespace PlayerECS
{
    public partial struct PlayerSpawnScript : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameManagerECS>();

            state.RequireForUpdate<CoreECS.Execute.PlayerSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            GameManagerECS gameManager = SystemAPI.GetSingleton<GameManagerECS>();

            Entity player = state.EntityManager.Instantiate(gameManager.PlayerPrefab);

            state.EntityManager.SetComponentData(player, new LocalTransform
            {
                Position = float3.zero,
                Scale = 1f,
                Rotation = quaternion.identity
            });
        }
    }
}