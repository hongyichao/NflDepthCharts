using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NflDepthChartsService.DataModel
{
    public class Team
    {
        public string Name { get; set; }
        public string Sport { get; set; }
        public Dictionary<string, DepthChart> DepthCharts { get; private set; }

        private IDepthChartStrategy _depthChartStrategy { get; set; }

        public Team(string name, string sport, IDepthChartStrategy depthChartStrategy)
        {
            Name = name;
            Sport = sport;
            DepthCharts = new Dictionary<string, DepthChart>();
            _depthChartStrategy = depthChartStrategy;
        }

        public async Task AddPlayerToDepthChart(Player player, int? depth)
        {
            await GetOrCreateDepthChart(player.Position).AddPlayerAsync(player, depth);
        }

        public DepthChart GetOrCreateDepthChart(string position)
        {
            if (!DepthCharts.ContainsKey(position))
            {
                DepthCharts[position] = new DepthChart(_depthChartStrategy);
            }

            return DepthCharts[position];
        }
    }
}
