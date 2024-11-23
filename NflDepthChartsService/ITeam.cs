
namespace NflDepthChartsService
{
    public interface ITeam
    {
        string Name { get; set; }

        Task AddPlayerToDepthChart(Player player, int? depth = null);
        Task<Player> GetPlayerFromDepthChart(string position, string name);

        Task<List<Player>> GetBackupsAsync(string position, string name);
        Task<List<Player>> GetBackupsAsync(Player player);
        IDepthChart GetDepthChart();
    }
}
