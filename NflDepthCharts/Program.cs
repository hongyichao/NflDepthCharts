

using NflDepthChartsService;
using NflDepthChartsService.DataModel;
using System.Numerics;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        // Create a sport
        var football = new Sport("Football");

        var footballDepthChartStrategy = new FootballDepthChartStrategy();

        // Create 2 teams
        var teams = new List<Team>() 
        {
            new Team("Tampa Bay Buccaneers", "Football", footballDepthChartStrategy),
            new Team("Arizona Cardinals", "Football", footballDepthChartStrategy)
        };

        //add teams into sport
        football.AddTeams(teams);

        //create players
        var players = new List<Player>()
        {
            new Player(12, "Tom Brady", "QB"),
            new Player(11, "Blaine Gabbert", "QB"),
            new Player(2, "Kyle Trask", "QB"),

            new Player(13, "Mike Evans", "LWR"),
            new Player(1, "Jaelon Darden", "LWR"),
            new Player(10, "Scott Miller", "LWR")
        };


        var team = football.GetTeam("Tampa Bay Buccaneers");

        //create 2 depth charts
        await BatchAddPlayersToTeamDepthChart(team, "QB", players);
        await BatchAddPlayersToTeamDepthChart(team, "LWR", players);


        // Retrieve backups
        var qbDepthChart = team.GetOrCreateDepthChart("QB");
        var player = await qbDepthChart.GetPlayerAsync("Tom Brady");
        var backups = await qbDepthChart.GetBackupsAsync(player);
        Console.WriteLine($"Backups for {player.Name}:\n {string.Join("\n ", backups)}");




        // Print full depth chart for Buccaneers
        Console.WriteLine("\ngetFullDepthChart() /* Output");
        foreach (var depthChart in team.DepthCharts) 
        {
            var fullDepthChart = await depthChart.Value.GetFullDepthChartAsync();
            Console.WriteLine($"{depthChart.Key} -- {string.Join(", ", fullDepthChart)}");
        }

        // Print all teams in Football
        Console.WriteLine("\nAll teams in Football:");
        foreach (var t in football.Teams)
        {
            Console.WriteLine($"- {t.Name}");
        }
    }

    private static async Task BatchAddPlayersToTeamDepthChart(Team team, string position, List<Player> players) 
    {
        foreach (var player in players.Where(p => p.Position.ToLower() == position.ToLower()))
        {
            await team.AddPlayerToDepthChart(player);
        }
    }
}
