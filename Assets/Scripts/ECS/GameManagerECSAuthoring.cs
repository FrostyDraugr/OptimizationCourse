using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace CoreECS
{
    public class GameManagerECSAuthoring : MonoBehaviour
    {
        public float PlayerSpeed;
        public float BulletSpeed;
        public float AsteroidSpeed;
        public float AsteroidSize;
        public float2 ScreenSize;
        public GameObject PlayerPrefab;
        public GameObject AsteroidPrefab;
        public GameObject BulletPrefab;


        class Baker :Baker<GameManagerECSAuthoring>
        {
            public override void Bake(GameManagerECSAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new GameManagerECS
                {
                    PlayerSpeed = authoring.PlayerSpeed,
                    BulletSpeed = authoring.BulletSpeed,
                    AsteroidSpeed = authoring.AsteroidSpeed,
                    AsteroidSize = authoring.AsteroidSize,
                    ScreenSize = authoring.ScreenSize,


                    PlayerPrefab = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic),
                    AsteroidPrefab = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic),
                    BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic)

                });
            }
        }

    }

    public struct GameManagerECS : IComponentData
    {
        public float PlayerSpeed;
        public float BulletSpeed;
        public float AsteroidSpeed;
        public float AsteroidSize;
        public float2 ScreenSize;
        public Entity PlayerPrefab;
        public Entity AsteroidPrefab;
        public Entity BulletPrefab;
    }
}
