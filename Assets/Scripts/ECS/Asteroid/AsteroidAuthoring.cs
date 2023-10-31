using UnityEngine;
using Unity.Entities;

namespace AsteroidECS
{
    public class AsteroidAuthoring : MonoBehaviour
    {
        class Baker : Baker<AsteroidAuthoring>
        {
            public override void Bake(AsteroidAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new Asteroid
                {
                    Entity = entity
                });

                AddComponent(entity, new AsteroidMovement
                {
                    Entity = entity
                });
            }
        }
    }

    public struct Asteroid : IComponentData
    {
        public Entity Entity;
    }

    public struct AsteroidMovement : IComponentData
    {
        public Entity Entity;
    }

}