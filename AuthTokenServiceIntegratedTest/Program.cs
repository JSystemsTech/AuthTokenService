using AuthTokenServiceIntegratedTest.AuthTokenService;
using System;
using System.Linq;

namespace AuthTokenServiceIntegratedTest
{

    internal class Program
    {
        private static ServiceClient Client { get; set; }
        static void Main(string[] args)
        {
            try
            {
                Client = new ServiceClient();
                TryRunTest(CreatesToken, "Verify Token Creation Result");
                TryRunTest(ParsesToken, "Verify Token Parses Correctly");
                TryRunTest(ParsesInvalidToken, "Verify Invalid Token Result");
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private static void Evaluate(bool passes, string message)
        {
            if (!passes)
            {
                throw new Exception(message); // throw error by design to stop process 
            }
        }
        private static void TryRunTest(Action test, string name)
        {
            try
            {
                Console.WriteLine($"Running Test: {name}");
                test();
                Console.WriteLine("Result: Pass");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Result: Fail");
            }
        }
        private static void CreatesToken()
        {
            TokenClaim[] claims = new TokenClaim[] {
                new TokenClaim(){ Name="TokenClaim1", Value="Value1"},
                new TokenClaim(){ Name="TokenClaim2", Value="Value2"},
                new TokenClaim(){ Name="TokenClaim3", Value="Value3"},
                new TokenClaim(){ Name="TokenClaim4", Value="Value4"}
            };
            var token = Client.ToToken(claims);
            Evaluate(token.IsValid, "Token Is Invalid");
            Evaluate(!string.IsNullOrWhiteSpace(token.Value), "Missing Token Value");
            Evaluate(token.ExpirationDate != null, "Missing Token Expiration Date");
            Evaluate(token.Claims != null, "Null Token Claims");
            Evaluate(token.Claims.Count() == 4, "Missing Token Claims");
            Evaluate(token.Claims.Any(c => c.Name == "TokenClaim1"), "Missing TokenClaim1 Token Claim");
            Evaluate(token.Claims.Any(c => c.Name == "TokenClaim2"), "Missing TokenClaim2 Token Claim");
            Evaluate(token.Claims.Any(c => c.Name == "TokenClaim3"), "Missing TokenClaim3 Token Claim");
            Evaluate(token.Claims.Any(c => c.Name == "TokenClaim4"), "Missing TokenClaim4 Token Claim");
            Evaluate("Value1" == token.Claims.FirstOrDefault(c => c.Name == "TokenClaim1").Value, "Invalid TokenClaim1 Token Claim");
            Evaluate("Value2" == token.Claims.FirstOrDefault(c => c.Name == "TokenClaim2").Value, "Invalid TokenClaim2 Token Claim");
            Evaluate("Value3" == token.Claims.FirstOrDefault(c => c.Name == "TokenClaim3").Value, "Invalid TokenClaim3 Token Claim");
            Evaluate("Value4" == token.Claims.FirstOrDefault(c => c.Name == "TokenClaim4").Value, "Invalid TokenClaim4 Token Claim");            
        }
        private static void ParsesToken()
        {
            TokenClaim[] claims = new TokenClaim[] {
                new TokenClaim(){ Name="TokenClaim1", Value="Value1"},
                new TokenClaim(){ Name="TokenClaim2", Value="Value2"},
                new TokenClaim(){ Name="TokenClaim3", Value="Value3"},
                new TokenClaim(){ Name="TokenClaim4", Value="Value4"}
            };
            var createdToken = Client.ToToken(claims);

            var token = Client.ParseToken(createdToken.Value);
            Evaluate(token.IsValid, "Token Is Invalid");
            Evaluate(!string.IsNullOrWhiteSpace(token.Value), "Missing Token Value");
            Evaluate(token.ExpirationDate != null, "Missing Token Expiration Date");
            Evaluate(token.Claims != null, "Null Token Claims");
            Evaluate(token.Claims.Count() == 4, "Missing Token Claims");
            Evaluate(token.Claims.Any(c => c.Name == "TokenClaim1"), "Missing TokenClaim1 Token Claim");
            Evaluate(token.Claims.Any(c => c.Name == "TokenClaim2"), "Missing TokenClaim2 Token Claim");
            Evaluate(token.Claims.Any(c => c.Name == "TokenClaim3"), "Missing TokenClaim3 Token Claim");
            Evaluate(token.Claims.Any(c => c.Name == "TokenClaim4"), "Missing TokenClaim4 Token Claim");
            Evaluate("Value1" == token.Claims.FirstOrDefault(c => c.Name == "TokenClaim1").Value, "Invalid TokenClaim1 Token Claim");
            Evaluate("Value2" == token.Claims.FirstOrDefault(c => c.Name == "TokenClaim2").Value, "Invalid TokenClaim2 Token Claim");
            Evaluate("Value3" == token.Claims.FirstOrDefault(c => c.Name == "TokenClaim3").Value, "Invalid TokenClaim3 Token Claim");
            Evaluate("Value4" == token.Claims.FirstOrDefault(c => c.Name == "TokenClaim4").Value, "Invalid TokenClaim4 Token Claim");
        }

        private static void ParsesInvalidToken()
        {
            var token = Client.ParseToken("Invalid token String");
            Evaluate(!token.IsValid, "Expected Invalid Token");
            Evaluate(token.Value == "Invalid token String", "Expected value to be 'Invalid token String'");
            Evaluate(token.ExpirationDate == null, "Expected Missing Token Expiration Date");
            Evaluate(token.Claims != null, "Null Token Claims");
            Evaluate(0 == token.Claims.Count(), "Expected Missing Token Claims");
        }
    }
}
