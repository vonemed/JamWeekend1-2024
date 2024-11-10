using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct VictoryUIComponent : IComponent {

    public Canvas canvas;
    public TMP_Text totalCozy;
    public List<CozyItemDisplay> cozyItems;
}