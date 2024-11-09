public static class GameTools
{
    public static bool IsInRange(in UnityEngine.Vector3 source, in UnityEngine.Vector3 target, in float range)
    {
        var sqrDistance = (source - target).sqrMagnitude;
        return sqrDistance <= range * range;
    }
}