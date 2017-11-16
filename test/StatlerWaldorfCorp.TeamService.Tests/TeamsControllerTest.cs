using Xunit;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamsControllerTest
    {
        TeamsController controller = new TeamsController();

        [Fact]
        public void QueryTeamListReturnsCorrectTeams()
        {
            List<Team> teams = new List<Team>(controller.GetAllTeams());
            Assert.Equal(teams.Count, 2);
        }
    }
}