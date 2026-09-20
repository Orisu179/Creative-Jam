using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace State_Machines
{
    public class DayStartState : IState
    {
        private readonly GameManager _manager;
        private readonly IState _nextState;
        public DayStartState(GameManager manager, IState nextState)
        {
            _manager = manager;
            _nextState = nextState;
        }

        public void Enter()
        {
            if (_manager.CurLoop > 0)
            {
               SoundManager.Instance.PostEvent(SoundManager.SoundEvent.PlayRewindMusic); 
            }
            Debug.Log("Entering Day start state");
            _manager.ResetCustomer();
            var counter = _manager.CustomerCounter;
            var curCustomer = _manager.Customers[counter];
            _manager.CurDay = new Day { CurrentCustomer = curCustomer, CurrentDrink = null, SatisfactionLevel = 0, OrderList = new List<Drink>() };
            _manager.SetSatisfactionLevel(0);
            _manager._stateMachine.ChangeState(_nextState);
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }
    }
}