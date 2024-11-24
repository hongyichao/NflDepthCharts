
namespace NflDepthChartsService
{
    public class Sport: ISport
    {
        public string Name { get; set; }
        public List<ITeam> Teams { get; private set; }

        public Sport(string name)
        {
            Name = name;
            Teams = new List<ITeam>();
        }

        public void AddTeam(ITeam team)
        {
            Teams.Add(team);
        }

        public void AddTeams(List<ITeam> teams)
        {
            Teams.AddRange(teams);
        }

        public ITeam GetTeam(string name)
        {
            return Teams.FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
