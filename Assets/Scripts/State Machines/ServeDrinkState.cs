namespace State_Machines
{
    public class ServeDrinkState : IState
    {
        private GameManager _manager; 
        private CustomerSpriteManager csm;
        public ServeDrinkState(GameManager manager, CustomerSpriteManager csm1)
        {
            _manager = manager;
            csm = csm1; 
        }
        
        public void Enter()
        {
            // calculate satisfaction
            int score = _manager.CalculateScore(_manager.CurDay);
            Day curDay = _manager.CurDay;
            curDay.SatisfactionLevel += score;

            // calc dialogue using day.current cust sat
            // setText(DialogueGenerator.GenerateResponse(score, ), callback);

            // add drink to list
            // _manager.CurDayOrderList.Add(CurrentDrink); 
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