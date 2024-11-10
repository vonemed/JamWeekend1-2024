using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh.Globals.Events;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GameStateSystem))]
public sealed class GameStateSystem : UpdateSystem {

    public GlobalEvent startGameEvent;
    public GlobalEvent exitGameEvent;
    public GlobalEvent playGameEvent;
    public GlobalEvent victoryGameEvent;
    public GlobalEvent pauseGameEvent;

    public Filter states;
    
    public override void OnAwake() {
        states = this.World.Filter.With<GameStateComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var state in states)
        {
            ref var stateComponent = ref state.GetComponent<GameStateComponent>();

            if(startGameEvent) stateComponent.state = GameStateComponent.States.Playing;
            if(playGameEvent) stateComponent.state = GameStateComponent.States.Playing;
            if(pauseGameEvent) stateComponent.state = GameStateComponent.States.Pause;
            if(victoryGameEvent) stateComponent.state = GameStateComponent.States.Pause;
            if(exitGameEvent) stateComponent.state = GameStateComponent.States.MainMenu;
        }
    }
}