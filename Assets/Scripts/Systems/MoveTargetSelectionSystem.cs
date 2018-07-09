using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

public class MoveTargetSelectionBarrier : BarrierSystem { }

public class MoveTargetSelectionSystem : JobComponentSystem
{
    struct MoverData
    {
        [ReadOnly] public EntityArray Entity;
        [ReadOnly] public ComponentDataArray<NeedMoveTarget> NeedMoveTarget;
    }

    struct MoveTargetData
    {
        [ReadOnly] public EntityArray Entity;
        [ReadOnly] public ComponentDataArray<WayPoint> WayPoint;
    }

    struct BulletTargetData
    {
        [ReadOnly] public EntityArray Entity;
        [ReadOnly] public ComponentDataArray<Health> Health;
    }

    [Inject] private MoverData m_MoverData;
    [Inject] private MoveTargetData m_MoveTargetData;
    [Inject] private BulletTargetData m_BulletTargetData;
    [Inject] private MoveTargetSelectionBarrier m_MoveTargetSelectionBarrier;

    [BurstCompile]
    struct SelectTargetJob : IJob
    {
        [ReadOnly] public EntityArray Mover;
        [ReadOnly] public ComponentDataArray<NeedMoveTarget> NeedMoveTarget;
        [ReadOnly] public EntityArray MoveTarget;
        [ReadOnly] public EntityArray BulletTarget;

        public EntityCommandBuffer Commands;

        public void Execute()
        {
            for (int i = 0; i < Mover.Length; i++)
            {
                if (NeedMoveTarget[i].Value == true)
                {
                    
                    //Find New proper Target
                    //Commands.SetComponent<MoveTarget>(Mover[i]);
                    
                    //Set new Move target to wayPoint1
                
                    //Need Move Target = false
                }
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new SelectTargetJob
        {
            Mover = m_MoverData.Entity,

        }.Schedule(inputDeps);
    }
}
