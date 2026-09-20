using DG.Tweening;

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
            // 1. Fade in the customer
            _customerSpriteManager.SetSprite(_manager.CurDay.CurrentCustomer.Species, _manager.CurDay.CurrentCustomer.Accessory);
            Tween fadeIn = _customerSpriteManager.FadeIn();
            // 2. Add the customer dialogue and callback to the dialogue box
            // Dialogue.SetDialogue(_day.CurrentCustomer.Dialogue, HandleDialogueComplete);

            await fadeIn.AsyncWaitForCompletion();
            // 3. Toggle the dialogue UI
            // Dialogue.Toggle();
            // That's it, the callback will call transition
        }

        public void Exit()
        {
            // 1. Toggle the dialogue UI
            // DialogueBox.Toggle();
            // 2. Translate the customer to the left
            _customerSpriteManager.MoveLeft();
        }

        private void HandleDialogueComplete()
        {
            // Transition to the next state
            _manager._stateMachine.ChangeState(_nextState);
        }
    }
}