using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class MoveToTargetSystem : ComponentSystem
{
    struct Data
    {
        public readonly int Length;
        public ComponentDataArray<MoveTarget> MoveTarget;
        public ComponentDataArray<Position> Position;

    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;

        for (int i = 0; i < m_Data.Length; i++)
        {
            var position = m_Data.Position[i].Value;
            position += dt * m_Data.MoveTarget[i].Value * 0.5f;

            m_Data.Position[i] = new Position { Value = position };
        }
    }
}
