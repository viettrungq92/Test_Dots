using UnityEngine;
using Unity.Entities;

namespace Components
{
    public partial struct ControlledMovingComponent:IComponentData
    {

    }



    public class ControlledMovingAuthoring : MonoBehaviour
    {
        public class ControlledMovingComponentBaker : Baker<ControlledMovingAuthoring>
        {
            public override void Bake(ControlledMovingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ControlledMovingComponent());
            }
        }
    }
}
