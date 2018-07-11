using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(MoveTargetSelectionSystem))]
[UpdateAfter(typeof(MoveTargetDistanceCheckSystem))]
public class MoveToTargetSystem : ComponentSystem
{
    struct Data
    {
        public readonly int Length;
        public ComponentDataArray<MoveTarget> MoveTarget;
        public ComponentDataArray<MoveSpeed> MoveSpeed;
        public ComponentDataArray<Position> Position;
    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;

        for (int i = 0; i < m_Data.Length; i++)
        {
            var position = m_Data.Position[i].Value;
            var target = m_Data.MoveTarget[i].Value;
            var dir = target - position;
            var dirNormal = mUtils.Normalize(dir);

            position += dt * dirNormal * m_Data.MoveSpeed[i].speed;
            m_Data.Position[i] = new Position { Value = position };
        }
    }
}
