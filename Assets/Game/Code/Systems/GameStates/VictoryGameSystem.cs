using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh.Globals.Events;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(VictoryGameSystem))]
public sealed class VictoryGameSystem : UpdateSystem {
    public GlobalEvent victoryEvent;

    private Filter victoryUI;
    private Filter interactables;
    public override void OnAwake() {
        victoryUI = this.World.Filter.With<VictoryUIComponent>().Build();
        interactables = this.World.Filter.With<InteractableComponent>().With<InteractablePlacedComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {

        if(victoryEvent)
        {
            Cursor.visible = true;
            
            foreach (var ui in victoryUI)
            {
                var uiComponent = ui.GetComponent<VictoryUIComponent>();

                uiComponent.canvas.enabled = true;

                foreach (var cozyItem in uiComponent.cozyItems)
                {
                    cozyItem.gameObject.SetActive(false);
                }

                var i = 0;
                var totalCoziness = 0;

                foreach (var interactable in interactables)
                {
                    var interactableComponent = interactable.GetComponent<InteractableComponent>();
                    uiComponent.cozyItems[i].gameObject.SetActive(true);
                    uiComponent.cozyItems[i].itemName.SetText(interactableComponent.interactableData.objectName);
                    uiComponent.cozyItems[i].cozyValue.SetText(interactableComponent.interactableData.cozyValue.ToString());

                    i++;
                    totalCoziness += interactableComponent.interactableData.cozyValue;
                }

                uiComponent.totalCozy.SetText(totalCoziness.ToString());
            }
        }
    }
}