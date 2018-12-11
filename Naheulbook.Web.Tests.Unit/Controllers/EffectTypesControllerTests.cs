using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Naheulbook.Core.Exceptions;
using Naheulbook.Core.Models;
using Naheulbook.Core.Services;
using Naheulbook.Data.Models;
using Naheulbook.Requests.Requests;
using Naheulbook.Web.Controllers;
using Naheulbook.Web.Exceptions;
using Naheulbook.Web.Responses;
using NSubstitute;
using NUnit.Framework;

namespace Naheulbook.Web.Tests.Unit.Controllers
{
    public class EffectTypesControllerTests : BaseControllerTests
    {
        private IEffectService _effectService;
        private IMapper _mapper;
        private EffectTypesController _effectTypesController;

        [SetUp]
        public void SetUp()
        {
            _effectService = Substitute.For<IEffectService>();
            _mapper = Substitute.For<IMapper>();
            _effectTypesController = new EffectTypesController(_effectService, _mapper);
            MockHttpContext(_effectTypesController);
        }

        [Test]
        public async Task PostCreateType_CallEffectService()
        {
            var createEffectTypeRequest = new CreateEffectTypeRequest();
            var effectType = new EffectType();
            var effectTypeResponse = new EffectTypeResponse();

            _effectService.CreateEffectTypeAsync(ExecutionContext, createEffectTypeRequest)
                .Returns(effectType);
            _mapper.Map<EffectTypeResponse>(effectType)
                .Returns(effectTypeResponse);

            var result = await _effectTypesController.PostCreateTypeAsync(createEffectTypeRequest);

            result.StatusCode.Should().Be(201);
            result.Value.Should().Be(effectTypeResponse);
        }

        [Test]
        public void PostCreateType_WhenCatchForbiddenAccessException_Return403()
        {
            _effectService.CreateEffectTypeAsync(Arg.Any<NaheulbookExecutionContext>(), Arg.Any<CreateEffectTypeRequest>())
                .Returns(Task.FromException<EffectType>(new ForbiddenAccessException()));

            Func<Task<JsonResult>> act = () =>  _effectTypesController.PostCreateTypeAsync(new CreateEffectTypeRequest());

            act.Should().Throw<HttpErrorException>().Which.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}