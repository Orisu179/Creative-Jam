namespace State_Machines
{
    public class LoopFailState : IState
    {
        private readonly GameManager _manager;
        private readonly IState _dayStartState;
        private readonly IState _gameOverState;
        public LoopFailState(GameManager manager, IState dayStartState, IState gameOverState)
        {
            _manager = manager;
            _dayStartState = dayStartState;
            _gameOverState = gameOverState;
        }

        public void Enter()
        {
            // 1. Show UI for loop fail
            // UI.ShowLoopFail(_manager.NextLoop);
        }

        public void Exit()
        {
        }

        private void HandleContinue()
        {
            if (_manager.CurLoop >= _manager.GetMaxLoop())
            {
                // game overscreen
                _manager._stateMachine.ChangeState(_gameOverState);
            }
            // Restart
            _manager.IncrementLoop();
            _manager._stateMachine.ChangeState(_dayStartState);
        }
    }
}