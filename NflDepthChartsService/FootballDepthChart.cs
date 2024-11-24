
namespace NflDepthChartsService
{
    public class FootballDepthChart: DepthChart
    {
        public FootballDepthChart(IDepthChartStrategy strategy) : base(strategy) 
        {
        }

        public override async Task AddPlayerAsync(Player player, int? positionDepth = null)
        {
            if(player == null)
                throw new ArgumentNullException("The player cannot be null");

            if (string.IsNullOrWhiteSpace(player.Name))
                throw new InvalidOperationException("The player name cannot be empty");
            
            var existingPlayer = await GetPlayerAsync(player.Position, player.Name);

            if (existingPlayer != null)
            {
                throw new InvalidOperationException("The player is an existing player");
    }
            else 
            {
                if (!_chartGroups.ContainsKey(player.Position))
                {
                    _chartGroups[player.Position] = new List<Player>();
                }

                await _strategy.AddPlayerAsync(_chartGroups[player.Position], player, positionDepth);
            }
        }
    }
}
