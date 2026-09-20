namespace State_Machines
{
    public interface IState
    {
        void Enter();       // Called once when switching to this state
        void Update();      // Frame-rate dependent logic (input, animation checks)
        void Exit();       // Called once when existing
    }
}