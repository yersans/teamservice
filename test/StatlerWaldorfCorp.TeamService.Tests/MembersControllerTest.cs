using System;
using StatlerWaldorfCorp.TeamService;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using Xunit;

namespace StatlerWaldorfCorp.TeamService
{
    public class MembersControllerTest
    {
        [Fact]
        public void CreateMemberAddsTeamToList()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestController", teamId);
            repository.Add(team);

            Guid newMemberId = Guid.NewGuid();
            Member newMember = new Member(newMemberId);
            controller.CreateMember(newMember, teamId);
            //When
            team = repository.Get(teamId);
            //Then
            Assert.True(team.Members.Contains(newMember));
        }
    }
}