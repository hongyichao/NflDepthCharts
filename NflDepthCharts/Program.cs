

using NflDepthChartsService;


class Program
{
    static async Task Main(string[] args)
    {
        // Create a sport
        var football = new Sport("Football");

        var footballDepthChartStrategy = new FootballDepthChartStrategy();

        var footballDepthChart = new DepthChart(footballDepthChartStrategy);

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
        var player = await team.GetPlayerFromDepthChart("QB", "Tom Brady");        
        var backups = await team.GetBackupsAsync(player);

        Console.WriteLine($"Backups for {player.Name}:\n {string.Join("\n ", backups)}");




        // Print full depth chart for Buccaneers
        Console.WriteLine("\ngetFullDepthChart() /* Output");
        var depthChart = await team.GetDepthChart().GetFullDepthChartAsync();
        foreach (var entry in depthChart)
        {
            Console.WriteLine($"Position: {entry.Key}");
            foreach (var p in entry.Value)
            {
                Console.WriteLine($"   {p}");
            }
        }
    }

    private static async Task BatchAddPlayersToTeamDepthChart(ITeam team, string position, List<Player> players) 
    {
        foreach (var player in players.Where(p => p.Position.ToLower() == position.ToLower()))
        {
            await team.AddPlayerToDepthChart(player);
        }
    }
}
