using AsteroidECS;
using CoreECS;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System.ComponentModel;
using Unity.Collections;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEditor.SceneManagement;
using Unity.Burst.Intrinsics;

namespace BulletECS
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct BulletMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameManagerECS>();
            state.RequireForUpdate<PlayerECS.Player>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var gameManager = SystemAPI.GetSingleton<GameManagerECS>();
            var killY = gameManager.ScreenSize.y + 1;

            foreach (var entity in SystemAPI.Query<BulletMovement>().WithEntityAccess())
            {
                //var oldPos = bulletTransform.ValueRO.Position;
                //var offSet = new float3(0, SystemAPI.Time.DeltaTime * gameManager.BulletSpeed,0);

                //bulletTransform.ValueRW.Position = oldPos + offSet;

                //bulletTransform.ValueRW.Position.y += SystemAPI.Time.DeltaTime * gameManager.BulletSpeed;

                //SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(entity);

                //var systemBase = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

                var job = new BulletMovementJob
                {
                    YMove = SystemAPI.Time.DeltaTime * gameManager.BulletSpeed,
                    KillY = killY,
                    Bullet = entity,
                    Buffer = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged)
                };

                job.Schedule();
            }
        }

        [BurstCompile]
        public partial struct BulletMovementJob : IJobEntity
        {
            public float YMove;
            public float KillY;
            public Entity Bullet;
            public EntityCommandBuffer Buffer;
            

            public void Execute(ref LocalTransform transform)
            {
                transform.Position.y += YMove;

                if (transform.Position.y > KillY)
                {
                    Buffer.DestroyEntity(Bullet);
                }
            }
        }

    }
}