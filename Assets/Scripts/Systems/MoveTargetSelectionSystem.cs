using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

//public class MoveTargetSelectionBarrier : BarrierSystem { }

public class MoveTargetSelectionSystem : ComponentSystem
{
    struct MoverData
    {
        public readonly int Length;
        public ComponentDataArray<NeedMoveTarget> NeedMoveTarget;
        public ComponentDataArray<MoveTarget> MoveTarget;
    }

    struct WayPointData
    {
        public readonly int Length;
        [ReadOnly] public ComponentDataArray<WayPoint> WayPoint;
        [ReadOnly] public ComponentDataArray<Position> Position;
    }

    struct BulletTargetData
    {
        public readonly int Length;
        [ReadOnly] public ComponentDataArray<Health> Health;
    }

    [Inject] private MoverData m_MoverData;
    [Inject] private WayPointData m_WayPointData;
    [Inject] private BulletTargetData m_BulletTargetData;
    //[Inject] private MoveTargetSelectionBarrier m_MoveTargetSelectionBarrier;

    protected override void OnUpdate()
    {
        for (int i = 0; i < m_MoverData.Length; i++)
        {
            m_MoverData.NeedMoveTarget[i] = new NeedMoveTarget { Value = false };
            m_MoverData.MoveTarget[i] = new MoveTarget { Value = m_WayPointData.Position[0].Value};
        }
    }
}