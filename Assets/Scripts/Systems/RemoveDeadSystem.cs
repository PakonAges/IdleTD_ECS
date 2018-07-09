using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

public class RemoveDeadBarrier : BarrierSystem { }

public class RemoveDeadSystem : JobComponentSystem
{
    struct Data
    {
        [ReadOnly] public EntityArray Entity;
        [ReadOnly] public ComponentDataArray<Health> Health;
    }

    struct PlayerCheck 
    {
        //[ReadOnly] public ComponentDataArray<PlayerInput> PlayerInput;
    }

    [Inject] private Data m_Data;
    [Inject] private PlayerCheck m_PlayerCheck;
    [Inject] private RemoveDeadBarrier m_RemoveDeadBarrier;

    [BurstCompile]
    struct RemoveDeadJob : IJob
    {
        public bool playerDead;
        [ReadOnly] public EntityArray Entity;
        [ReadOnly] public ComponentDataArray<Health> Health;
        public EntityCommandBuffer Commands;

        public void Execute()
        {
            for (int i = 0; i < Entity.Length; i++)
            {
                if (Health[i].Value <= 0.0f /*|| playerDead*/)
                {
                    Commands.DestroyEntity(Entity[i]);
                }
            }
        }

    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new RemoveDeadJob
        {
            playerDead = false, //Don't do this job if player is dead. Is it cool for me?
            Entity = m_Data.Entity,
            Health = m_Data.Health,

            Commands = m_RemoveDeadBarrier.CreateCommandBuffer(),
        }.Schedule(inputDeps);
    }
}
