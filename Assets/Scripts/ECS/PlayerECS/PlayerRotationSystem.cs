using CoreECS;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace PlayerECS
{
    public partial struct PlayerRotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerRotation>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var gameManager = SystemAPI.GetSingleton<GameManagerECS>();

            var mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            //Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
            //transform.rotation = rot;
            //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        }
    }
}