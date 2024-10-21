using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerControllerData", menuName = "Scriptables/Controller/Player")]
public class PlayerControllerData : AgentControllerData
{
    [Header("Movement")]
    public float WalkSpeed, SprintSpeed, CrouchSpeed, StealthSpeed;
    public float Acceleration, Decceleration;
    public float VelocityPower, FrictionAmount;

    [Header("Gravity")]
    public float FallGravityMultiplier;
    public float GravityScale;
    public float TerminalVelocity;

    [Header("Idle")]
    public float IdleThreshold;

    [Header("Other")]
    public float HurtForce;
    public float InvulnerabilityDuration;
    public Material FlashMaterial;
    public float FlashDuration;
}