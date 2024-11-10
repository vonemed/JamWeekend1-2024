using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(GameInitializeSystem))]
public sealed class GameInitializeSystem : Initializer {
    private Filter timers;
    public override void OnAwake() {
        this.World.CreateEntity().AddComponent<GameStateComponent>();

        timers = this.World.Filter.With<TimerComponent>().Build();

        foreach (var timer in timers)
        {
            timer.GetComponent<TimerComponent>().currentTime = timer.GetComponent<TimerComponent>().gameConfig.timer;
        }
    }

    public override void Dispose() {
    }
}