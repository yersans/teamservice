using Xunit;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamsControllerTest
    {
        TeamsController controller = new TeamsController();

        [Fact]
        public void QueryTeamListReturnsCorrectTeams()
        {
            List<Team> teams = new List<Team>(controller.GetAllTeams());
            Assert.Equal(2, teams.Count);
        }

        [Fact]
        public async void CreateTeamAddsTeamToList()
        {
            TeamsController controller = new TeamsController();
            var teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> original = new List<Team>(teams);

            Team t = new Team("sample");
            var result = controller.CreateTeam(t);

            var newTeamsRaw = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> newTeams = new List<Team>(newTeamsRaw);

            Assert.Equal(original.Count + 1, newTeams.Count);

            var sampleTeam = newTeams.FirstOrDefault(team => team.Name == "sample");
            Assert.NotNull(sampleTeam);
        }
    }
}