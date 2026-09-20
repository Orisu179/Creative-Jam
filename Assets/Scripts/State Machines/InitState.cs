namespace State_Machines
{
    public class InitState : IState
    {
        private readonly GameManager _manager;
        
        public InitState(GameManager manager)
        {
            _manager = manager;
        }
        
        public void Enter()
        {
            GlobalFields.Instance.score = 0.0f;
            GlobalFields.Instance.State = null;
            _manager.GenerateCustomers();
            _manager.ResetLoop();
            _manager._stateMachine.ChangeState(_manager._dayStartState);
        }

        public void Exit()
        {
            
        }
    }
}