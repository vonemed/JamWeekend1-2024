using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Globals.Events;
using System.Linq;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(TimerSystem))]
public sealed class TimerSystem : UpdateSystem
{
    public Filter timers;
    public Filter states;
    public GlobalEvent victoryEvent;

    private bool isGameStarted = false;
    public override void OnAwake()
    {
        timers = this.World.Filter.With<TimerComponent>().Build();
        states = this.World.Filter.With<GameStateComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var state in states)
        {
            var stateComponent = state.GetComponent<GameStateComponent>();

            if (stateComponent.state == GameStateComponent.States.Pause) return;

            foreach (var timer in timers)
            {
                ref var currentTime = ref timer.GetComponent<TimerComponent>().currentTime;

                if (currentTime <= 0) return;
                
                currentTime -= Time.deltaTime;
                timer.GetComponent<TimerComponent>().timer.SetText($"{(int)currentTime}");

                if (currentTime <= 0) victoryEvent.Publish();
            }
        }
    }
}