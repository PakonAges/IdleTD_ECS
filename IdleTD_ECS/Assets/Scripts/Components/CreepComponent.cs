using Unity.Entities;

public struct Creep : IComponentData 
{
    public int Health;
}

class CreepComponent : ComponentDataWrapper<Creep>
{

}