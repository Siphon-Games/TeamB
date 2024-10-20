using UnityEngine;

public class Player : Agent<PlayerStateEnum, PlayerState>, IControllerData<PlayerControllerData>
{
    public override StateMachine<PlayerStateEnum, PlayerState> StateMachine { get; set; }

    [field: SerializeField] public PlayerControllerData Data { get; set; }

    public Animator Animator;
    public Rigidbody Rigidbody;
    public SpriteRenderer SpriteRenderer;

    protected override void InitStateMachine()
    {
        StateMachine = gameObject.AddComponent<PlayerStateMachine>();
        StateMachine.Create(this);
    }
}
