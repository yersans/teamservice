using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        [Fact]
        public void CreateMembertoNonexistantTeamReturnsNotFound()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Guid newMemberId = Guid.NewGuid();
            Member newMember = new Member(newMemberId);
            //When
            var result = controller.CreateMember(newMember, teamId);
            //Then
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void GetExistingMemberReturnsMember()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            repository.Add(team);

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);
            //When
            var member = (Member)(controller.GetMember(teamId, memberId) as ObjectResult).Value;
            //Then
            Assert.Equal(newMember.ID, member.ID);
        }

        [Fact]
        public void GetMembersReturnsMembers()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            repository.Add(team);

            Guid firstMemberId = Guid.NewGuid();
            Member newMember = new Member(firstMemberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

            Guid secondMemberId = Guid.NewGuid();
            newMember = new Member(secondMemberId);
            newMember.FirstName = "John";
            newMember.LastName = "Doe";
            controller.CreateMember(newMember, teamId);
            //When
            ICollection<Member> members = (ICollection<Member>)(controller.GetMembers(teamId) as ObjectResult).Value;
            //Then
            Assert.Equal(2, members.Count);
            Assert.NotNull(members.Where(m => m.ID == firstMemberId).First());
            Assert.NotNull(members.Where(m => m.ID == secondMemberId).First());
        }

        [Fact]
        public void GetMembersForNewTeamIsEmpty()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            repository.Add(team);
            //When
            ICollection<Member> members = (ICollection<Member>)(controller.GetMembers(teamId) as ObjectResult).Value;
            //Then
            Assert.Empty(members);
        }

        [Fact]
        public void GetMembersForNonExistantTeamReturnNotFound()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);
            //When
            var result = controller.GetMembers(Guid.NewGuid());
            //Then
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void GetNonExistantTeamReturnsNotFound()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);
            //When
            var result = controller.GetMember(Guid.NewGuid(), Guid.NewGuid());
            //Then
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void GetNonExistantMemberReturnsNotFound()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            repository.Add(team);
            //When
            var result = controller.GetMember(teamId, Guid.NewGuid());
            //Then
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void UpdateMemberOverwrites()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            repository.Add(team);

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

            Member updatedMember = new Member(memberId);
            updatedMember.FirstName = "Bob";
            updatedMember.LastName = "Jones";
            controller.UpdateMember(updatedMember, teamId, memberId);

            team = repository.Get(teamId);
            //When
            Member testMember = team.Members.Where(m => m.ID == memberId).First();
            //Then
            Assert.Equal("Bob", testMember.FirstName);
            Assert.Equal("Jones", testMember.LastName);
        }

        [Fact]
        public void UpdateMembertoNonexistantMemberReturnsNoMatch()
        {
            //Given
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestController", teamId);
            repository.Add(team);

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            controller.CreateMember(newMember, teamId);

            Guid nonMatchedGuid = Guid.NewGuid();
            Member updatedMember = new Member(nonMatchedGuid);
            updatedMember.FirstName = "Bob";

            //When
            var result = controller.UpdateMember(updatedMember, teamId, nonMatchedGuid);
            //Then
            Assert.True(result is NotFoundResult);
        }
    }
}