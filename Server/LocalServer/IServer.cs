using System.Threading.Tasks;

namespace LocalServer
{
    public interface IServer
    {
        public void RunServer();
        // public Task StartClientsAccepting();
        // public void StopClientsAccepting();
        public void CloseServer();
    }
}