namespace State_Machines
{
    public interface IState
    {
        void Enter();       // Called once when switching to this state
        void Exit();       // Called once when existing
    }
}