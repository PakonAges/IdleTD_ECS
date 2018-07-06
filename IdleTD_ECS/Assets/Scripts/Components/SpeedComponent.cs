using System;
using Unity.Entities;

[Serializable]
public struct Speed : IComponentData {
    public float Value;
}

class SpeedComponent : ComponentDataWrapper<Speed> {

}