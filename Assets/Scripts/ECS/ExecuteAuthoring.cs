using Unity.Entities;
using UnityEngine;


namespace CoreECS.Execute
{
    public class ExecuteAuthoring : MonoBehaviour
    {
        [Header("Player")]
        public bool SpawnPlayer;
        public bool PlayerMovement;

        [Header("Spawn AsteroidSpawner")]
        public bool SpawnAsteroids;
        public bool AsteroidMover;

        class Baker : Baker<ExecuteAuthoring>
        {
            public override void Bake(ExecuteAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                if(authoring.SpawnPlayer) AddComponent<PlayerSpawner>(entity);
                if(authoring.PlayerMovement) AddComponent<PlayerMovement>(entity);
                if(authoring.SpawnAsteroids) AddComponent<AsteroidSpawner>(entity);
                if(authoring.AsteroidMover) AddComponent<AsteroidMover>(entity);


            }
        }
    }

    public struct PlayerSpawner : IComponentData
    {
    
    }

    public struct PlayerMovement : IComponentData
    {

    }

    public struct AsteroidSpawner : IComponentData
    {
    
    }

    public struct AsteroidMover : IComponentData
    {

    }
}
