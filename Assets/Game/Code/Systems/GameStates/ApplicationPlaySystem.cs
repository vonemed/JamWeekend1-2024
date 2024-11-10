using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh.Globals.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ApplicationPlaySystem))]
public sealed class ApplicationPlaySystem : UpdateSystem
{
    public GlobalEvent playGame;
    public override void OnAwake()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        if (playGame)
        {
            Cursor.visible = false;
        }
    }
}