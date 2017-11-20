using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.TeamService
{
    [Route("[controller]")]
    public class TeamsController : Controller
    {
        ITeamRepository repository;

        public TeamsController(ITeamRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public virtual IActionResult GetAllTeams()
        {
            return this.Ok(repository.GetTeams());
        }

        [HttpPost]
        public IActionResult CreateTeam(Team t)
        {
            repository.AddTeam(t);
            return this.Created($"/teams/{t.ID}", t);
        }

        [HttpGet("{id}")]
        public IActionResult GetTeam(Guid id)
        {
            Team team = repository.Get(id);

            if (team != null)
            {
                return this.Ok(team);
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam(Team team, Guid id)
        {
            team.ID = id;

            if (repository.Update(team) == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(team);
            }
        }
    }
}