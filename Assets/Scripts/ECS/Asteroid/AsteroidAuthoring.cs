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
                AddComponent<Asteroid>(entity);
            }
        }
    }

    public struct Asteroid : IComponentData
    {

    }

}