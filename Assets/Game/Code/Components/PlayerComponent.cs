using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Cinemachine;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct PlayerComponent : IComponent {
    public StaminaConfig staminaConfig;
    public PlayerConfig playerConfig;
    public GameConfig gameConfig;
    public SoundConfig soundConfig;
    public float speed;
    public int totalCoziness;
    public Rigidbody body;
    public Transform pickupPosition;
    public CinemachineVirtualCamera fpsCamera;
}