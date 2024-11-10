using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using DG.Tweening;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InteractablePlaceSystem))]
public sealed class InteractablePlaceSystem : UpdateSystem
{
    public Filter players;
    public Filter interactables;

    public override void OnAwake()
    {
        players = this.World.Filter.With<PlayerComponent>().With<InteractingComponent>().With<BusyComponent>().With<PickedUpInteractableComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var player in players)
        {
            var playerComponent = player.GetComponent<PlayerComponent>();
            var interactable = player.GetComponent<PickedUpInteractableComponent>().interactable;
            var finalPosition = interactable.GetComponent<InteractableComponent>().finalPlace.position;

            if (interactable.Has<InteractableFlyToPlaceComponent>()) return;

            if (GameTools.IsInRange(playerComponent.pickupPosition.position, finalPosition, 5f))
            {
                player.RemoveComponent<BusyComponent>();
                interactable.GetComponent<InteractableComponent>().body.transform.SetParent(null);
                interactable.AddComponent<InteractableFlyToPlaceComponent>();

                interactable.GetComponent<InteractableComponent>().body.transform.DOMove(interactable.GetComponent<InteractableComponent>().finalPlace.position,
                0.5f).OnComplete(() =>
                {
                    interactable.AddComponent<InteractablePlacedComponent>();
                    player.GetComponent<PlayerComponent>().totalCoziness += interactable.GetComponent<InteractableComponent>().interactableData.cozyValue;
                    player.RemoveComponent<PickedUpInteractableComponent>();
                });
            }
        }
    }
}