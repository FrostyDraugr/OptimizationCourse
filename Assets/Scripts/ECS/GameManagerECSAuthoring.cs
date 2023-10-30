using Unity.Entities;
using UnityEngine;

namespace CoreECS
{
    public class GameManagerECSAuthoring : MonoBehaviour
    {
        public float PlayerSpeed;
        public GameObject PlayerPrefab;
        public GameObject AsteroidPrefab;

        class Baker:Baker<GameManagerECSAuthoring>
        {
            public override void Bake(GameManagerECSAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new GameManagerECS
                {
                    PlayerSpeed = authoring.PlayerSpeed,
                    PlayerPrefab = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic),
                    AsteroidPrefab = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic),
                });
            }
        }
    }

    public struct GameManagerECS :IComponentData
    {
        public float PlayerSpeed;
        public Entity PlayerPrefab;
        public Entity AsteroidPrefab;
    }
}
