using Unity.Entities;

public struct WayPoint : IComponentData
{
}

class WayPointComponent : ComponentDataWrapper<WayPoint>
{

}