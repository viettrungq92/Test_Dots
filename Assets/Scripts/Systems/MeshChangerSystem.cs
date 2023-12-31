﻿using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

[RequireMatchingQueriesForUpdate]
public partial class MeshChangerSystem : SystemBase
{
    private Dictionary<Mesh, BatchMeshID> m_MeshMapping;

    private void RegisterMesh(EntitiesGraphicsSystem hybridRendererSystem, Mesh mesh)
    {
        // Only register each mesh once, so we can also unregister each mesh just once
        if (!m_MeshMapping.ContainsKey(mesh))
            m_MeshMapping[mesh] = hybridRendererSystem.RegisterMesh(mesh);
    }

    protected override void OnStartRunning()
    {
        var hybridRenderer = World.GetOrCreateSystemManaged<EntitiesGraphicsSystem>();
        m_MeshMapping = new Dictionary<Mesh, BatchMeshID>();

        Entities
            .WithoutBurst()
            .ForEach((in MeshChanger changer) =>
            {
                RegisterMesh(hybridRenderer, changer.mesh0);
                RegisterMesh(hybridRenderer, changer.mesh1);
            }).Run();
    }

    private void UnregisterMeshes()
    {
        // Can't call this from OnDestroy(), so we can't do this on teardown
        var hybridRenderer = World.GetExistingSystemManaged<EntitiesGraphicsSystem>();
        if (hybridRenderer == null)
            return;

        foreach (var kv in m_MeshMapping)
            hybridRenderer.UnregisterMesh(kv.Value);
    }

    protected override void OnUpdate()
    {
        EntityManager entityManager = EntityManager;

        Entities
            .WithoutBurst()
            .ForEach((MeshChanger changer, ref MaterialMeshInfo mmi) =>
            {
                changer.frame = changer.frame + 1;

                if (changer.frame >= changer.frequency)
                {
                    changer.frame = 0;
                    changer.active = changer.active == 0 ? 1u : 0u;

                    var mesh = changer.active == 0 ? changer.mesh0 : changer.mesh1;
                    mmi.MeshID = m_MeshMapping[mesh];
                }

            }).Run();
    }
}