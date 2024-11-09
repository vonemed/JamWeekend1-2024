using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using UnityEditor.Rendering;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InteractablePickupSystem))]
public sealed class InteractablePickupSystem : UpdateSystem
{
    public Filter players;
    public Filter interactables;
    public override void OnAwake()
    {
        players = this.World.Filter.With<PlayerComponent>().With<InteractingComponent>().Without<BusyComponent>().Build();
        interactables = this.World.Filter.With<InteractableComponent>().With<InteractableHoveredComponent>().Without<InteractablePickedUpComponent>()
        .Without<InteractingComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var player in players)
        {
            foreach (var interactable in interactables)
            {
                var playerPosition = player.GetComponent<PlayerComponent>().pickupPosition.position;
                var interactablePosition = interactable.GetComponent<InteractableComponent>().collider.transform.position;

                if (GameTools.IsInRange(interactablePosition, playerPosition, player.GetComponent<PlayerComponent>().playerConfig.interactRange))
                {
                    interactable.AddComponent<InteractingComponent>();

                    player.AddComponent<BusyComponent>();
                }
            }
        }
    }
}