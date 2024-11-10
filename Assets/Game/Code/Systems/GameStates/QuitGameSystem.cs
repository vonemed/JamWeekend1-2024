using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh.Globals.Events;
using Scellecs.Morpeh;
using Ami.BroAudio;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(QuitGameSystem))]
public sealed class QuitGameSystem : UpdateSystem {
    public GlobalEvent quitEvent;
    public Filter playlist;
    public override void OnAwake() {
        playlist = this.World.Filter.With<MusicComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        if(quitEvent)
        {
            var musicComponent = playlist.FirstOrDefault().GetComponent<MusicComponent>();

            BroAudio.Play(musicComponent.musicConfig.mainMenuMusic).AsBGM();
        }
    }
}