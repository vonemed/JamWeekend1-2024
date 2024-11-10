using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh.Globals.Events;
using Scellecs.Morpeh;
using Ami.BroAudio;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(RestartGameSystem))]
public sealed class RestartGameSystem : UpdateSystem
{
    private Filter timers;
    private Filter players;
    private Filter interactables;
    public Filter playlist;

    public GlobalEvent restartEvent;
    public override void OnAwake()
    {
        players = this.World.Filter.With<PlayerComponent>().With<StartPositionComponent>().Build();
        interactables = this.World.Filter.With<InteractableComponent>().With<StartPositionComponent>().Build();
        playlist = this.World.Filter.With<MusicComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (restartEvent)
        {
            Cursor.visible = false;

            //* Restart timer
            timers = this.World.Filter.With<TimerComponent>().Build();

            foreach (var timer in timers)
            {
                timer.GetComponent<TimerComponent>().currentTime = timer.GetComponent<TimerComponent>().gameConfig.timer;
            }

            //* Reset player
            foreach (var player in players)
            {
                ref var playerComponent = ref player.GetComponent<PlayerComponent>();
                ref var currentStamina = ref player.GetComponent<StaminaComponent>();

                playerComponent.body.transform.position = player.GetComponent<StartPositionComponent>().position;
                playerComponent.totalCoziness = 0;

                currentStamina.value = playerComponent.staminaConfig.totalStamina;

                player.RemoveComponent<PickedUpInteractableComponent>();
            }

            //* Reset interactables
            foreach (var interactable in interactables)
            {
                ref var interactableComponent = ref interactable.GetComponent<InteractableComponent>();

                interactableComponent.body.transform.position = interactable.GetComponent<StartPositionComponent>().position;

                interactable.RemoveComponent<InteractablePlacedComponent>();
                interactable.RemoveComponent<InteractableFlyToPlaceComponent>();
                interactable.RemoveComponent<InteractablePickedUpComponent>();

                interactable.GetComponent<InteractableComponent>().collider.enabled = true;
                interactable.GetComponent<InteractableComponent>().body.isKinematic = false;
                interactable.GetComponent<InteractableComponent>().body.useGravity = true;
                interactable.GetComponent<InteractableComponent>().body.constraints = RigidbodyConstraints.None;
            }

            //* Music reset
            var musicComponent = playlist.FirstOrDefault().GetComponent<MusicComponent>();

            BroAudio.Play(musicComponent.musicConfig.gameMusic).AsBGM();
        }
    }
}