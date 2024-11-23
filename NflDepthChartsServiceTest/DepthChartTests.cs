using NflDepthChartsService;

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
        public async Task AddPlayer_ShouldAddToDepthChart_WhenPositionDepthIsNull()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var player = new Player(1, "Test Player", "QB");

            // Act
            await depthChart.AddPlayerAsync(player);

            // Assert
            var fullDepthChart = await depthChart.GetFullDepthChartAsync();
            Assert.Contains(player, fullDepthChart);
        }

        [Fact]
        public async Task AddPlayer_ShouldAddAtSpecificDepth_WhenPositionDepthIsProvided()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var player1 = new Player(1, "Player 1", "QB");
            var player2 = new Player(2, "Player 2", "QB");
            await depthChart.AddPlayerAsync(player1);

            // Act
            await depthChart.AddPlayerAsync(player2, 0);

            // Assert
            var fullDepthChart = await depthChart.GetFullDepthChartAsync();
            Assert.Equal(player2, fullDepthChart[0]);
            Assert.Equal(player1, fullDepthChart[1]);
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
        public async Task RemovePlayer_ShouldRemoveFromDepthChart()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var player = new Player(1, "Test Player", "QB");
            await depthChart.AddPlayerAsync(player);

            // Act
            var removedPlayer = await depthChart.RemovePlayerAsync(player);

            // Assert
            var fullDepthChart = await depthChart.GetFullDepthChartAsync();
            Assert.DoesNotContain(player, fullDepthChart);
            Assert.Equal(player, removedPlayer);
        }

        [Fact]
        public async Task GetBackups_ShouldReturnCorrectBackups()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var player1 = new Player(1, "Player 1", "QB");
            var player2 = new Player(2, "Player 2", "QB");
            var player3 = new Player(3, "Player 3", "QB");

            await depthChart.AddPlayerAsync(player1);
            await depthChart.AddPlayerAsync(player2);
            await depthChart.AddPlayerAsync(player3);

            // Act
            var backups = await depthChart.GetBackupsAsync(player1);

            // Assert
            Assert.Contains(player2, backups);
            Assert.Contains(player3, backups);
        }

        [Fact]
        public async Task GetBackups_ShouldReturnEmpty_WhenNoBackupsExist()
        {
            // Arrange
            var strategy = new FootballDepthChartStrategy();
            var depthChart = new DepthChart(strategy);
            var player = new Player(1, "Player 1", "QB");
            await depthChart.AddPlayerAsync(player);

            // Act
            var backups = await depthChart.GetBackupsAsync(player);

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