using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(UserLookSystem))]
public sealed class UserLookSystem : UpdateSystem
{
    private Filter players;

    public override void OnAwake()
    {
        players = this.World.Filter.With<PlayerComponent>().With<LookAngleComponent>().Build();

    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var player in players)
        {
            var playerData = player.GetComponent<PlayerComponent>();

            playerData.body.transform.rotation = Quaternion.Euler(0, player.GetComponent<LookAngleComponent>().angle, 0);
        }
    }
}