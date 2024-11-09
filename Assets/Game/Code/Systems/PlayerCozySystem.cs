using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerCozySystem))]
public sealed class PlayerCozySystem : UpdateSystem
{
    public Filter players;
    public Filter playerUI;
    public override void OnAwake()
    {
        players = this.World.Filter.With<PlayerComponent>().With<StaminaComponent>().Build();
        playerUI = this.World.Filter.With<PlayerUIComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var ui in playerUI)
        {
            var playerComponent = players.FirstOrDefault().GetComponent<PlayerComponent>();
            var cozySlider = ui.GetComponent<PlayerUIComponent>().cozyBar;

            cozySlider.value = playerComponent.totalCoziness;
        }
    }
}