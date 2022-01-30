using System.Threading.Tasks;

namespace LocalServer
{
    public interface IServer
    {
        public void RunServer();
        public void CloseServer();
    }
}