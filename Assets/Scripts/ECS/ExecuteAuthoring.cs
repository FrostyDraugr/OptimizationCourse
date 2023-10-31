using Unity.Entities;
using UnityEngine;


namespace CoreECS.Execute
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

                if(authoring.SpawnPlayer) AddComponent<PlayerSpawner>(entity);
                if(authoring.SpawnAsteroids) AddComponent<AsteroidSpawner>(entity);
            }
        }
    }

    public struct PlayerSpawner : IComponentData
    {

    }

    public struct AsteroidSpawner : IComponentData
    {
    
    }
}
