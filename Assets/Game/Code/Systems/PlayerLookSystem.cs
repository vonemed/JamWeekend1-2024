using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh.Collections;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerLookSystem))]
public sealed class PlayerLookSystem : UpdateSystem
{
    RaycastHit hit;
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    float rayLength = 500f;
    Ray ray;

    public Filter interactables;

    public override void OnAwake()
    {
        interactables = this.World.Filter.With<InteractableComponent>().With<InteractableHoveredComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        ray = Camera.main.ViewportPointToRay(rayOrigin);
        // debug Ray
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (EntityProvider.map.TryGetValue(hit.collider.gameObject.GetInstanceID(), out var item))
            {
                if (item.entity.Has<InteractableComponent>() && !item.entity.Has<InteractableHoveredComponent>())
                    item.entity.AddComponent<InteractableHoveredComponent>();
            }
            else
            {
                foreach (var interactable in interactables)
                {
                    interactable.RemoveComponent<InteractableHoveredComponent>();
                }
            }
        }
    }
}