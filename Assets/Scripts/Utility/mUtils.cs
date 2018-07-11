using Unity.Mathematics;
using UnityEngine;

public static class mUtils
{
    public static float Distance(float3 a, float3 b)
    {
        return Mathf.Sqrt(Mathf.Pow((b.x - a.x), 2.0f) + Mathf.Pow((b.y - a.y), 2.0f) + Mathf.Pow((b.z - a.z), 2.0f));
    }

    public static float3 Normalize(float3 a)
    {
        return new float3(a.x / Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z), a.y / Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z), a.z / Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z));
    }
	
}
