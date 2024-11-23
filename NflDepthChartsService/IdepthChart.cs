
namespace NflDepthChartsService
{
    public interface IDepthChart
    {
        Task AddPlayerAsync(Player player, int? positionDepth = null);
        Task<Player> GetPlayerAsync(string position, string name);
        Task<Player> RemovePlayerAsync(string position, Player player);
        Task<List<Player>> GetBackupsAsync(string position, string playerName);
        Task<List<Player>> GetBackupsAsync(Player player);
        Task<Dictionary<string, List<Player>>> GetFullDepthChartAsync();
    }
}
