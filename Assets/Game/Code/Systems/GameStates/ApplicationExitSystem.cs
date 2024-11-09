using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh.Globals.Events;
using UnityEditor;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ApplicationExitSystem))]
public sealed class ApplicationExitSystem : UpdateSystem
{
    public GlobalEvent quitGame;
    public override void OnAwake()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        if (quitGame)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
}