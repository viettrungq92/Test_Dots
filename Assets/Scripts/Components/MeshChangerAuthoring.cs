using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshChanger : IComponentData
{
    public Mesh mesh0;
    public Mesh mesh1;
    public uint frequency;
    public uint frame;
    public uint active;
}

[DisallowMultipleComponent]
public class MeshChangerAuthoring : MonoBehaviour
{
    public Mesh mesh0;
    public Mesh mesh1;

    [RegisterBinding(typeof(MeshChanger), "frequency")]
    public uint frequency;
    [RegisterBinding(typeof(MeshChanger), "frame")]
    public uint frame;
    [RegisterBinding(typeof(MeshChanger), "active")]
    public uint active;

    class MeshChangerBaker : Baker<MeshChangerAuthoring>
    {
        public override void Bake(MeshChangerAuthoring authoring)
        {
            MeshChanger component = new MeshChanger();
            component.mesh0 = authoring.mesh0;
            component.mesh1 = authoring.mesh1;
            component.frequency = authoring.frequency;
            component.frame = authoring.frequency;
            component.active = authoring.active;
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
        }
    }
}