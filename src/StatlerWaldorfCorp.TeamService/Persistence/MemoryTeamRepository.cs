using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Team Get(Guid id)
        {
            return teams.FirstOrDefault(t => t.ID == id);
        }

        public Team Update(Team team)
        {
            Team t = this.Delete(team.ID);

            if (t != null)
            {
                t = this.Add(team);
            }

            return t;
        }

        public Team Add(Team team)
        {
            teams.Add(team);
            return team;
        }

        public Team Delete(Guid id)
        {
            var q = teams.Where(t => t.ID == id);
            Team team = null;

            if (q.Count()>0)
            {
                team = q.First();
                teams.Remove(team);
            }

            return team;
        }
    }
}