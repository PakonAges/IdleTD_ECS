using Unity.Entities;
using Unity.Transforms;
public struct BulletSpawnData : IComponentData 
{
    public Bullet Bullet;
    public Position Position;

}
