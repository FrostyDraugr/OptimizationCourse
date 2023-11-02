using Unity.Burst;
using Unity.Entities;
using CoreECS;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

namespace BulletECS
{
    public partial struct BulletSpawner : ISystem
    {
        private double _lastSpawned;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerECS.Player>();
            state.RequireForUpdate<GameManagerECS>();
            state.RequireForUpdate<CoreECS.Execute.BulletSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            GameManagerECS gameManager = SystemAPI.GetSingleton<GameManagerECS>();
            var player = SystemAPI.GetSingleton<PlayerECS.Player>();
            var playerTransform = SystemAPI.GetComponentRW<LocalTransform>(player.Entity);
            var yPos = playerTransform.ValueRO.Position.y + 1f;

            if (SystemAPI.Time.ElapsedTime > _lastSpawned + gameManager.FireCoolDown)
            {
                if (Input.GetButton("Fire1"))
                {
                    Entity bullet = state.EntityManager.Instantiate(gameManager.BulletPrefab);

                    state.EntityManager.SetComponentData(bullet, new LocalTransform
                    {
                        Position = new float3
                        {
                            x = playerTransform.ValueRO.Position.x,
                            y = yPos,
                            z = 0
                        },
                        Scale = 1f,
                        Rotation = quaternion.identity
                    }
                    );
                    _lastSpawned = SystemAPI.Time.ElapsedTime;
                }
            }
        }
    }
}

