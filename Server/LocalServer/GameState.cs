using System.Collections.Generic;

namespace LocalServer
{
    public class GameState
    {
        private List<Player> _players = new List<Player>();

        public GameState()
        {
            
        }


        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }
    }
}