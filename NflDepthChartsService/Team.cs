
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
            await _depthChart.AddPlayerAsync(player, depth);
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

        //public IDepthChart GetOrCreateDepthChart(string position)
        //{
        //    if (!DepthCharts.ContainsKey(position))
        //    {
        //        DepthCharts[position] = new DepthChart(_depthChartStrategy);
        //    }

        //    return DepthCharts[position];
        //}
    }
}
