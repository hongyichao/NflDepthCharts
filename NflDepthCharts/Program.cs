

using NflDepthChartsService;
using System.Diagnostics;
using System.Numerics;
using System.Xml.Linq;


class Program
{
    static async Task Main(string[] args)
    {
        // Create a sport
        var football = new Sport("Football");

        var footballDepthChartStrategy = new FootballDepthChartStrategy();

        var footballDepthChart = new FootballDepthChart(footballDepthChartStrategy);

        // Create 2 teams
        var teams = new List<ITeam>() 
        {
            new Team("Tampa Bay Buccaneers", "Football", footballDepthChart),
            new Team("Arizona Cardinals", "Football", footballDepthChart)
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
        await GetBackUps(team, "QB", "Tom Brady");
        await GetBackUps(team, "LWR", "Jaelon Darden");
        await GetBackUps(team, "QB", "Mike Evans");
        await GetBackUps(team, "QB", "Blaine Gabbert");
        await GetBackUps(team, "QB", "Kyle Trask");

        // Print full depth chart for Buccaneers
        await PrintFullDepthChart(team);

        var depthChart = team.GetDepthChart();

        //delete player
        Console.WriteLine("\nremove player from DepthChart “WR” MikeEvans");
        var playerToDelete = await depthChart.GetPlayerAsync("LWR", "Mike Evans");
        var deletedPlayer = await depthChart.RemovePlayerAsync("LWR", playerToDelete);
        Console.WriteLine($"{deletedPlayer}");

        // Print full depth chart for Buccaneers
        await PrintFullDepthChart(team);
    }

    private static async Task PrintFullDepthChart(ITeam team) 
    {
        // Print full depth chart for Buccaneers
        Console.WriteLine("\ngetFullDepthChart() /* Output");
        var depthChartDictionary = await team.GetDepthChart().GetFullDepthChartAsync();
        foreach (var entry in depthChartDictionary)
        {
            Console.WriteLine($"Position: {entry.Key}");
            foreach (var p in entry.Value)
            {
                Console.WriteLine($"   {p}");
            }
        }
    }

    private static async Task GetBackUps(ITeam team,string position, string name)
    {
        Console.WriteLine($"Backups for {name}:");

        var player = await team.GetPlayerFromDepthChart(position, name);
        if (player == null)
        { 
            Console.WriteLine($"<NO LIST>\n");
            return;
        }

        var backups = await team.GetBackupsAsync(player);
        if (backups == null || backups.Count ==0)
        { 
            Console.WriteLine($"<NO LIST>\n");
            return;
        }

        Console.WriteLine($"{string.Join("\n", backups)}\n");
    }
    private static async Task BatchAddPlayersToTeamDepthChart(ITeam team, string position, List<Player> players) 
    {
        foreach (var player in players.Where(p => p.Position.ToLower() == position.ToLower()))
        {
            await team.AddPlayerToDepthChart(player);
        }
    }
}
