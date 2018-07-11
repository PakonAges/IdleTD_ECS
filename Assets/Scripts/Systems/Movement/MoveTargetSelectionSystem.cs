using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

//public class MoveTargetSelectionBarrier : BarrierSystem { }
[UpdateAfter(typeof(MoveTargetDistanceCheckSystem))]
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
            if (m_MoverData.NeedMoveTarget[i].Value == true)
            {
                var target = m_MoverData.MoveTarget[i].Value;
                var wp1 = m_WayPointData.Position[0].Value;
                var wp2 = m_WayPointData.Position[1].Value;

                //hack
                if (target.x == wp1.x && target.y == wp1.y && target.z == wp1.z)
                {
                    target = wp2;
                }
                else
                {
                    target = wp1;
                }

                m_MoverData.MoveTarget[i] = new MoveTarget { Value = target};
                m_MoverData.NeedMoveTarget[i] = new NeedMoveTarget { Value = false };
            }
        }
    }
}