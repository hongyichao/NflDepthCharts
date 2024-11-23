
namespace NflDepthChartsService
{
    public interface IDepthChartStrategy
    {
        Task AddPlayerAsync(List<Player> players, Player player, int? depth = null);
    }

    public class FootballDepthChartStrategy : IDepthChartStrategy
    {
        public async Task AddPlayerAsync(List<Player> players, Player player, int? depth = null)
        {
            await Task.Run(() =>
            {
                if (depth == null || depth >= players.Count)
                {
                    players.Add(player);
                }
                else
                {
                    players.Insert(depth.Value, player);
                }
            });
        }
    }

    public class BasketballDepthChartStrategy : IDepthChartStrategy
    {
        public async Task AddPlayerAsync(List<Player> players, Player player, int? depth = null)
        {
            // Simplified example for basketball rotations
            await Task.Run(() => players.Add(player));
        }
    }

}
