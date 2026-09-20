namespace State_Machines
{
    public class CustomerInteractState : IState
    {
        public CustomerInteractState(GameManager manager)
        {
            // 1. Fade in the customer
            // 2. Add the customer dialogue and callback to the dialogue box
            // 3. Toggle the dialogue UI
            // That's it, the callback will call transition
        }

        public void Exit()
        {
            // 1. Toggle the dialogue UI
            // 2. Translate the customer to the left
        }
    }
}