using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<TEnum, TState> : MonoBehaviour where TEnum : Enum where TState : BaseState<TEnum>
{
    protected abstract Dictionary<TEnum, TState> states { get; set; }

    public abstract TEnum CurrentState { get; protected set; }

    public abstract StateMachine<TEnum, TState> Create(Agent<TEnum, TState> agent);

    protected void AddState(BaseState<TEnum> state)
    {
        states[state.State] = (TState)state;
    }

    protected void ChangeState(TEnum state)
    {
        if (CurrentState.Equals(state)) return;

        states[CurrentState].ExitState();

        CurrentState = state;

        states[CurrentState].EnterState();
    }
}

public abstract class BaseState<TEnum> where TEnum : Enum
{
    public abstract TEnum State { get; }

    public void EnterState()
    {
        Debug.Log($"Entered {State.GetType().Name}");
    }

    public void ExitState()
    {
        Debug.Log($"Exited {State.GetType().Name}");
    }
}

public abstract class PlayerState : BaseState<PlayerStateEnum> { }

public class PlayerIdleState : PlayerState
{
    public override PlayerStateEnum State => PlayerStateEnum.IDLE;
}

public abstract class NonHostileEntityState : BaseState<EntityState> { }

public abstract class HostileEntityState : BaseState<EntityState> { }

public enum PlayerStateEnum
{
    IDLE, WALK, RUN
}

public enum EntityState
{
    IDLE, WALK, RUN, SLEEP
}
