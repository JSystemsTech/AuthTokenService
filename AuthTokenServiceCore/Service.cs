using AuthTokenServiceCore.Helpers;

namespace AuthTokenServiceCore
{
    public class Service
    {
        public AuthTokenResponse ToToken(IEnumerable<TokenClaim> claims) => claims.ToToken();

        public AuthTokenResponse ParseToken(string tokenStr) => tokenStr.ParseToken();
    }
}
