using Unity.Entities;
using UnityEngine;

namespace Components
{
    public partial struct MovingRange:IComponentData
    {
        public float minX;
        public float maxX;
    }
    public class MovingRangeAuthoring : MonoBehaviour
    {
        public float MinX = -5f;
        public float MaxX = 5f;

        public class MovingRangeBaker : Baker<MovingRangeAuthoring>
        {
            public override void Bake(MovingRangeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MovingRange { minX = authoring.MinX, maxX = authoring.MaxX});
            }
        }
    }
}
