using Unity.Entities;
using UnityEngine;

namespace Components
{
    public partial struct MovingComponent:IComponentData
    {
        public float moveSpeed;
    }
    public class MovingAuthoring : MonoBehaviour{

        public float MoveSpeed = 3f;
        public class MovingComponentBaker : Baker<MovingAuthoring>
        {
            public override void Bake(MovingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MovingComponent { moveSpeed = authoring.MoveSpeed });
            }
        }
    }
}