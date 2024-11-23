using NflDepthChartsService;
using System.Numerics;

namespace NflDepthChartsServiceTest
{
    public class DepthChartTests
    {
        [Fact]
        public async Task AddPlayerAsync_AddOnePlayer_ShouldAddOnePlayer()
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
        public async Task AddPlayerAsync_AddDuplicatedPlayer_ShouldHandleDuplicatePlayers()
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
        public async Task AddPlayerAsync_WhenGivingNoDepth_ShouldAddPlayersInCorrectOrder()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);

            var position = "QB";

            // Act
            await depthChart.AddPlayerAsync(new Player(12, "Tom Brady", position));
            await depthChart.AddPlayerAsync(new Player(11, "Blaine Gabbert", position));
            await depthChart.AddPlayerAsync(new Player(2, "Kyle Trask", position));


            var playerDictionary = await depthChart.GetFullDepthChartAsync();

            var players = playerDictionary[position];

            // Assert
            Assert.True(players.Count ==3);
            Assert.Equal("Tom Brady", players[0].Name);
            Assert.Equal("Blaine Gabbert", players[1].Name);
            Assert.Equal("Kyle Trask", players[2].Name);
        }

        [Fact]
        public async Task AddPlayerAsync_WhenGivingDepth_ShouldAddPlayersInCorrectOrder()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);

            var position = "QB";

            // Act
            await depthChart.AddPlayerAsync(new Player(12, "Tom Brady", position), 2); //the player's depth will become 0 as the list is empty 
            await depthChart.AddPlayerAsync(new Player(11, "Blaine Gabbert", position),1); 
            await depthChart.AddPlayerAsync(new Player(2, "Kyle Trask", position),0); // push down the 2 player above 
            await depthChart.AddPlayerAsync(new Player(7, "Test Mate", position)); // added the the end of the list


            var playerDictionary = await depthChart.GetFullDepthChartAsync();

            var players = playerDictionary[position];

            // Assert
            Assert.True(players.Count == 4);
            Assert.Equal("Kyle Trask", players[0].Name);
            Assert.Equal("Tom Brady", players[1].Name);
            Assert.Equal("Blaine Gabbert", players[2].Name);
            Assert.Equal("Test Mate", players[3].Name);

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