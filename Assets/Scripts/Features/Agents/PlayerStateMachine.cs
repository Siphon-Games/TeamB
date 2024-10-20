using System.Collections.Generic;

public class PlayerStateMachine : StateMachine<PlayerStateEnum, PlayerState>
{
    protected override Dictionary<PlayerStateEnum, PlayerState> states { get; set; }

    public override PlayerStateEnum CurrentState { get; protected set; }

    public override StateMachine<PlayerStateEnum, PlayerState> Create(Agent<PlayerStateEnum, PlayerState> agent)
    {
        states = new();

        // Add all desired states here
        AddState(new PlayerIdleState());

        ChangeState(PlayerStateEnum.IDLE);

        return this;
    }
}
