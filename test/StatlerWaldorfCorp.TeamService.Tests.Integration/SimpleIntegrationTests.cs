using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using StatlerWaldorfCorp.TeamService.Models;
using Xunit;

namespace StatlerWaldorfCorp.TeamService.Tests.Integration
{
    public class SimpleIntegrationTests
    {
        private readonly TestServer testServer;
        private readonly HttpClient testClient;
        private readonly Team teamZombie;

        public SimpleIntegrationTests()
        {
            testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            testClient = testServer.CreateClient();
            teamZombie = new Team
            {
                ID = Guid.NewGuid(),
                Name = "Zombie"
            };
        }

        [Fact]
        public async void TestTeamPostAndGet()
        {
            //Given
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(teamZombie), UnicodeEncoding.UTF8, "application/json");

            //When
            HttpResponseMessage postResponse = await testClient.PostAsync("/teams", stringContent);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await testClient.GetAsync("/teams");
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();
            List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(raw);
            //Then
            Assert.Equal(1, teams.Count);
            Assert.Equal("Zombie", teams[0].Name);
            Assert.Equal(teamZombie.ID, teams[0].ID);
        }
    }
}