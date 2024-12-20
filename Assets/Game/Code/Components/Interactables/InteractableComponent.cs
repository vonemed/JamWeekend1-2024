using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using TMPro;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct InteractableComponent : IComponent {
    public Interactable interactableData;
    public InteractablesConfig interactablesConfig;
    public Collider collider;
    public Rigidbody body;
    public Transform nameHolder;
    public Transform finalPlace;
    public TMP_Text hoverText;
    public bool canBePickedUp;
}