using Unity.Entities;

public struct Tower : IComponentData 
{
    public float TowerRange;
    public float RateOfFire;
}

class TowerComponent : ComponentDataWrapper<Tower>
{

}