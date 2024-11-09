using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(UserMovementFixedSystem))]
public sealed class UserMovementFixedSystem : FixedUpdateSystem
{
    private Filter directions;
    private Filter players;
    public override void OnAwake()
    {
        directions = this.World.Filter.With<MoveDirectionComponent>().Build();
        players = this.World.Filter.With<PlayerComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var player in players)
        {
            var direction = directions.FirstOrDefault().GetComponent<MoveDirectionComponent>().direction;
            if (direction.sqrMagnitude == 0) return;
            //look todo: probably move to another system

            var playerData = player.GetComponent<PlayerComponent>();

            var speed = playerData.speed;

            //* if player is running and has stamina
            if(player.Has<RunningComponent>() && player.GetComponent<StaminaComponent>().value > 0) speed *= playerData.staminaConfig.speedMultiplier;
            
            var scaledMoveSpeed = speed * Time.deltaTime;
            var moveDirection = playerData.body.transform.TransformDirection(new Vector3(direction.x, 0, direction.y));

            playerData.body.MovePosition(playerData.body.transform.position + moveDirection * scaledMoveSpeed);
        }
    }
}