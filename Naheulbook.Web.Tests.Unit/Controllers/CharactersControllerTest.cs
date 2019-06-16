using System;
using System.Collections.Generic;
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
    public class CharactersControllerTest
    {
        private ICharacterService _characterService;
        private IMapper _mapper;
        private NaheulbookExecutionContext _executionContext;

        private CharactersController _controller;

        [SetUp]
        public void SetUp()
        {
            _characterService = Substitute.For<ICharacterService>();
            _mapper = Substitute.For<IMapper>();

            _controller = new CharactersController(
                _characterService,
                _mapper
            );

            _executionContext = new NaheulbookExecutionContext();
        }

        [Test]
        public async Task GetCharactersListAsync_ShouldLoadListOfCharacterFromService_ThenMapItIntoSummary()
        {
            var characters = new List<Character>();
            var charactersResponse = new List<CharacterSummaryResponse>();

            _characterService.GetCharacterListAsync(_executionContext)
                .Returns(characters);
            _mapper.Map<List<CharacterSummaryResponse>>(characters)
                .Returns(charactersResponse);

            var result = await _controller.GetCharactersListAsync(_executionContext);

            result.Value.Should().BeSameAs(charactersResponse);
        }

        [Test]
        public async Task GetCharactersListAsync_ShouldLoadCharacterFromService_ThenMapItIntoResponse()
        {
            const int characterId = 2;
            var character = new Character();
            var characterResponse = new CharacterResponse();;

            _characterService.LoadCharacterDetailsAsync(_executionContext, characterId)
                .Returns(character);
            _mapper.Map<CharacterResponse>(character)
                .Returns(characterResponse);

            var result = await _controller.GetCharacterDetailsAsync(_executionContext, characterId);

            result.Value.Should().BeSameAs(characterResponse);
        }

        [Test]
        public void GetCharactersListAsync_WhenCatchForbiddenAccessException_Return403()
        {
            const int characterId = 2;
            _characterService.LoadCharacterDetailsAsync(Arg.Any<NaheulbookExecutionContext>(), Arg.Any<int>())
                .Returns(Task.FromException<Character>(new ForbiddenAccessException()));

            Func<Task> act = () => _controller.GetCharacterDetailsAsync(_executionContext, characterId);

            act.Should().Throw<HttpErrorException>().Which.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public void GetCharactersListAsync_WhenCatchCharacterNotFoundException_Return404()
        {
            const int characterId = 2;
            _characterService.LoadCharacterDetailsAsync(Arg.Any<NaheulbookExecutionContext>(), Arg.Any<int>())
                .Returns(Task.FromException<Character>(new CharacterNotFoundException(characterId)));

            Func<Task> act = () => _controller.GetCharacterDetailsAsync(_executionContext, characterId);

            act.Should().Throw<HttpErrorException>().Which.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task PostCreateCharacterAsync_ShouldCreateCharacterWithCharacterService_ThenReturnCharacterResponse()
        {
            var createCharacterRequest = new CreateCharacterRequest();
            var createdCharacter = new Character();
            var characterResponse = new CreateCharacterResponse();

            _characterService.CreateCharacterAsync(_executionContext, createCharacterRequest)
                .Returns(createdCharacter);
            _mapper.Map<CreateCharacterResponse>(createdCharacter)
                .Returns(characterResponse);

            var result = await _controller.PostCreateCharacterAsync(_executionContext, createCharacterRequest);

            result.Value.Should().BeSameAs(characterResponse);
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}