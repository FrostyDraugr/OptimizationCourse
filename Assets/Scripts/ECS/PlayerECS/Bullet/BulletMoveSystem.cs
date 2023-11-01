using AsteroidECS;
using CoreECS;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace BulletECS
{ 
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

            foreach (var (bulletTransform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<BulletMovement>().WithEntityAccess())
            {
                bulletTransform.ValueRW.Position.y += (gameManager.BulletSpeed * SystemAPI.Time.DeltaTime);

                //Add Entity to Destroy Buffer
                if (bulletTransform.ValueRW.Position.y < gameManager.ScreenSize.y + 1)
                    SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(entity);
            }
        }
    }
}