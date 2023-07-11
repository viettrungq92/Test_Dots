using UnityEngine;
using Unity.Entities;

namespace Components
{
    public class BulletComponentAuthoring : MonoBehaviour
    {
        public float Speed = 3f;

        public class BulletComponentBaker : Baker<BulletComponentAuthoring>
        {
            public override void Bake(BulletComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletComponent { speed = authoring.Speed });
            }
        }
    }
}