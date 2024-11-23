using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NflDepthChartsService.DataModel
{
    public class Sport
    {
        public string Name { get; set; }
        public List<Team> Teams { get; private set; }

        public Sport(string name)
        {
            Name = name;
            Teams = new List<Team>();
        }

        public void AddTeam(Team team)
        {
            Teams.Add(team);
        }

        public void AddTeams(List<Team> teams) 
        {
            Teams.AddRange(teams); 
        }

        public Team GetTeam(string name)
        {
            return Teams.FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
