using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(StaminaTickSystem))]
public sealed class StaminaTickSystem : UpdateSystem {
    public Filter players;
    public override void OnAwake() {
        players = this.World.Filter.With<PlayerComponent>().With<RunningComponent>().With<StaminaComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var player in players)
        {
            ref var stamina =  ref player.GetComponent<StaminaComponent>();
            var staminaConfig = player.GetComponent<PlayerComponent>().staminaConfig;

            if(stamina.value <= 0) return;

            stamina.value -= Time.deltaTime * staminaConfig.useStaminaPerSecond;
        }
    }
}