using Unity.Entities;

public struct Bullet : IComponentData 
{
    public int Damage;
}

class BulletComponent : ComponentDataWrapper<Bullet> 
{

}