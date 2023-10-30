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

                AddComponent<Player>(entity);
                AddComponent<PlayerMovement>(entity);
            }
        }
    }

    public struct Player : IComponentData
    {

    }
    public struct PlayerMovement : IComponentData
    {

    }
}
