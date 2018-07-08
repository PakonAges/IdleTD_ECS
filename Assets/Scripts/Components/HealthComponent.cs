using System;
using Unity.Entities;

[Serializable]
public struct Health : IComponentData {
    public int Value;
}

class HealthComponent : ComponentDataWrapper<Health> {

}