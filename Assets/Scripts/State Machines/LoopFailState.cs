namespace State_Machines
{
    public class LoopFailState : IState
    {
        private readonly GameManager _manager;
        public LoopFailState(GameManager manager)
        {
            _manager = manager;
        }

        public void Enter()
        {
            // 1. Show UI for loop fail
            // UI.ShowLoopFail(_manager.NextLoop);
            // 2. Wait for player input to continue
        }

        public void Exit()
        {
        }
    }
}