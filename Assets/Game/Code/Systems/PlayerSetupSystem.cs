using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(PlayerSetupSystem))]
public sealed class PlayerSetupSystem : Initializer {

    public Filter players;

    public override void OnAwake() {
        players = this.World.Filter.With<PlayerComponent>().Build();
        var player = players.FirstOrDefault();

        if(player != null)
        {
            player.AddComponent<StaminaComponent>().value = player.GetComponent<PlayerComponent>().staminaConfig.totalStamina;
        }
    }

    public override void Dispose() {
    }
}