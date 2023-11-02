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
using System;
using UnityEditor.Experimental.GraphView;

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

            foreach (var (bulletTransform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<BulletMovement>().WithEntityAccess())
            {
                //var oldPos = bulletTransform.ValueRO.Position;
                //var offSet = new float3(0, SystemAPI.Time.DeltaTime * gameManager.BulletSpeed,0);

                //bulletTransform.ValueRW.Position = oldPos + offSet;

                //bulletTransform.ValueRW.Position.y += SystemAPI.Time.DeltaTime * gameManager.BulletSpeed;

                //SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(entity);

                //var systemBase = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

                if (bulletTransform.ValueRW.Position.y > killY)
                {
                    SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(entity);
                    continue;
                }

            }
            var job = new BulletMovementJob
            {
                YMove = SystemAPI.Time.DeltaTime * gameManager.BulletSpeed,
                //KillY = killY,
                //Bullet = entity,
                //Buffer = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged),
            };
            job.ScheduleParallel();

        }

        [WithAll(typeof(BulletMovement))]
        [BurstCompile]
        public partial struct BulletMovementJob : IJobEntity
        {
            public float YMove;
            // float KillY;
            //public Entity Bullet;
            //public EntityCommandBuffer Buffer;
            //public RefRW<LocalTransform> transform;

            public void Execute(ref LocalTransform transform)
            {
                transform.Position.y += YMove;
            }
        }

    }
}