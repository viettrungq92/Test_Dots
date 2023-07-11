using UnityEngine;
using Unity.Entities;

namespace Components
{
    public class EnemyComponentAuthoring : MonoBehaviour
    {
        public class EnemyComponentBaker : Baker<EnemyComponentAuthoring>
        {
            public override void Bake(EnemyComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyComponent());
            }
        }
    }
}