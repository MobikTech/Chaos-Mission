namespace LocalServer.Settings
{
    public static class ServerSettings
    {
        public static string IP { get; } = "127.0.0.1";
        public static int Port { get; } = 8888;
        public static int MaxPlayers { get; } = 20;
    }
}