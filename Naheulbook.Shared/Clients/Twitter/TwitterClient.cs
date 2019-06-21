using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Naheulbook.Shared.Clients.Oauth1;
using Naheulbook.Shared.Clients.Twitter.Exceptions;
using Naheulbook.Shared.Clients.Twitter.Responses;
using Naheulbook.Shared.Utils;

namespace Naheulbook.Shared.Clients.Twitter
{
    public interface ITwitterClient
    {
        Task<TwitterRequestTokenResponse> GetRequestTokenAsync();
        Task<TwitterAccessTokenResponse> GetAccessTokenAsync(string loginToken, string oauthToken, string oauthVerifier);
    }

    public class TwitterClient : ITwitterClient
    {
        private const string RequestTokenUri = "https://api.twitter.com/oauth/request_token";
        private const string AccessTokenUri = "https://api.twitter.com/oauth/access_token";

        private readonly TwitterConfiguration _configuration;

        public TwitterClient(TwitterConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TwitterRequestTokenResponse> GetRequestTokenAsync()
        {
            var oauth = new Oauth(_configuration.AppId, _configuration.AppSecret)
            {
                RequestUrl = RequestTokenUri
            };
            oauth.AddOauthParameter("callback", _configuration.Callback);

            try
            {
                var oauthResult = await oauth.DoRequest();

                return new TwitterRequestTokenResponse
                {
                    OAuthToken = oauthResult["oauth_token"]
                };
            }
            catch (Exception ex)
            {
                throw new TwitterClientException(ex);
            }
        }

        public async Task<TwitterAccessTokenResponse> GetAccessTokenAsync(string loginToken, string oauthToken, string oauthVerifier)
        {
            var oauth = new Oauth(_configuration.AppId, _configuration.AppSecret)
            {
                RequestUrl = AccessTokenUri,
                AccessSecret = loginToken
            };
            oauth.AddOauthParameter("token", oauthToken);
            oauth.AddParameter("oauth_verifier", oauthVerifier);

            try
            {
                var oauthResult = await oauth.DoRequest();

                return new TwitterAccessTokenResponse
                {
                    ScreenName = oauthResult["screen_name"],
                    UserId = oauthResult["user_id"]
                };
            }
            catch (Exception ex)
            {
                throw new TwitterClientException(ex);
            }
        }
    }
}