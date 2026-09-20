using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace State_Machines
{
    public class LoopFailState : IState
    {
        private readonly GameManager _manager;
        private readonly IState _dayStartState;
        public LoopFailState(GameManager manager, IState dayStartState)
        {
            _manager = manager;
            _dayStartState = dayStartState;
        }
        public void Enter()
        {
            HandleContinue();

            // 2. show fail menu
            _manager.SetFailMenuText((_manager.GetMaxLoop() - _manager.CurLoop));
            _manager.SetFailMenuList(_manager.CurDay.OrderList);
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }

        private void HandleContinue()
        {
            if (_manager.CurLoop >= _manager.GetMaxLoop())
            {
                // game overscreen
                // update global state
                SceneManager.LoadScene("GameOverScene");
                return;
            }
            // Restart
            _manager.IncrementLoop();
            _manager._stateMachine.ChangeState(_dayStartState);
        }
    }
}