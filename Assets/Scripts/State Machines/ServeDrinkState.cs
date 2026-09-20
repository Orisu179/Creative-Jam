namespace State_Machines
{
    public class ServeDrinkState : IState
    {
        private GameManager _manager; 
        public ServeDrinkState(GameManager manager)
        {
            _manager = manager;
        }
        
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}