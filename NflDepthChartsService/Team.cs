
namespace NflDepthChartsService
{
    public class Team: ITeam
    {
        public string Name { get; set; }
        public string Sport { get; set; }
        private IDepthChart _depthChart;

        public Team(string name, string sport, IDepthChart depthChart)
        {
            Name = name;
            Sport = sport;
            _depthChart = depthChart;            
        }

        public async Task AddPlayerToDepthChart(Player player, int? depth = null)
        {
            if (player == null)
                throw new ArgumentNullException("The player cannot be null");

            if (string.IsNullOrWhiteSpace(player.Name))
                throw new InvalidOperationException("The player name cannot be empty");

            await _depthChart.AddPlayerAsync(player, depth);
        }

        public async Task<Player> RemovePlayerFromDepthChart(string position, string name)
        {
            var playerToDelete = await _depthChart.GetPlayerAsync(position, name);
            var deletedPlayer = await _depthChart.RemovePlayerAsync(position, playerToDelete);
            
            return deletedPlayer;
        }

        public async Task<Player> GetPlayerFromDepthChart(string position, string name) 
        {
            return await _depthChart.GetPlayerAsync(position, name);
        }

        public async Task<List<Player>> GetBackupsAsync(string position, string name)
        {
            return await _depthChart.GetBackupsAsync(position, name);
        }

        public async Task<List<Player>> GetBackupsAsync(Player player)
        {
            return await _depthChart.GetBackupsAsync(player);
        }

        public IDepthChart GetDepthChart() 
        {
            return _depthChart;
        }
    }
}
