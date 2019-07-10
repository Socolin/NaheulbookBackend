using Naheulbook.Core.Models;
using Naheulbook.Core.Notifications;
using Naheulbook.Data.Models;
using Naheulbook.Requests.Requests;
using Naheulbook.Shared.Utils;

namespace Naheulbook.Core.Utils
{
    public interface ICharacterUtil
    {
        void ApplyCharactersChange(NaheulbookExecutionContext executionContext, PatchCharacterRequest request, Character character, INotificationSession notificationSession);
    }

    public class CharacterUtil : ICharacterUtil
    {
        private readonly IAuthorizationUtil _authorizationUtil;
        private readonly IJsonUtil _jsonUtil;
        private readonly ICharacterHistoryUtil _characterHistoryUtil;

        public CharacterUtil(
            IAuthorizationUtil authorizationUtil,
            ICharacterHistoryUtil characterHistoryUtil,
            IJsonUtil jsonUtil
        )
        {
            _authorizationUtil = authorizationUtil;
            _characterHistoryUtil = characterHistoryUtil;
            _jsonUtil = jsonUtil;
        }

        public void ApplyCharactersChange(NaheulbookExecutionContext executionContext, PatchCharacterRequest request, Character character, INotificationSession notificationSession)
        {
            if (request.Debilibeuk.HasValue)
            {
                _authorizationUtil.EnsureIsGroupOwner(executionContext, character.Group);
                var gmData = _jsonUtil.Deserialize<CharacterGmData>(character.GmData) ?? new CharacterGmData();
                gmData.Debilibeuk = request.Debilibeuk.Value;
                character.GmData = _jsonUtil.Serialize(gmData);
                notificationSession.NotifyCharacterGmChangeData(character, gmData);
            }

            if (request.Mankdebol.HasValue)
            {
                _authorizationUtil.EnsureIsGroupOwner(executionContext, character.Group);
                var gmData = _jsonUtil.Deserialize<CharacterGmData>(character.GmData) ?? new CharacterGmData();
                gmData.Mankdebol = request.Mankdebol.Value;
                character.GmData = _jsonUtil.Serialize(gmData);
                notificationSession.NotifyCharacterGmChangeData(character, gmData);
            }

            if (request.IsActive.HasValue)
            {
                _authorizationUtil.EnsureIsGroupOwner(executionContext, character.Group);
                character.IsActive = request.IsActive.Value;
                notificationSession.NotifyCharacterGmChangeActive(character);
            }

            if (request.Color != null)
            {
                _authorizationUtil.EnsureIsGroupOwner(executionContext, character.Group);
                character.Color = request.Color;
                notificationSession.NotifyCharacterGmChangeColor(character);
            }

            if (request.OwnerId != null)
            {
                _authorizationUtil.EnsureIsGroupOwner(executionContext, character.Group);
                character.OwnerId = request.OwnerId.Value;
            }

            if (request.Target != null)
            {
                _authorizationUtil.EnsureIsGroupOwner(executionContext, character.Group);
                if (request.Target.IsMonster)
                {
                    character.TargetedCharacterId = null;
                    character.TargetedMonsterId = request.Target.Id;
                }
                else
                {
                    character.TargetedMonsterId = null;
                    character.TargetedCharacterId = request.Target.Id;
                }
                notificationSession.NotifyCharacterGmChangeTarget(character, request.Target);
            }

            if (request.Ev.HasValue)
            {
                character.AddHistoryEntry(_characterHistoryUtil.CreateLogChangeEv(character, character.Ev, request.Ev));
                character.Ev = request.Ev;
                notificationSession.NotifyCharacterChangeEv(character);
            }

            if (request.Ea.HasValue)
            {
                character.AddHistoryEntry(_characterHistoryUtil.CreateLogChangeEa(character, character.Ea, request.Ea));
                character.Ea = request.Ea;
                notificationSession.NotifyCharacterChangeEa(character);
            }

            if (request.FatePoint.HasValue)
            {
                character.AddHistoryEntry(_characterHistoryUtil.CreateLogChangeFatePoint(character, character.FatePoint, request.FatePoint));
                character.FatePoint = request.FatePoint.Value;
                notificationSession.NotifyCharacterChangeFatePoint(character);
            }

            if (request.Experience.HasValue)
            {
                character.AddHistoryEntry(_characterHistoryUtil.CreateLogChangeExperience(character, character.Experience, request.Experience));
                character.Experience = request.Experience.Value;
                notificationSession.NotifyCharacterChangeExperience(character);
            }

            if (request.Sex != null)
            {
                character.AddHistoryEntry(_characterHistoryUtil.CreateLogChangeSex(character, character.Sex, request.Sex));
                character.Sex = request.Sex;
                notificationSession.NotifyCharacterChangeSex(character);
            }

            if (request.Name != null)
            {
                character.AddHistoryEntry(_characterHistoryUtil.CreateLogChangeName(character, character.Name, request.Name));
                character.Name = request.Name;
                notificationSession.NotifyCharacterChangeName(character);
            }
        }
    }
}