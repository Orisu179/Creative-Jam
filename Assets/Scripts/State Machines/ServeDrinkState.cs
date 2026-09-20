namespace State_Machines
{
    public class ServeDrinkState : IState
    {
        private readonly GameManager _manager; 
        private Day d; 
        private CustomerSpriteManager csm;
        public ServeDrinkState(GameManager manager, Day d1, CustomerSpriteManager csm1)
        {
            _manager = manager;
            d = d1; 
            csm = csm1; 
        }
        
        public void Enter()
        {
            // calculate satisfaction
            int score = _manager.CalculateScore(d);
            d.SatisfactionLevel += score;

            // calc dialogue using day.current cust sat
            // setText(DialogueGenerator.GenerateResponse(score, ), callback);

            // add drink to list
            // d.OrderList.Add(CurrentDrink); 
            // call settext
            // call toggle
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
            // disable dialogue. ie. call dialogue.toggle
            // sprite.fade out 
        }

        // handle callback from settext - ie. manager . next state
    }
}