using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Components;
using UnityEngine.UIElements;

namespace Systems{
    public partial struct BulletSpawnerSystem:ISystem{
        public void Onupdate(ref SystemState state)
        {

            var isPressedSpace = Input.GetKeyDown(KeyCode.Space);
            
            foreach (var (tf, spawner) in SystemAPI.Query<RefRO<LocalTransform>, RefRW<BulletSpawnerComponent>>())
            {
                if (!isPressedSpace)
                {
                    spawner.ValueRW.lastSpawnedTime = 0;
                }
                else
                {
                    if (spawner.ValueRO.lastSpawnedTime <= 0)
                    {
                        var newBulletE = state.EntityManager.Instantiate(spawner.ValueRO.prefab);
                        state.EntityManager.SetComponentData(newBulletE, new LocalTransform
                        {
                            Position = tf.ValueRO.Position + spawner.ValueRO.offset,
                            Scale = 1f,
                            Rotation = Quaternion.identity
                        });
                        state.EntityManager.SetComponentData(newBulletE, new BulletComponent
                        {
                            speed = 3f,
                            direction = calculateDirection(tf)
                                
                        });
                        spawner.ValueRW.lastSpawnedTime = spawner.ValueRO.spawnSpeed;
                    }
                    else
                    {
                        spawner.ValueRW.lastSpawnedTime -= SystemAPI.Time.DeltaTime;
                    }
                }
            }
        }

        private float3 calculateDirection(RefRO<LocalTransform> tf)
        {
            return new float3(0f, 0f, 1f);
        }
    }
}