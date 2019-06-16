using System.Collections.Generic;
using Naheulbook.Data.Models;

// ReSharper disable MemberCanBeMadeStatic.Global

namespace Naheulbook.TestUtils
{
    public partial class DefaultEntityCreator
    {
        public User CreateUser(string suffix = null)
        {
            if (suffix == null)
                suffix = RngUtil.GetRandomHexString(8);

            return new User
            {
                Username = $"some-username-{suffix}",
                DisplayName = $"some-display-name-{suffix}",
                HashedPassword = $"some-hashed-password-{suffix}",
                ActivationCode = "some-activation-code"
            };
        }
    }
}