using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh.Globals.Events;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PauseGameSystem))]
public sealed class PauseGameSystem : UpdateSystem {
    public GlobalEvent pauseEvent;
    public Filter pauseUI;
    public override void OnAwake() {
        pauseUI = this.World.Filter.With<PauseGameUIComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        if(pauseEvent)
        {
            foreach (var ui in pauseUI)
            {
                ui.GetComponent<PauseGameUIComponent>().pauseGameUI.SetActive(true);
            }
        }
    }
}