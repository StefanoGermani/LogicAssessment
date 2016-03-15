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
        }

        [Fact]
        public void generate_user_password()
        {
            var browser = new Browser(with => with.Module<UserModule>());

            var result = GeneratePassword(browser);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.False(string.IsNullOrEmpty(result.Body.AsString()));
        }



        private static BrowserResponse GeneratePassword(Browser browser)
        {
            return browser.Post("/generatePassword", with =>
            {
                with.HttpRequest();
                with.JsonBody(new User() { UserId = 12345 });
            });
        }
    }
}
