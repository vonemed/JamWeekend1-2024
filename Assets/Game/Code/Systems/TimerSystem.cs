using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Globals.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(TimerSystem))]
public sealed class TimerSystem : UpdateSystem {
    public Filter timers;
    public GlobalEvent victoryEvent;
    public override void OnAwake() {
        timers = this.World.Filter.With<TimerComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var timer in timers)
        {
            ref var currentTime = ref timer.GetComponent<TimerComponent>().currentTimer;
            currentTime -= Time.deltaTime;
            timer.GetComponent<TimerComponent>().timer.SetText(currentTime.ToString());

            if(currentTime <= 0) victoryEvent.Publish();
        }
    }
}