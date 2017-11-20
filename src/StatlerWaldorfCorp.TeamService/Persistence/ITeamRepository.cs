using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetTeams();
        void AddTeam(Team team);
        Team Get(Guid id);
        Team Update(Team team);
        Team Delete(Guid id);
    }
}