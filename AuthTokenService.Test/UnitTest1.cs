using AuthTokenService;
using AuthTokenService.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AuthTokenService.Test
{
    [TestClass]
    public class UnitTest1
    {
        public static IService AuthTokenService { get; set; }
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            AuthTokenService = new Service();
        }
        [TestMethod]
        public void CreatesToken()
        {
            TokenClaim[] claims = new TokenClaim[] { 
                new TokenClaim(){ Name="TokenClaim1", Value="Value1"},
                new TokenClaim(){ Name="TokenClaim2", Value="Value2"},
                new TokenClaim(){ Name="TokenClaim3", Value="Value3"},
                new TokenClaim(){ Name="TokenClaim4", Value="Value4"}
            };
            var token = AuthTokenService.ToToken(claims);
            Assert.IsTrue(token.IsValid, "Token Is Invalid");
            Assert.IsNotNull(token.Value, "Missing Token Value");
            Assert.IsNotNull(token.ExpirationDate, "Missing Token Expiration Date");
            Assert.IsNotNull(token.Claims, "Null Token Claims");
            Assert.AreEqual(4,token.Claims.Count(), "Missing Token Claims");
            Assert.IsTrue(token.Claims.Any(c=> c.Name == "TokenClaim1"), "Missing TokenClaim1 Token Claim");
            Assert.IsTrue(token.Claims.Any(c => c.Name == "TokenClaim2"), "Missing TokenClaim2 Token Claim");
            Assert.IsTrue(token.Claims.Any(c => c.Name == "TokenClaim3"), "Missing TokenClaim3 Token Claim");
            Assert.IsTrue(token.Claims.Any(c => c.Name == "TokenClaim4"), "Missing TokenClaim4 Token Claim");
            Assert.AreEqual("Value1", token.Claims.FirstOrDefault(c => c.Name == "TokenClaim1").Value, "Invalid TokenClaim1 Token Claim");
            Assert.AreEqual("Value2", token.Claims.FirstOrDefault(c => c.Name == "TokenClaim2").Value, "Invalid TokenClaim2 Token Claim");
            Assert.AreEqual("Value3", token.Claims.FirstOrDefault(c => c.Name == "TokenClaim3").Value, "Invalid TokenClaim3 Token Claim");
            Assert.AreEqual("Value4", token.Claims.FirstOrDefault(c => c.Name == "TokenClaim4").Value, "Invalid TokenClaim4 Token Claim");
        }
        [TestMethod]
        public void ParsesToken()
        {
            TokenClaim[] claims = new TokenClaim[] {
                new TokenClaim(){ Name="TokenClaim1", Value="Value1"},
                new TokenClaim(){ Name="TokenClaim2", Value="Value2"},
                new TokenClaim(){ Name="TokenClaim3", Value="Value3"},
                new TokenClaim(){ Name="TokenClaim4", Value="Value4"}
            };
            var createdToken = AuthTokenService.ToToken(claims);

            var token = AuthTokenService.ParseToken(createdToken.Value);
            Assert.IsTrue(token.IsValid, "Token Is Invalid");
            Assert.IsNotNull(token.Value, "Missing Token Value");
            Assert.IsNotNull(token.ExpirationDate, "Missing Token Expiration Date");
            Assert.IsNotNull(token.Claims, "Null Token Claims");
            Assert.AreEqual(4, token.Claims.Count(), "Missing Token Claims");
            Assert.IsTrue(token.Claims.Any(c => c.Name == "TokenClaim1"), "Missing TokenClaim1 Token Claim");
            Assert.IsTrue(token.Claims.Any(c => c.Name == "TokenClaim2"), "Missing TokenClaim2 Token Claim");
            Assert.IsTrue(token.Claims.Any(c => c.Name == "TokenClaim3"), "Missing TokenClaim3 Token Claim");
            Assert.IsTrue(token.Claims.Any(c => c.Name == "TokenClaim4"), "Missing TokenClaim4 Token Claim");
            Assert.AreEqual("Value1", token.Claims.FirstOrDefault(c => c.Name == "TokenClaim1").Value, "Invalid TokenClaim1 Token Claim");
            Assert.AreEqual("Value2", token.Claims.FirstOrDefault(c => c.Name == "TokenClaim2").Value, "Invalid TokenClaim2 Token Claim");
            Assert.AreEqual("Value3", token.Claims.FirstOrDefault(c => c.Name == "TokenClaim3").Value, "Invalid TokenClaim3 Token Claim");
            Assert.AreEqual("Value4", token.Claims.FirstOrDefault(c => c.Name == "TokenClaim4").Value, "Invalid TokenClaim4 Token Claim");
        }
        [TestMethod]
        public void ParsesInvalidToken()
        {
            var token = AuthTokenService.ParseToken("Invalid token String");
            Assert.IsFalse(token.IsValid, "Expected Invalid Token");
            Assert.AreEqual(token.Value, "Invalid token String");
            Assert.IsNull(token.ExpirationDate, "Expected Missing Token Expiration Date");
            Assert.IsNotNull(token.Claims, "Null Token Claims");
            Assert.AreEqual(0, token.Claims.Count(), "Expected Missing Token Claims");
        }
    }
}
