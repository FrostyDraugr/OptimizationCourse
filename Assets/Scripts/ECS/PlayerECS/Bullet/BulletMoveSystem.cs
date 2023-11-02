using AsteroidECS;
using CoreECS;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

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
            var killY = gameManager.ScreenSize.y + 1f;

            foreach (var (bulletTransform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<BulletMovement>().WithEntityAccess())
            {
                //var oldPos = bulletTransform.ValueRO.Position;
                //var offSet = new float3(0, SystemAPI.Time.DeltaTime * gameManager.BulletSpeed,0);

                //bulletTransform.ValueRW.Position = oldPos + offSet;

                bulletTransform.ValueRW.Position.y += SystemAPI.Time.DeltaTime * gameManager.BulletSpeed;

                //Add Entity to Destroy Buffer
                if (bulletTransform.ValueRW.Position.y > killY)
                    SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(entity);
            }
        }
    }
}