using Unity.Entities;
using Components;
using Unity.Transforms;

namespace Systems
{
    public partial struct BulletMovingSystem: ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            new MovingJob { deltaTime = SystemAPI.Time.DeltaTime}.ScheduleParallel();
        }
        public partial struct MovingJob:IJobEntity
        {
            public float deltaTime;
            void Execute(RefRW<LocalTransform> tf, RefRO<BulletComponent> bullet)
            {
                tf.ValueRW.Position += bullet.ValueRO.direction * bullet.ValueRO.speed * deltaTime;
            }
        }
    }
}