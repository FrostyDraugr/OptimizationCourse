using Unity.Entities;
using UnityEngine;

namespace BulletECS
{
    public class BulletAuthoring : MonoBehaviour
    {
            class Baker : Baker<BulletAuthoring>
            {
            public override void Bake(BulletAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new Bullet
                {
                    Entity = entity
                });

                AddComponent(entity, new BulletMovement
                {
                    Entity = entity
                });
            }
        }
    }

    //It's unnecessary to have two different tags since the structs contain the same info.
    public struct Bullet : IComponentData
    {
        public Entity Entity;
    }
    public struct BulletMovement : IComponentData
    {
        public Entity Entity;
    }
}

