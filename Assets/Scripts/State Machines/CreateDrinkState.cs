namespace State_Machines
{
    public class CreateDrinkState : IState
    {
        private CreateDrink _createDrink;
        public CreateDrinkState(GameManager manager,CreateDrink drink)
        {
            _createDrink = drink;
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