using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public sealed class MainBootStrap 
{
    public static EntityArchetype TowerArchetype;
    public static EntityArchetype CreepArchetype;
    public static EntityArchetype BulletArchetype;

    public static MeshInstanceRenderer TowerLook;
    public static MeshInstanceRenderer CreepLook;
    public static MeshInstanceRenderer BulletLook;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize() 
    {
        //Creation of all archetypes

        var entityManager = World.Active.GetOrCreateManager<EntityManager>();

        TowerArchetype = entityManager.CreateArchetype(
            typeof(Tower),
            typeof(Position),
            typeof(TransformMatrix),
            typeof(MeshInstanceRenderer));

        CreepArchetype = entityManager.CreateArchetype(
            typeof(Creep),
            typeof(Position),
            typeof(Rotation),
            typeof(TransformMatrix),
            typeof(MeshInstanceRenderer));

        BulletArchetype = entityManager.CreateArchetype(
            typeof(Bullet),
            typeof(Position),
            typeof(Rotation),
            typeof(TransformMatrix),
            typeof(MeshInstanceRenderer));
    }

    public static void NewGame() 
    {
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();

        Entity tower = entityManager.CreateEntity(TowerArchetype);
        entityManager.SetComponentData(tower, new Position {Value = new float3(-2.0f, 0.0f, 0.0f)});
        entityManager.SetSharedComponentData(tower, TowerLook);

        Entity creep = entityManager.CreateEntity(CreepArchetype);
        entityManager.SetComponentData(creep, new Position { Value = new float3(0.0f, 0.0f, 0.0f) });
        entityManager.SetSharedComponentData(creep, CreepLook);


        Entity bullet = entityManager.CreateEntity(BulletArchetype);
        entityManager.SetComponentData(bullet, new Position { Value = new float3(2.0f, 0.0f, 0.0f) });
        entityManager.SetSharedComponentData(bullet, BulletLook);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeAfterSceneLoad() 
    {
        var settings = GameObject.Find("Setup");

        TowerLook.mesh = settings.GetComponent<TowerSettings>().TowerMesh;
        TowerLook.material = settings.GetComponent<TowerSettings>().TowerMaterial;

        CreepLook.mesh = settings.GetComponent<CreepSettings>().CreepMesh;
        CreepLook.material = settings.GetComponent<CreepSettings>().CreepMaterial;

        BulletLook.mesh = settings.GetComponent<BulletSettings>().BulletMesh;
        BulletLook.material = settings.GetComponent<BulletSettings>().BulletMaterial;
    }
}