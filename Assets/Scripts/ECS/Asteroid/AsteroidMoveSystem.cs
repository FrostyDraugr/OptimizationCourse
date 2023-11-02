using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using CoreECS;
using UnityEngine;
using BulletECS;

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
            var killY = -gameManager.ScreenSize.y - 1f;


            foreach (var (asteroidTransform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<AsteroidMovement>().WithEntityAccess())
            {
                bool destroyed = false; 
                asteroidTransform.ValueRW.Position.y += -(gameManager.AsteroidSpeed * SystemAPI.Time.DeltaTime);
                
                //I don't like getting a new Query list every single time, I would prefer to just have one list updated once per frame.
                //But I got a compile error stating that Query requests are only allowed in foreach loops, sure
                foreach (var (bulletTransform, bulletEntity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<BulletMovement>().WithEntityAccess())
                {
                    if (math.distance(bulletTransform.ValueRO.Position, asteroidTransform.ValueRO.Position) < 1.0f)
                    {
                        destroyed = true;
                        SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(bulletEntity);
                        SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(entity);
                        break;
                    }
                }

                if (destroyed)
                    continue;
                //Add Entity to Destroy Buffer
                if (asteroidTransform.ValueRO.Position.y < killY)
                    SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(entity);

                if(gameManager.PlayerDeath)
                    if (math.distance(playerTransform.ValueRO.Position, asteroidTransform.ValueRO.Position) < 0.5f)
                    {
                        SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(player.Entity);
                    }
            }
        }
    }
}
