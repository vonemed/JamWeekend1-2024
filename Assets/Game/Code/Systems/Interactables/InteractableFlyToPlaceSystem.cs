using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InteractableFlyToPlaceSystem))]
public sealed class InteractableFlyToPlaceSystem : UpdateSystem
{
    public Filter interactables;
    public override void OnAwake()
    {
        interactables = this.World.Filter.With<InteractableComponent>().With<InteractableFlyToPlaceComponent>().Without<InteractablePlacedComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var interactable in interactables)
        {
            // var interactableComponent = interactable.GetComponent<InteractableComponent>();
            // if (GameTools.IsInRange(interactableComponent.body.transform.position, interactableComponent.finalPlace.position, 0.1f))
            // {
            //     interactable.AddComponent<InteractablePlacedComponent>();
            //     interactable.RemoveComponent<InteractableFlyToPlaceComponent>();
            // }

            // var direction =  interactableComponent.finalPlace.position - interactableComponent.body.transform.position;

            // interactableComponent.collider.transform.Translate(direction *
            // interactableComponent.interactablesConfig.interactableFlySpeed * Time.deltaTime);
        }
    }
}