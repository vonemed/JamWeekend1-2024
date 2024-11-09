using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(PlayerUISetupSystem))]
public sealed class PlayerUISetupSystem : Initializer
{
    public Filter players;
    public Filter playerUI;
    public override void OnAwake()
    {
        players = this.World.Filter.With<PlayerComponent>().With<StaminaComponent>().Build();
        playerUI = this.World.Filter.With<PlayerUIComponent>().Build();

        foreach (var ui in playerUI)
        {
            var playerStamina = players.FirstOrDefault().GetComponent<StaminaComponent>();
            var staminaSlider = ui.GetComponent<PlayerUIComponent>().staminaBar;

            staminaSlider.maxValue = playerStamina.value;
            staminaSlider.value = playerStamina.value;
        }
    }

    public override void Dispose()
    {

    }
}