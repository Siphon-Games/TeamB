using TMPro;
using UnityEngine;

public class EnemyMovement2D : NpcMovement2D
{
    CharacterController2D target;

    public override void InitializeNpc()
    {
        base.InitializeNpc();
        target = FindAnyObjectByType<CharacterController2D>();
    }

    public override Vector3 GetNewTargetPosition()
    {
        return target.transform.position;
    }
}
