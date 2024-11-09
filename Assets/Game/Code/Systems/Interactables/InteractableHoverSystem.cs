using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InteractableHoverSystem))]
public sealed class InteractableHoverSystem : UpdateSystem
{
    public Filter interactables;
    public override void OnAwake()
    {
        interactables = this.World.Filter.With<InteractableComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var interactable in interactables)
        {
            var hoverText = interactable.GetComponent<InteractableComponent>().hoverText;

            if (interactable.Has<InteractableHoveredComponent>())
            {
                var interactableData = interactable.GetComponent<InteractableComponent>().interactableData;
                hoverText.SetText(interactableData.objectName);
                hoverText.gameObject.SetActive(true);
                interactable.GetComponent<InteractableComponent>().nameHolder.LookAt(Camera.main.transform);
            }
            else
            {
                hoverText.gameObject.SetActive(false);
            }
        }
    }
}