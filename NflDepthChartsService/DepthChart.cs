

using NflDepthChartsService.DataModel;

namespace NflDepthChartsService
{
    public class DepthChart: IdepthChart
    {
        private readonly List<Player> _players;
        private readonly IDepthChartStrategy _strategy;

        public DepthChart(IDepthChartStrategy strategy)
        {
            _players = new List<Player>();
            _strategy = strategy;
        }

        public async Task AddPlayerAsync(Player player, int? positionDepth = null)
        {
            await _strategy.AddPlayerAsync(_players, player, positionDepth);
        }

        public async Task<Player> GetPlayerAsync(string name) 
        {
            return await Task.Run(()=> 
            { 
                return _players.FirstOrDefault(p => p.Name.ToLower() == name.ToLower()); 
            });
        }


        public async Task<Player> RemovePlayerAsync(Player player)
        {
            return await Task.Run(() =>
            {
                if (_players.Remove(player))
                {
                    return player;
                }
                return null;
            });
        }

        public async Task<List<Player>> GetBackupsAsync(string playerName)
        {
            var player = _players.FirstOrDefault(p => p.Name.ToLower() == playerName.ToLower());

            return await GetBackupsAsync(player);
        }

        public async Task<List<Player>> GetBackupsAsync(Player player)
        {
            return await Task.Run(() =>
            {
                var index = _players.IndexOf(player);
                if (index >= 0 && index < _players.Count - 1)
                {
                    return _players.GetRange(index + 1, _players.Count - index - 1);
                }
                return new List<Player>();
            });
        }

        public async Task<List<Player>> GetFullDepthChartAsync()
        {
            return await Task.Run(() => new List<Player>(_players));
        }
    }

}
