using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using CoreECS;
using System.Diagnostics;
using UnityEngine;

namespace AsteroidECS
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct AsteroidMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameManagerECS>();
            state.RequireForUpdate<PlayerECS.Player>();
            state.RequireForUpdate<CoreECS.Execute.AsteroidMover>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var gameManager = SystemAPI.GetSingleton<GameManagerECS>();
            var player = SystemAPI.GetSingleton<PlayerECS.Player>();
            var playerTransform = SystemAPI.GetComponentRW<LocalTransform>(player.Entity);

            foreach (var (asteroidTransform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<AsteroidMovement>().WithEntityAccess())
            {
                asteroidTransform.ValueRW.Position.y += -(gameManager.AsteroidSpeed * SystemAPI.Time.DeltaTime);

                //Add Entity to Destroy Buffer
                if (asteroidTransform.ValueRW.Position.y < -gameManager.ScreenSize.y - 1)
                    SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(entity);

                if (math.distance(playerTransform.ValueRO.Position, asteroidTransform.ValueRO.Position) < 1.0f)
                {
                    SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(player.Entity);
                }

            }
        }
    }
}
