using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
    public class MemoryTeamRepository : ITeamRepository
    {
        protected static ICollection<Team> teams;

        public MemoryTeamRepository()
        {
            if (teams == null)
            {
                teams = new List<Team>();
            }
        }

        public MemoryTeamRepository(ICollection<Team> teamsParam)
        {
            teams = teamsParam;
        }

        public IEnumerable<Team> GetTeams()
        {
            return teams;
        }

        public void AddTeam(Team team)
        {
            teams.Add(team);
        }
    }
}