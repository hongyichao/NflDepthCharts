
namespace NflDepthChartsService
{
    public abstract class DepthChart: IDepthChart
    {
        protected readonly Dictionary<string, List<Player>> _chartGroups;
        protected readonly IDepthChartStrategy _strategy;

        public DepthChart(IDepthChartStrategy strategy)
        {
            _chartGroups = new Dictionary<string, List<Player>>();
            _strategy = strategy;
        }

        public abstract Task AddPlayerAsync(Player player, int? positionDepth = null);
        
        public virtual async Task<Player> GetPlayerAsync(string position, string name) 
        {
            return await Task.Run(()=> 
            {
                if (!_chartGroups.ContainsKey(position))
                    return null;
                                
                return _chartGroups[position].FirstOrDefault(p=>p.Name.ToLower() == name.ToLower()); 
            });
        }

        public virtual async Task<Player> RemovePlayerAsync(string position, Player player)
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

        public virtual async Task<List<Player>> GetBackupsAsync(string position, string playerName)
        {
            var player = await GetPlayerAsync(position, playerName);

            return await GetBackupsAsync(player);
        }

        public virtual async Task<List<Player>> GetBackupsAsync(Player player)
        {
            return await Task.Run(() =>
            {
                if (player!=null && _chartGroups.ContainsKey(player.Position))
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
