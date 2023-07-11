using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public partial struct BulletComponent:IComponentData,IEnableableComponent
    {
        public float speed;

        public float3 direction;
        
        public float minDistance;
    }

    
}