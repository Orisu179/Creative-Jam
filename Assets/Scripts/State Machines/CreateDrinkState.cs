namespace State_Machines
{
    public class CreateDrinkState : IState
    {
        private CreateDrink _createDrink;
        private GameManager _manager;
    
        public CreateDrinkState(GameManager manager, CreateDrink drink)
        {
            _manager = manager;
            _createDrink = drink;
        }

        public void Enter()
        {
            // 1. Fade in the create drink UI
            // TODO: Add drink UI sprite fade in and await

            // 2. Enable ingredients listeners
            // 3. Enable the menu button
        }

        public void Exit()
        {
            _manager.SetCurrentDrink(_createDrink.drinkManager.CreatedDrink);
            _manager.DrinkMenu.CloseAndDisable();
            // TODO: Disable ingredients listeners
            // Ease out the create drink UI
            // Customer move is handled by the next state
        }

        public void HandleDrinkCreated()
        {

            _manager._stateMachine.ChangeState(_manager._serveDrinkState);
        }
    }
}