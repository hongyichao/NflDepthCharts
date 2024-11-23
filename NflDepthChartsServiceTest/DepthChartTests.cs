using NflDepthChartsService;

namespace NflDepthChartsServiceTest
{
    public class DepthChartTests
    {
        [Fact]
        public async Task AddPlayerAsync_ShouldAddPlayerToCorrectPosition()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var player = new Player(12, "Tom Brady", "QB");

            // Act
            await depthChart.AddPlayerAsync(player);
            var result = await depthChart.GetPlayerAsync("QB", "Tom Brady");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tom Brady", result.Name);
        }

        [Fact]
        public async Task AddPlayerAsync_ShouldHandleDuplicatePlayers()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var player = new Player(12, "Tom Brady", "QB");

            // Act
            await depthChart.AddPlayerAsync(player);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await depthChart.AddPlayerAsync(player);
            });

            // Assert
            Assert.Equal("The player is an existing player", exception.Message);
        }

        [Fact]
        public async Task GetBackupsAsync_ShouldReturnCorrectBackups()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var starter = new Player(12, "Tom Brady", "QB");
            var backup = new Player(11, "Blaine Gabbert", "QB");
            await depthChart.AddPlayerAsync(starter);
            await depthChart.AddPlayerAsync(backup);

            // Act
            var backups = await depthChart.GetBackupsAsync(starter);

            // Assert
            Assert.Single(backups);
            Assert.Equal("Blaine Gabbert", backups[0].Name);
        }

        [Fact]
        public async Task GetBackupsAsync_ShouldHandleNonExistentPosition()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);

            // Act
            var backups = await depthChart.GetBackupsAsync("QB", "Non Existent Player");

            // Assert
            Assert.Empty(backups);
        }

        [Fact]
        public async Task RemovePlayerAsync_ShouldRemovePlayerFromPosition()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var player = new Player(12, "Tom Brady", "QB");
            await depthChart.AddPlayerAsync(player);

            // Act
            var removedPlayer = await depthChart.RemovePlayerAsync("QB", player);
            var result = await depthChart.GetPlayerAsync("QB", "Tom Brady");

            // Assert
            Assert.NotNull(removedPlayer);
            Assert.Equal("Tom Brady", removedPlayer.Name);
            Assert.Null(result);
        }

        [Fact]
        public async Task RemovePlayerAsync_ShouldHandleRemovingNonExistentPlayer()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var player = new Player(12, "Non Existent", "QB");

            // Act
            var removedPlayer = await depthChart.RemovePlayerAsync("QB", player);

            // Assert
            Assert.Null(removedPlayer);
        }

    }
}