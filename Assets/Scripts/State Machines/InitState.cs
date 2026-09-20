using UnityEngine;
using System.Threading.Tasks;

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
            Debug.Log("Entering Init state");
            GlobalFields.Instance.Score = 0.0f;
            GlobalFields.Instance.State = null;
            GlobalFields.Instance.MouseInteractable = false;
            _manager.SetFailMenuActive(false);
            _manager.GenerateCustomers();
            _manager.ResetLoop();

            TextBoxManager.Instance.SetDisabled(true, 0f);
            SoundManager.Instance.PostEvent(SoundManager.SoundEvent.PlayLoopZeroMusic);
            _manager._stateMachine.ChangeState(_manager._dayStartState);
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }
    }
}