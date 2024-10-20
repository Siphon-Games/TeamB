public class NonHostileEntity : Agent<EntityState, NonHostileEntityState>
{
    public override StateMachine<EntityState, NonHostileEntityState> StateMachine {  get; set; }

    protected override void InitStateMachine()
    {
        StateMachine = gameObject.AddComponent<StateMachine<EntityState, NonHostileEntityState>>();
        StateMachine.Create(this);
    }
}
