using LocalServer.Settings;

namespace LocalServer
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            IServer server = new Server(
                ServerSettings.IP, 
                ServerSettings.Port, 
                ServerSettings.MaxPlayers);
            
            server.RunServer();
        }
    }
}