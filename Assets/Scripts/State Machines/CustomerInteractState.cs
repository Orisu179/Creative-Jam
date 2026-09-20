using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace State_Machines
{
    public class CustomerInteractState : IState
    {
        private readonly GameManager _manager;
        private readonly CustomerSpriteManager _customerSpriteManager;
        private readonly IState _nextState;

        public CustomerInteractState(GameManager manager, CustomerSpriteManager customerSpriteManager, IState nextState)
        {
            _manager = manager;
            _customerSpriteManager = customerSpriteManager;
            _nextState = nextState;
        }

        public async void Enter()
        {
            Debug.Log("CustomerInteractState: Enter");
            // 1. Fade in the customer
            Debug.Log($"CustomerInteractState: {_manager.CurLoop} loop {_manager.CustomerCounter} customer");
            Debug.Log($"CustomerInteractState: Current customer is {_manager.CurDay.CurrentCustomer.Species} with accessory {_manager.CurDay.CurrentCustomer.Accessory}");
            _customerSpriteManager.SetSprite(_manager.CurDay.CurrentCustomer.Species, _manager.CurDay.CurrentCustomer.Accessory);
            Tween fadeIn = _customerSpriteManager.FadeIn();
            await fadeIn.AsyncWaitForCompletion();

            // 2. Add the customer dialogue and callback to the dialogue box
            // Dialogue.SetDialogue(_day.CurrentCustomer.Dialogue, HandleDialogueComplete);
            Debug.Log("Starting chatbox");
            // 3. Toggle the dialogue UI
            TextBoxManager.Instance.SetDialogue(_manager.CurDay.CurrentCustomer.Dialogue, HandleDialogueComplete);
            await TextBoxManager.Instance.SetDisabled(false, 1.0f);
            // Fade in complete
            // That's it, the callback will call transition
        }

        public async Task Exit()
        {
            // 1. Toggle the dialogue UI
            await TextBoxManager.Instance.SetDisabled(true);

            // 2. Translate the customer to the left
            await _customerSpriteManager.MoveLeft().AsyncWaitForCompletion();
        }

        private void HandleDialogueComplete()
        {
            // Transition to the next state
            _manager._stateMachine.ChangeState(_nextState);
        }
    }
}