
namespace WebServiceClient.Authenticator;

public class OAuthAuthenticator(string appId, string login, string password, Uri uriOAuthHost = null) : IAuthenticator
{
    private readonly HttpClientHandler httpOAuthClientHandler = new()
    {
        CookieContainer = new System.Net.CookieContainer(),
        UseCookies = true,
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
    };

    // https://login.microsoftonline.com/{tenant}/oauth2/v2.0/authorize?

    // https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id=de8bc8b5-d9f9-48b1-a8ad-b748da725064&scope=openid+profile+User.Read+offline_access&redirect_uri=https%3a%2f%2fdeveloper.microsoft.com%2fen-us%2fgraph%2fgraph-explorer&client-request-id=0193644e-5090-7f5c-911c-b8df55e65e11&response_mode=fragment&response_type=code&x-client-SKU=msal.js.browser&x-client-VER=3.14.0&client_info=1&code_challenge=hw_2e9LVNApuGQrssKIWotS9jAN2Y-Skf-tmNGniCKY&code_challenge_method=S256&prompt=select_account&nonce=0193644e-509c-7500-a08f-3c18545ebe96&state=eyJpZCI6IjAxOTM2NDRlLTUwOWItNzkwMS05ZjE1LTlhMTA4YTYzMmFlNiIsIm1ldGEiOnsiaW50ZXJhY3Rpb25UeXBlIjoicG9wdXAifX0%3d&claims=%7b%22access_token%22%3a%7b%22xms_cc%22%3a%7b%22values%22%3a%5b%22CP1%22%5d%7d%7d%7d&mkt=en-US&safe_rollout=apply%3a0238caeb-f6ca-4efc-afd0-a72e1273a8bc&sso_nonce=AwABEgEAAAADAOz_BQD0_xcLBOGHl5nDiZgBjkajs7mmioNWt-PwEsHZUSW84jjKH9YWDSD2fNJ2aSu8izj0_CVAWoQaKTQfrXYxYEF7ZKogAA&mscrid=0193644e-5090-7f5c-911c-b8df55e65e11
    // https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id=de8bc8b5-d9f9-48b1-a8ad-b748da725064&scope=openid+profile+User.Read+offline_access&redirect_uri=https%3a%2f%2fdeveloper.microsoft.com%2fen-us%2fgraph%2fgraph-explorer&client-request-id=0193644e-5090-7f5c-911c-b8df55e65e11&response_mode=fragment&response_type=code&x-client-SKU=msal.js.browser&x-client-VER=3.14.0&client_info=1&code_challenge=hw_2e9LVNApuGQrssKIWotS9jAN2Y-Skf-tmNGniCKY&code_challenge_method=S256&prompt=select_account&nonce=0193644e-509c-7500-a08f-3c18545ebe96&state=eyJpZCI6IjAxOTM2NDRlLTUwOWItNzkwMS05ZjE1LTlhMTA4YTYzMmFlNiIsIm1ldGEiOnsiaW50ZXJhY3Rpb25UeXBlIjoicG9wdXAifX0%3d&claims=%7b%22access_token%22%3a%7b%22xms_cc%22%3a%7b%22values%22%3a%5b%22CP1%22%5d%7d%7d%7d&mkt=en-US&safe_rollout=apply%3a0238caeb-f6ca-4efc-afd0-a72e1273a8bc&sso_nonce=AwABEgEAAAADAOz_BQD0_xcLBOGHl5nDiZgBjkajs7mmioNWt-PwEsHZUSW84jjKH9YWDSD2fNJ2aSu8izj0_CVAWoQaKTQfrXYxYEF7ZKogAA&mscrid=0193644e-5090-7f5c-911c-b8df55e65e11
    // https://login.microsoftonline.com/common/login
    // https://developer.microsoft.com/en-us/graph/graph-explorer#code=1.ARIAa8Nk5y4BFkKRDY_RYoMYLbXIi9752bFIqK23SNpyUGQSAKMSAA.AgABBAIAAADW6jl31mB3T7ugrWTT8pFeAwDs_wUA9P9rsjeq8KXGaehYnEpHkMJ1wp9aklPJQaVK_RwFQQzy-jmE6XH76aX-8VuatnmPTWwj8Pvay1eGTuorjcvowAcfPQv4EG3wEdVQDNSQZAPi24VYykaCqoJGnqPflkFNvoebbDkoUBehHdHf0u_0hDImuZ25WR6N2vh_AvN1Rs29gk_a3M1fNsF4ZhafDylpqAZcSwMk478lL5N0bFXqdph7y9ADB4b209oXA-fp1MszdyQMYbcZfrOxPR0zRSvlk9w0MP99SFZt-1SSyKM8lvHSSoN3exXDjdbUhZtwPMZpl4uxHkjutnwpmYH0LREj9HeS3BXrNqEcgyzmbDGIvSVsKuFMDeUiijq-FZ8_wOpa6L-oGSHLn4NHoJwa4osmEtafQsTYzXIHQfCnvYqHvNMs7FSbDaI6SA1IDPwDYNI_h7i4jX522LacTWqof1CktXIkZ-jKH7cUjVsebVTz3yFjn88ojpA8Nj4ctF66C6kvesOImonlYCCf_vmK2zD4p9gCftEZ1zxyZIpAHTlEbSYTWg6siHfw1mGCpC2f2Z8_hGj3FNOx-JLDg85MeERoAkxR31NavZ2zE5ZkwfjMm4zAOc1q-O2VqVj1h_2RMkHKLHVEZoXdxmg9-Jy9oLm0LamNc2Yq8pMy7xcapHeTLtdcksRxHwnjNu2WAh14A-Y6l_0VxHkFpYEHBLhfYzSdjv9TD9itLlb7NUVC0-qUS2Iki06KibGMqdjAzteXrs0uyN67xa2q7UoP97kjjAVcybmRSWxp1cZoXiu8we69jHuDjiiGYfAwMN6IE7nPxjtFzGz3h2vQuwJaqI3_B0zi9wzo27RnZpcFEK9rCwnTmjht31h09U_uU4rm1b3XCgCO5OOgU_W2lHHk_2lhhfYwTBC8JLpFywBDh6hstSrwt0Wz-OZVRLjDoLdAQHmjyS3wDMjfct_32OXuMm9SI9Ggc_4WK3ozbplOCIPMK5yW1w_smupSxCJd0FCVrdcrOXKt0CgNLcA05ftJ7_-d7GBAYLdO-Go76f9e&client_info=eyJ1aWQiOiIyZjFkYTVlYi0yMjhiLTQ2NGYtODc1Ni05NDk2NDYxZDlhMDUiLCJ1dGlkIjoiZTc2NGMzNmItMDEyZS00MjE2LTkxMGQtOGZkMTYyODMxODJkIn0&state=eyJpZCI6IjAxOTM2NDRlLTUwOWItNzkwMS05ZjE1LTlhMTA4YTYzMmFlNiIsIm1ldGEiOnsiaW50ZXJhY3Rpb25UeXBlIjoicG9wdXAifX0%3d&session_state=e86d9073-bcde-4056-9651-ba316b2e2e63
    // https://login.microsoftonline.com/common/oauth2/v2.0/token?mkt=en-US&safe_rollout=apply%3a0238caeb-f6ca-4efc-afd0-a72e1273a8bc

    // https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id=de8bc8b5-d9f9-48b1-a8ad-b748da725064&scope=openid+profile+User.Read+offline_access&sso_nonce=AwABEgEAAAADAOz_BQD0_958AccYvJv1bRfQKjoWQwrBQM0IOrwILunkle4mo02tUNSq_yGIunaXHT9XU5cbtIVKwaKIMQsn6Mwj53aitDAgAA&client-request-id=2bc15488-2f08-4e65-8c76-3ed9621c1948&mscrid=2bc15488-2f08-4e65-8c76-3ed9621c1948
    // POST https://login.microsoftonline.com/common/login


    // https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id=de8bc8b5-d9f9-48b1-a8ad-b748da725064&scope=openid+profile+User.Read+offline_access&sso_nonce=AwABEgEAAAADAOz_BQD0_958AccYvJv1bRfQKjoWQwrBQM0IOrwILunkle4mo02tUNSq_yGIunaXHT9XU5cbtIVKwaKIMQsn6Mwj53aitDAgAA&client-request-id=2bc15488-2f08-4e65-8c76-3ed9621c1948&mscrid=2bc15488-2f08-4e65-8c76-3ed9621c1948

    private string[] Scopes { get; set; } = new string[] { "User.Read", "User.ReadBasic.All" };

    public void Authenticate(WebService service, HttpClient client)
    {
        try
        {
            var options = new PublicClientApplicationOptions()
            {
                AadAuthorityAudience = AadAuthorityAudience.AzureAdAndPersonalMicrosoftAccount,
                AzureCloudInstance = AzureCloudInstance.AzureGermany,
                ClientId = "de8bc8b5-d9f9-48b1-a8ad-b748da725064",
                TenantId = appId,
            };

            IPublicClientApplication app = PublicClientApplicationBuilder.CreateWithApplicationOptions(options).Build();


            //var accounts = app.GetAccountsAsync().Result; 

            //AcquireTokenSilentParameterBuilder? result = null;
            //if (accounts.Any())
            //{
            //    try
            //    {
            //        IAccount account = accounts.First();
            //        // Attempt to get a token from the cache (or refresh it silently if needed)
            //        result = (app /*as PublicClientApplication*/)?.AcquireTokenSilent(Scopes, account);
            //    }
            //    catch (MsalUiRequiredException)
            //    {
            //        // No token for the account. Will proceed below
            //    }
            //}

            //AcquireTokenByUsernamePasswordParameterBuilder? acquireTokenBy = null;

            //AuthenticationResult? acquireTokenBy = null;

            AuthenticationResult? acquireTokenBy = app.AcquireTokenByUsernamePassword(Scopes, login, password).ExecuteAsync(CancellationToken.None).Result;


            

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", acquireTokenBy.AccessToken);

        }
        catch(Exception ex)
        {

        }
    }
}
