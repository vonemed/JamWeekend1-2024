using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InteractableFlySystem))]
public sealed class InteractableFlySystem : UpdateSystem
{
    public Filter players;
    public Filter interactables;
    public override void OnAwake()
    {
        players = this.World.Filter.With<PlayerComponent>().Build();
        interactables = this.World.Filter.With<InteractableComponent>().With<InteractingComponent>().Without<InteractablePickedUpComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var player in players)
        {
            foreach (var interactable in interactables)
            {
                var playerPosition = player.GetComponent<PlayerComponent>().pickupPosition.position;
                var interactableComponent = interactable.GetComponent<InteractableComponent>();
                var interactablePosition = interactableComponent.collider.transform.position;

                if (GameTools.IsInRange(interactablePosition, playerPosition, interactableComponent.interactablesConfig.interactablePickupRange))
                {
                    interactable.AddComponent<InteractablePickedUpComponent>();
                    interactableComponent.collider.transform.SetParent(player.GetComponent<PlayerComponent>().pickupPosition);
                    interactable.RemoveComponent<InteractingComponent>();
                    interactable.GetComponent<InteractableComponent>().collider.enabled = false;
                    interactable.GetComponent<InteractableComponent>().body.isKinematic = true;
                    interactable.GetComponent<InteractableComponent>().body.useGravity = false;
                    interactable.GetComponent<InteractableComponent>().body.constraints = RigidbodyConstraints.FreezeAll;

                    if(!player.Has<PickedUpInteractableComponent>()) player.AddComponent<PickedUpInteractableComponent>().interactable = interactable;
                    else player.SetComponent<PickedUpInteractableComponent>(new PickedUpInteractableComponent() { interactable = interactable});
                }

                var direction = playerPosition - interactablePosition;

                interactableComponent.collider.transform.Translate(direction * 
                interactableComponent.interactablesConfig.interactableFlySpeed * Time.deltaTime);
            }
        }
    }
}