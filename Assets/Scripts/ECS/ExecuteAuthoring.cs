using Unity.Entities;
using UnityEngine;


namespace CoreECS
{
    public class ExecuteAuthoring : MonoBehaviour
    {
        [Header("Spawn Player")]
        public bool SpawnPlayer;

        [Header("Spawn AsteroidSpawner")]
        public bool SpawnAsteroids;

        class Baker : Baker<ExecuteAuthoring>
        {
            public override void Bake(ExecuteAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                if(authoring.SpawnPlayer) AddComponent<ExecutePlayerSpawner>(entity);
                if(authoring.SpawnAsteroids) AddComponent<ExecuteAsteroidSpawner>(entity);
            }
        }
    }

    public struct ExecutePlayerSpawner : IComponentData
    {

    }

    public struct ExecuteAsteroidSpawner : IComponentData
    {
    
    }
}
