using Nancy;
using Nancy.Testing;
using System.Threading;
using Xunit;

namespace LogicAssessment.Tests
{
    public class LoginTests
    {
        [Fact]
        public void can_login_with_generated_password()
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var password = GeneratePassword(browser, 12345).Body.AsString();

            var result = browser.Post("/login", with =>
                {
                    with.HttpRequest();
                    with.JsonBody(new UserTestModel() { UserId = 12345, Password = password });
                }
            );

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("true", result.Body.AsString());
        }

        [Fact]
        public void cannot_login_with_generated_password_after_30_secs()
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var password = GeneratePassword(browser, 12345).Body.AsString();

            Thread.Sleep(30500);

            var result = browser.Post("/login", with =>
            {
                with.HttpRequest();
                with.JsonBody(new UserTestModel() { UserId = 12345, Password = password });
            }
            );

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("false", result.Body.AsString());
        }

        [Fact]
        public void login_returns_bad_request_if_user_id_is_missing()
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var result = browser.Post("/login", with =>
            {
                with.HttpRequest();
                with.JsonBody(new UserTestModel() { Password = "password" });
            });

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1000)]
        public void login_returns_bad_request_if_user_id_is_invalid(int userId)
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var result = browser.Post("/login", with =>
            {
                with.HttpRequest();
                with.JsonBody(new UserTestModel() { UserId = userId, Password = "password" });
            });

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void login_returns_bad_request_if_password_is_invalid(string password)
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var result = browser.Post("/login", with =>
            {
                with.HttpRequest();
                with.JsonBody(new UserTestModel() { UserId = 123456, Password = password });
            });

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void cannot_login_with_the_same_password_twice()
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var password = GeneratePassword(browser, 12345).Body.AsString();
            var user = new UserTestModel() { UserId = 12345, Password = password };

            var result = browser.Post("/login", with =>
            {
                with.HttpRequest();
                with.JsonBody(user);
            }
            );

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("true", result.Body.AsString());

            result = browser.Post("/login", with =>
            {
                with.HttpRequest();
                with.JsonBody(user);
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("false", result.Body.AsString());
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
                with.JsonBody(new UserTestModel() { UserId = 12345, Password = password });
            }
            );

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("false", result.Body.AsString());
        }

        private static BrowserResponse GeneratePassword(Browser browser, int userId)
        {
            return browser.Post("/generatePassword", with =>
            {
                with.HttpRequest();
                with.JsonBody(new UserTestModel() { UserId = userId });
            });
        }
    }
}
