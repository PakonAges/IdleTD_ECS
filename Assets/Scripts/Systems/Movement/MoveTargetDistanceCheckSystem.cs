using Unity.Entities;
using Unity.Transforms;

public class MoveTargetDistanceCheckSystem : ComponentSystem
{
    struct Data
    {
        public readonly int Length;
        public ComponentDataArray<MoveTarget> MoveTarget;
        public ComponentDataArray<Position> Position;
        public ComponentDataArray<NeedMoveTarget> NeedMoveTarget;
    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < m_Data.Length; i++)
        {
            var position = m_Data.Position[i].Value;
            var target = m_Data.MoveTarget[i].Value;
            var distance = mUtils.Distance(position, target);

            if (distance < 0.1f)
            {
                m_Data.NeedMoveTarget[i] = new NeedMoveTarget { Value = true };
            }
        }
    }
}
