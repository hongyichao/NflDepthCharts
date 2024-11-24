
namespace NflDepthChartsService
{
    public interface ISport
    {
        void AddTeam(ITeam team);

        void AddTeams(List<ITeam> teams);

        ITeam GetTeam(string name);
    }
}
