using System;
using System.Threading.Tasks;
using FluentAssertions;
using Naheulbook.Tests.Functional.Code.Extensions;
using Naheulbook.Tests.Functional.Code.Servers;
using Naheulbook.Tests.Functional.Code.TestServices;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

namespace Naheulbook.Tests.Functional.Code.Steps
{
    [Binding]
    public class UserSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly UserTestService _userTestService;

        public UserSteps(ScenarioContext scenarioContext, UserTestService userTestService)
        {
            _scenarioContext = scenarioContext;
            _userTestService = userTestService;
        }

        [Given("a user identified by a password")]
        public async Task GivenAUserIdentifiedByAPassword()
        {
            var (username, password) = await _userTestService.CreateUserAsync();

            _scenarioContext.SetUsername(username);
            _scenarioContext.SetPassword(password);
        }

        [Given("a JWT for a user")]
        public async Task GivenAUser()
        {
            var (username, password) = await _userTestService.CreateUserAsync();

            _scenarioContext.SetUsername(username);
            _scenarioContext.SetPassword(password);

            var jwt = await _userTestService.GenerateJwtAsync(username, password);
            _scenarioContext.SetJwt(jwt);
        }

        [Given("a JWT for an admin user")]
        public async Task GivenAnAdminUser()
        {
            var (username, password) = await _userTestService.CreateUserAsync();

            _scenarioContext.SetUsername(username);
            _scenarioContext.SetPassword(password);

            await _userTestService.SetUserAdminAsync(username);

            var jwt = await _userTestService.GenerateJwtAsync(username, password);
            _scenarioContext.SetJwt(jwt);
        }

        [Then("the response content contains a valid JWT")]
        public void ThenTheResponseContentContainsAValidJwt()
        {
            var responseContent = _scenarioContext.GetLastHttpResponseContent();
            var response = JObject.Parse(responseContent);

            response.Should().ContainKey("token");
            var jwt = response.Value<string>("token");
            _scenarioContext.SetJwt(jwt);

            var payload = Jose.JWT.Decode<JObject>(jwt, Convert.FromBase64String(NaheulbookApiServer.JwtSigningKey));

            payload.Should().ContainKey("sub");
            payload.Should().ContainKey("exp");
        }
    }
}