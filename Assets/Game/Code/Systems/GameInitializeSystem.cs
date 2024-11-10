using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(GameInitializeSystem))]
public sealed class GameInitializeSystem : Initializer
{
    private Filter players;
    private Filter interactables;

    public override void OnAwake()
    {
        this.World.CreateEntity().AddComponent<GameStateComponent>();

        players = this.World.Filter.With<PlayerComponent>().Build();
        interactables = this.World.Filter.With<InteractableComponent>().Build();

        foreach (var player in players)
        {
            player.AddComponent<StartPositionComponent>().position = player.GetComponent<PlayerComponent>().body.transform.position;
        }

        foreach (var interactable in interactables)
        {
            interactable.AddComponent<StartPositionComponent>().position = interactable.GetComponent<InteractableComponent>().body.transform.position;
        }
    }

    public override void Dispose()
    {
    }
}