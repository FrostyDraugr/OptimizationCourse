using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerECS
{
    public class MovementAuthoring : MonoBehaviour
    {
        public float Speed;

    }

    class Baker : Baker<MovementAuthoring>
    {
        public override void Bake(MovementAuthoring authoring)        {
            var enitity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<Movement>(enitity);
        }
    }

}
