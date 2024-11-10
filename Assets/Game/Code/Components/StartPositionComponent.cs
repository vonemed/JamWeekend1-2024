using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

//* Resets objects to this position
[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct StartPositionComponent : IComponent {
    public Vector3 position;
}