using Xunit;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamsControllerTest
    {
        TeamsController controller = new TeamsController();

        public void QueryTeamListReturnsCorrectTeams()
        {
            List<Team> teams = new List<Team>(controller.GetAllTeams());
        }
    }
}