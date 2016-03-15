using Nancy;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LogicAssessment.Tests
{
    public class GeneratePasswordTests
    {
        [Fact]
        public void generate_user_password()
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var result = browser.Post("/generatePassword", with =>
            {
                with.HttpRequest();
                with.JsonBody(new UserTestModel() { UserId = 12345 });
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.False(string.IsNullOrEmpty(result.Body.AsString()));
        }

        [Fact]
        public void generate_user_password_returns_bad_request_if_user_id_is_missing()
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var result = browser.Post("/generatePassword", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1000)]
        public void generate_user_password_returns_bad_request_if_user_id_is_invalid(int userId)
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var result = browser.Post("/generatePassword", with =>
            {
                with.HttpRequest();
                with.JsonBody(new UserTestModel() { UserId = userId });
            }); ;

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
