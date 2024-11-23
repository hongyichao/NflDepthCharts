using NflDepthChartsService;
using NflDepthChartsService.DataModel;

namespace NflDepthChartsServiceTest
{
    public class DepthChartTests
    {
        private DepthChart _depthChart;

        public DepthChartTests() 
        {
            var footballStrategy = new FootballDepthChartStrategy();
            _depthChart = new DepthChart(footballStrategy);
        }

        [Fact]
        public async Task AddPlayerAsync_ShouldAddPlayerToDepthChart()
        {
            // Arrange
            var player = new Player(12, "Tom Brady", "QB");

            // Act
            await _depthChart.AddPlayerAsync(player);

            // Assert
            var chart = await _depthChart.GetFullDepthChartAsync();            
            Assert.Contains(player, chart);
        }

        [Fact]
        public async Task RemovePlayerAsync_ShouldRemovePlayerFromDepthChart()
        {
            // Arrange
            var player = new Player(12, "Tom Brady", "QB");
            await _depthChart.AddPlayerAsync(player, 0);

            // Act
            var removedPlayer = await _depthChart.RemovePlayerAsync(player);

            // Assert
            var chart = await _depthChart.GetFullDepthChartAsync();
            Assert.Equal(player, removedPlayer);
            Assert.DoesNotContain(player, chart);
        }

        [Fact]
        public async Task GetBackupsAsync_ShouldReturnCorrectBackups()
        {
            // Arrange
            var tomBrady = new Player(12, "Tom Brady", "QB");
            var kyleTrask = new Player(2, "Kyle Trask", "QB");

            await _depthChart.AddPlayerAsync(tomBrady, 0);
            await _depthChart.AddPlayerAsync(kyleTrask, 1);

            // Act
            var backups = await _depthChart.GetBackupsAsync(tomBrady);

            // Assert
            Assert.Contains(kyleTrask, backups);
            Assert.Single(backups); // Only one backup expected
        }

        [Fact]
        public async Task GetBackupsAsync_ShouldReturnEmptyList_WhenNoBackupsExist()
        {
            // Arrange
            var tomBrady = new Player(12, "Tom Brady", "QB");
            await _depthChart.AddPlayerAsync(tomBrady);

            // Act
            var backups = await _depthChart.GetBackupsAsync(tomBrady);

            // Assert
            Assert.Empty(backups);
        }

        //    [Fact]
        //    public async Task GetFullDepthChartAsync_ShouldReturnAllPlayers()
        //    {
        //        // Arrange
        //        var tomBrady = new Player(12, "Tom Brady", "QB");
        //        var mikeEvans = new Player(13, "Mike Evans", "WR");

        //        await _depthChart.AddPlayerAsync("QB", tomBrady);
        //        await _depthChart.AddPlayerAsync("WR", mikeEvans);

        //        // Act
        //        var chart = await _depthChart.GetFullDepthChartAsync();

        //        // Assert
        //        Assert.Contains(tomBrady, chart["QB"]);
        //        Assert.Contains(mikeEvans, chart["WR"]);
        //    }

        //    [Fact]
        //    public async Task RemovePlayerAsync_ShouldReturnNull_WhenPlayerDoesNotExist()
        //    {
        //        // Arrange
        //        var tomBrady = new Player(12, "Tom Brady", "QB");

        //        // Act
        //        var result = await _depthChart.RemovePlayerAsync("QB", tomBrady);

        //        // Assert
        //        Assert.Null(result);
        //    }
        }
    }