using System.Collections.Generic;

namespace State_Machines
{
    public class DayStartState : IState
    {
        private readonly GameManager _manager;
        public DayStartState(GameManager manager)
        {
            _manager = manager;
        }
        
        public void Enter()
        {
           _manager.IncrementLoop();
           _manager.ResetCustomer();
           var counter = _manager.CustomerCounter;
           var curCustomer = _manager.Customers[counter];
           _manager.CurDay = new Day{ CurrentCustomer = curCustomer, CurrentDrink = null, SatisfactionLevel =  0, OrderList =  new List<Drink>()};
           _manager._stateMachine.ChangeState(_manager._customerInteractState);
        }

        public void Exit()
        {
        }
    }
}