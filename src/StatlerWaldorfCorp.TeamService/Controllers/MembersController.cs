using System;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;

namespace StatlerWaldorfCorp.TeamService
{
    public class MembersController
    {
        private ITeamRepository repository;

        public MembersController(ITeamRepository repo)
        {
            repository = repo;
        }

        public void CreateMember(Member newMember, Guid teamId)
        {
            throw new NotImplementedException();
        }
    }
}