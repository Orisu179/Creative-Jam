using System.Threading.Tasks;

namespace State_Machines
{
    public interface IState
    {
        void Enter();       // Called once when switching to this state
        Task Exit();       // Called once when existing
    }
}