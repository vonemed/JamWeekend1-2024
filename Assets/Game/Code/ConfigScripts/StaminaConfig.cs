using UnityEngine;

[CreateAssetMenu(fileName = "StaminaConfig", menuName = "Configs/StaminaConfig")]
public class StaminaConfig : ScriptableObject
{
    public float speedMultiplier;
    public float totalStamina;
    public float useStaminaPerSecond;
    public float staminaRecoveryPerSecond;
}