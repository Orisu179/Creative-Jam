namespace State_Machines
{
    public class StateMachine
    {
        private IState CurrentState { get; set; }

        public void Initialize(IState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            if (newState == null || newState == CurrentState)
                return;

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Update() => CurrentState?.Update();
    }
}