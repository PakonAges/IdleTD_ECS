using Unity.Entities;

public struct Creep : IComponentData 
{
}

class CreepComponent : ComponentDataWrapper<Creep>
{

}