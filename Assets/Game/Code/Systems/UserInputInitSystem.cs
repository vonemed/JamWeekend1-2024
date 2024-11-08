using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Cinemachine;
using System;
using Unity.VisualScripting;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(UserInputInitSystem))]
public sealed class UserInputInitSystem : UpdateSystem
{
    private Filter filter;
    public override void OnAwake()
    {
        filter = this.World.Filter.With<UserComponent>().Build();

        foreach (var user in filter)
        {
            ref var inputActions = ref user.GetComponent<UserComponent>().inputActions;
            inputActions = new PlayerInput();
            inputActions.Enable();
            user.AddComponent<MoveDirectionComponent>();
            user.AddComponent<LookAngleComponent>();
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var user in filter)
        {
            var inputActions = user.GetComponent<UserComponent>().inputActions;

            //* Read player input
            user.GetComponent<MoveDirectionComponent>().direction = inputActions.Player.Movement.ReadValue<Vector2>();

            //* Read the horizontal value of fps camera
            user.GetComponent<LookAngleComponent>().angle = user.GetComponent<PlayerComponent>().fpsCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value;

            //* Read the running input
            if (inputActions.Player.Running.IsPressed())
            {
                if(!user.Has<RunningComponent>()) user.AddComponent<RunningComponent>();
                else user.SetComponent<RunningComponent>(user.GetComponent<RunningComponent>());
            }
            else
            {
                user.RemoveComponent<RunningComponent>();
            }
        }
    }
}