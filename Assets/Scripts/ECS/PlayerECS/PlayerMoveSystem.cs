using UnityEngine;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using CoreECS;

namespace PlayerECS
{
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerMovement>();

            state.RequireForUpdate<CoreECS.Execute.PlayerMovement>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var gameManager = SystemAPI.GetSingleton<GameManagerECS>();

            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            var input = gameManager.PlayerSpeed * SystemAPI.Time.DeltaTime * new float3(horizontal, vertical, 0);

            if (input.Equals(float3.zero))
            { return; }

            var player = SystemAPI.GetSingleton<PlayerMovement>();
            var playerTransform = SystemAPI.GetComponentRW<LocalTransform>(player.Entity);

            var newPos = playerTransform.ValueRO.Position;

            var border = gameManager.ScreenSize;

            if (newPos.x + input.x < -border.x || newPos.x + input.x > border.x)
            {
                input.x *= 0;
            }

            if (newPos.y + input.y < -border.y || newPos.y + input.y > border.y)
            {
                input.y *= 0;
            }

            newPos += input;

            playerTransform.ValueRW.Position = newPos;


        }
    }

}
