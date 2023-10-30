using Unity.Entities;
using UnityEngine;

namespace PlayerECS
{
    public class PlayerAuthoring:MonoBehaviour
    {
        public Quaternion Rotation;

        class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake (PlayerAuthoring playerAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<Player>(entity);
                AddComponent<PlayerMovement>(entity);
                AddComponent(entity, new PlayerRotation
                    {
                    Rotation = playerAuthoring.Rotation
                });
            }
        }
    }

    public struct Player : IComponentData
    {

    }
    public struct PlayerMovement : IComponentData
    {

    }

    public struct PlayerRotation : IComponentData
    {
        public Quaternion Rotation;
    }
}
