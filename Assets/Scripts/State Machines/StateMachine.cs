using System.Threading.Tasks;

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

        public async Task ChangeState(IState newState)
        {
            if (newState == null || newState == CurrentState)
                return;

            await CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}