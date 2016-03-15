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
    public class Tests
    {
        class User
        {
            public int UserId { get; set; }
            public string Password { get; set; }
        }

        [Fact]
        public void generate_user_password()
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var result = GeneratePassword(browser);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.False(string.IsNullOrEmpty(result.Body.AsString()));
        }

        [Fact]
        public void can_login_with_generated_password()
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var password = GeneratePassword(browser).Body.AsString();
            var user = new User() { UserId = 12345, Password = password };

            var result = browser.Post("/login", with =>
                {
                    with.HttpRequest();
                    with.Header("Accept", "application/json");
                    with.JsonBody(user);
                }
            );

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("true", result.Body.AsString());
        }

        [Theory]
        [InlineData("abcdefgh")]
        [InlineData("123asdfa390")]
        public void cannot_login_with_wrong_password(string password)
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var result = browser.Post("/login", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.JsonBody(new User() { UserId = 12345, Password = password });
            }
            );

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("false", result.Body.AsString());
        }

        private static BrowserResponse GeneratePassword(Browser browser)
        {
            return browser.Post("/generatePassword", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.JsonBody(new User() { UserId = 12345 });
            });
        }
    }
}
