using Xunit;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamsControllerTest
    {
        [Fact]
        public void QueryTeamListReturnsCorrectTeams()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());
            var rawTeams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> teams = new List<Team>(rawTeams);
            Assert.Equal(2, teams.Count);
            Assert.Equal("one", teams[0].Name);
            Assert.Equal("two", teams[1].Name);
        }

        [Fact]
        public void CreateTeamAddsTeamToList()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());
            var teams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> original = new List<Team>(teams);

            Team t = new Team("sample");
            var result = controller.CreateTeam(t);
            Assert.Equal(201, (result as ObjectResult).StatusCode);

            var newTeamsRaw = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> newTeams = new List<Team>(newTeamsRaw);

            Assert.Equal(original.Count + 1, newTeams.Count);

            var sampleTeam = newTeams.FirstOrDefault(team => team.Name == "sample");
            Assert.NotNull(sampleTeam);
        }

        [Fact]
        public void GetTeamRetrievesTeam()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            string sampleName = "sample";
            Guid id = Guid.NewGuid();
            Team sampleTeam = new Team(sampleName, id);
            controller.CreateTeam(sampleTeam);

            Team retrievedTeam = (Team)(controller.GetTeam(id) as ObjectResult).Value;
            Assert.Equal(sampleName, retrievedTeam.Name);
            Assert.Equal(id, retrievedTeam.ID);
        }

        [Fact]
        public void GetNonExistentTeamReturnsNotFound()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            Guid id = Guid.NewGuid();
            var team = controller.GetTeam(id);
            Assert.True(team is NotFoundResult);
        }

        [Fact]
        public void UpdateTeamModifiesTeamToList()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            Guid id = Guid.NewGuid();
            Team team = new Team("sample", id);
            controller.CreateTeam(team);

            Team newTeam =  new Team("sample2", id);
            controller.UpdateTeam(newTeam, id);

            var newTeamsRaw = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> newTeams = new List<Team>(newTeamsRaw);
            var sampleTeam = newTeams.FirstOrDefault(t => t.Name == "sample");
            Assert.Null(sampleTeam);

            Team retrievedTeam = (Team)(controller.GetTeam(id) as ObjectResult).Value;
            Assert.Equal("sample2", retrievedTeam.Name);
        }

        [Fact]
        public void UpdateNonExistentTeamReturnsNotFound()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            Guid newTeamId = Guid.NewGuid();
            Team newTeam = new Team("New Team", newTeamId);

            var result = controller.UpdateTeam(newTeam, newTeamId);
            Assert.True(result is NotFoundResult);
        }
    }
}