using Xunit;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamsControllerTest
    {
        [Fact]
        public void QueryTeamListReturnsCorrectTeams()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());
            var rawTeams = (IEnumerable<Team>)(controller.GetAllTeams().Result as ObjectResult).Value;
            List<Team> teams = new List<Team>(rawTeams);
            Assert.Equal(2, teams.Count);
            Assert.Equal("one", teams[0].Name);
            Assert.Equal("two", teams[1].Name);
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