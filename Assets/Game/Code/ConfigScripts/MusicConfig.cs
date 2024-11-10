using Ami.BroAudio;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicConfig", menuName = "Configs/MusicConfig")]
public class MusicConfig : ScriptableObject
{
    public SoundID mainMenuMusic;
    public SoundID gameMusic;
    public SoundID victoryMusic;
}