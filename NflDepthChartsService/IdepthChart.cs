using NflDepthChartsService.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NflDepthChartsService
{
    public interface IdepthChart
    {
        Task AddPlayerAsync(Player player, int? positionDepth = null);
        Task<Player> RemovePlayerAsync(Player player);
        Task<List<Player>> GetBackupsAsync(Player player);
        Task<List<Player>> GetFullDepthChartAsync();
    }
}
