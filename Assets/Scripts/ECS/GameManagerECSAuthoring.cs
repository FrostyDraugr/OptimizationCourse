using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace CoreECS
{
    public class GameManagerECSAuthoring : MonoBehaviour
    {
        [Header("Player")]
        public float PlayerSpeed;
        public bool PlayerDeath;
        public GameObject PlayerPrefab;

        [Header("Bullet")]
        public float BulletSpeed;
        public float FireCoolDown;
        public GameObject BulletPrefab;

        [Header("Asteroid")]
        public float AsteroidSpeed;
        public float AsteroidSize;
        public float AsteroidYOffset;
        public float AsteroidSpawnBoxY;
        public float AsteroidSpawnPause;
        public int AsteroidCount;
        public float SpawnCycleModifier;
        public GameObject AsteroidPrefab;

        [Header("Misc")]
        public float2 ScreenSize;


        class Baker :Baker<GameManagerECSAuthoring>
        {
            public override void Bake(GameManagerECSAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new GameManagerECS
                {
                    PlayerSpeed = authoring.PlayerSpeed,
                    PlayerDeath = authoring.PlayerDeath,
                    BulletSpeed = authoring.BulletSpeed,
                    FireCoolDown = authoring.FireCoolDown,
                    AsteroidSpeed = authoring.AsteroidSpeed,
                    AsteroidSize = authoring.AsteroidSize,
                    //Might as well compile the data here instead of having two entries
                    AsteroidYOffset = authoring.AsteroidYOffset + authoring.ScreenSize.y,
                    AsteroidSpawnBoxY = authoring.AsteroidSpawnBoxY,
                    AsteroidSpawnPause = authoring.AsteroidSpawnPause,
                    AsteroidCount = authoring.AsteroidCount,
                    SpawnCycleModifier = authoring.SpawnCycleModifier,
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
        public bool PlayerDeath;
        public float BulletSpeed;
        public float FireCoolDown;
        public float AsteroidSpeed;
        public float AsteroidSize;
        public float AsteroidYOffset;
        public float AsteroidSpawnBoxY;
        public float AsteroidSpawnPause;
        public int AsteroidCount;
        public float SpawnCycleModifier;
        public float2 ScreenSize;
        public Entity PlayerPrefab;
        public Entity AsteroidPrefab;
        public Entity BulletPrefab;
    }
}
