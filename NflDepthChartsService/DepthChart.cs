
namespace NflDepthChartsService
{
    public class DepthChart: IDepthChart
    {
        private readonly Dictionary<string, List<Player>> _chartGroups;
        private readonly IDepthChartStrategy _strategy;

        public DepthChart(IDepthChartStrategy strategy)
        {
            _chartGroups = new Dictionary<string, List<Player>>();
            _strategy = strategy;
        }

        public async Task AddPlayerAsync(Player player, int? positionDepth = null)
        {
            

            if (!_chartGroups.ContainsKey(player.Position))
            {
                _chartGroups[player.Position] = new List<Player>();
            }

            await _strategy.AddPlayerAsync(_chartGroups[player.Position], player, positionDepth);
        }

        public async Task<Player> GetPlayerAsync(string position, string name) 
        {
            return await Task.Run(()=> 
            {
                if (!_chartGroups.ContainsKey(position))
                    return null;
                                
                return _chartGroups[position].FirstOrDefault(p=>p.Name.ToLower() == name.ToLower()); 
            });
        }


        public async Task<Player> RemovePlayerAsync(string position, Player player)
        {
            return await Task.Run(() =>
            {
                if (!_chartGroups.ContainsKey(position))
                    return null;

                if (_chartGroups[position].Remove(player))
                {
                    return player;
                }
                return null;
            });
        }

        public async Task<List<Player>> GetBackupsAsync(string position, string playerName)
        {
            var player = await GetPlayerAsync(position, playerName);

            return await GetBackupsAsync(player);
        }

        public async Task<List<Player>> GetBackupsAsync(Player player)
        {
            return await Task.Run(() =>
            {
                if (_chartGroups.ContainsKey(player.Position))
                {
                    var playerList = _chartGroups[player.Position];

                    var index = playerList.IndexOf(player);
                    if (index >= 0 && index < playerList.Count - 1)
                    {
                        return playerList.GetRange(index + 1, playerList.Count - index - 1);
                    }
                }
                
                return new List<Player>();
            });
        }

        public async Task<Dictionary<string, List<Player>>> GetFullDepthChartAsync()
        {
            return await Task.Run(() => _chartGroups);
        }
    }

}
