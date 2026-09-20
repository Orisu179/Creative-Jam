namespace State_Machines
{
    public class LoopFailState : IState
    {
        private readonly GameManager _manager;
        private readonly IState _failState;
        private readonly IState _nextState;
        public LoopFailState(GameManager manager, IState failState, IState nextState)
        {
            _manager = manager;
            _failState = failState;
            _nextState = nextState;
        }

        public void Enter()
        {
            // 1. Show UI for loop fail
            // UI.ShowLoopFail(HandleContinue);
            // 2. Wait for player input to continue
        }

        public void Exit()
        {
            return;
        }

        private void HandleContinue()
        {
            // Transition to the next state
            if (_manager.CurrentLoop >= _manager.MaxLoop)
            {
                _manager._stateMachine.ChangeState(_failState);
            }
            else
            {
                _manager.NextLoop();
                _manager._stateMachine.ChangeState(_nextState);
            }
        }
    }
}