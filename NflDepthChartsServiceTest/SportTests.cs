using NflDepthChartsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NflDepthChartsServiceTest
{
    public class SportTests
    {
        [Fact]
        public void AddTeams_ThereAreMoreThanOneTeam_ShouldAddAllTeams()
        {
            // Arrange
            var sport = new Sport("Football");
            var team1 = new Team("team1", "Football", null);
            var team2 = new Team("team2", "Football", null);

            // Act
            sport.AddTeams(new List<ITeam> { team1, team2 });

            // Assert
            Assert.Equal(2, sport.Teams.Count);
            Assert.Contains(sport.Teams, t => t.Name == "team1");
            Assert.Contains(sport.Teams, t => t.Name == "team2");
        }

        [Fact]
        public void AddTeams_WhenAddingNoTeam_ShouldAddNoTeam()
        {
            // Arrange
            var sport = new Sport("Football");

            // Act
            sport.AddTeams(new List<ITeam>());

            // Assert
            Assert.Empty(sport.Teams);
        }

        [Fact]
        public void GetTeam_GiveTeamName_ShouldReturnCorrectTeam()
        {
            // Arrange
            var sport = new Sport("Football");
            var team = new Team("team1", "Football", null);
            sport.AddTeam(team);

            // Act
            var result = sport.GetTeam("team1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("team1", result.Name);
        }

        [Fact]
        public void GetTeam_WhenTeamNotExisting_ReturnNull()
        {
            // Arrange
            var sport = new Sport("Football");

            // Act
            var result = sport.GetTeam("Non Existent Team");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetTeam_WhenNameTeamNameAsNull_ShouldReturnNull() 
        {
            var sport = new Sport("Foorball");
            var team = sport.GetTeam(null);

            Assert.Null(team);
        }

        [Fact]
        public void RemoveTeam_WhenGivenTeamName_ShouldRemoveTheCorrectTeam() 
        {
            var sport = new Sport("Football");
            var team = new Team("team1", "Football", null);
            sport.AddTeam(team);

            var result = sport.RemoveTeam("team1");

            Assert.True(result);
            Assert.Empty(sport.Teams);
        }

        [Fact]
        public void RemoveTeam_WhenGivenWrongTeamName_ShouldReturnFalse()
        {
            var sport = new Sport("Football");
            var team = new Team("team1", "Football", null);
            sport.AddTeam(team);

            var result = sport.RemoveTeam("team2");

            Assert.False(result);
            Assert.NotEmpty(sport.Teams);
        }
    }
}
