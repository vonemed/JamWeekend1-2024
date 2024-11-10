using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Ami.BroAudio;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(WalkingAudioSystem))]
public sealed class WalkingAudioSystem : UpdateSystem {
    public Filter players;
    private float currentStep = 0f;
    private float stepThreshold = 3f;
    public override void OnAwake() {
        players = this.World.Filter.With<PlayerComponent>().With<WalkingComponent>().Build();
        stepThreshold = 0.4f;
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var player in players)
        {
            currentStep += Time.deltaTime;

            if(currentStep >= stepThreshold)
            {
                BroAudio.Play(player.GetComponent<PlayerComponent>().soundConfig.walking);
                currentStep = 0f;
            }
        }
    }
}