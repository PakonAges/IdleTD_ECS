using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ShotSpawnBarrier : BarrierSystem { }

public class TowerShootSystem : JobComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        [ReadOnly] public ComponentDataArray<Position> Position;
        public ComponentDataArray<TowerShootState> ShotState;
    }

    [Inject] Data m_data;

    [Inject] ShotSpawnBarrier m_shotSpawnBarrier;

    struct SpawnBullet : IJob 
    {
        public float3 TargetPosition;
        public float DeltaTime;
        public float RateOfFire;
        public int BulletDamage;
        public EntityArchetype BulletArchetype;

        [ReadOnly] public ComponentDataArray<Position> Position;
        public ComponentDataArray<TowerShootState> ShootState;

        public EntityCommandBuffer CommandBuffer;

        public void Execute() 
        {
            for (int i = 0; i < ShootState.Length; i++) 
            {
                var state = ShootState[i];
                state.Cooldown -= DeltaTime;

                if (state.Cooldown <= 0.0)
                {
                    state.Cooldown = RateOfFire;

                    BulletSpawnData spawn;
                    spawn.Bullet.Damage = BulletDamage;
                    spawn.Position = Position[i];

                    CommandBuffer.CreateEntity(BulletArchetype);
                    CommandBuffer.SetComponent(spawn);
                }

                ShootState[i] = state;

            }
            
        }

    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (m_data.Length == 0)
        {
            return inputDeps;
        }

        return new SpawnBullet {
            //TargetPosition = 
            DeltaTime = Time.deltaTime,
            //RateOfFire = 
            //BulletDamage = 
            //BulletArchetype = 
            Position = m_data.Position,
            ShootState = m_data.ShotState,
            CommandBuffer = m_shotSpawnBarrier.CreateCommandBuffer(),
        }.Schedule(inputDeps);
    }
}
