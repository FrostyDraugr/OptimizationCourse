using Unity.Entities;
using UnityEngine;

namespace PlayerECS
{
    public class PlayerAuthoring:MonoBehaviour
    {

        class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake (PlayerAuthoring playerAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new Player
                {
                    Entity = entity
                });
                AddComponent(entity, new PlayerMovement
                {
                    Entity = entity
                });
            }
        }
    }

    public struct Player : IComponentData
    {
        public Entity Entity;
    }
    public struct PlayerMovement : IComponentData
    {
        public Entity Entity;
    }
}
