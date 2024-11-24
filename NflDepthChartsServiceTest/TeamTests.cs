
using NflDepthChartsService;

namespace NflDepthChartsServiceTest
{
    public class TeamTests
    {
        [Fact]
        public async Task AddPlayerToDepthChart_ShouldAddPlayerCorrectly_WhenGivePlayer()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new FootballDepthChart(strategy);
            var team = new Team("team1", "Football", depthChart);
            var player = new Player(12, "Tom Brady", "QB");

            // Act
            await team.AddPlayerToDepthChart(player);
            var result = await team.GetPlayerFromDepthChart("QB", "Tom Brady");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tom Brady", result.Name);
        }

        [Fact]
        public async Task AddPlayerToDepthChart_ShouldThrowException_WhenPlayerIsNull()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new FootballDepthChart(strategy);
            var team = new Team("team1", "Football", depthChart);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => team.AddPlayerToDepthChart(null));
        }

        [Fact]
        public async Task GetBackupsAsync_ShouldReturnBackupsCorrectly()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new FootballDepthChart(strategy);
            var team = new Team("team1", "Football", depthChart);
            var starter = new Player(12, "Tom Brady", "QB");
            var backup = new Player(11, "Blaine Gabbert", "QB");
            await team.AddPlayerToDepthChart(starter);
            await team.AddPlayerToDepthChart(backup);

            // Act
            var backups = await team.GetBackupsAsync(starter);

            // Assert
            Assert.Single(backups);
            Assert.Equal("Blaine Gabbert", backups[0].Name);
        }

        [Fact]
        public async Task GetBackupsAsync_ShouldHandleNonExistentPlayer()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new FootballDepthChart(strategy);
            var team = new Team("Tampa Bay Buccaneers", "Football", depthChart);

            // Act
            var backups = await team.GetBackupsAsync("QB", "Non Existent Player");

            // Assert
            Assert.Empty(backups);
        }
    }
}
