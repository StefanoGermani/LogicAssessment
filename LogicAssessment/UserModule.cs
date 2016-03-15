using Nancy;
using Nancy.ModelBinding;

namespace LogicAssessment
{
    public class UserModule : Nancy.NancyModule
    {
        public UserModule()
        {
            var passwordGenerator = new PasswordGenerator();
            var userRepository = UserRepository.Instance;

            Post["/generatePassword"] = _ =>
            {
                var user = this.Bind<User>();

                if(user.UserId < 1)
                {
                    return new Response() { StatusCode = HttpStatusCode.BadRequest };
                }

                var password = passwordGenerator.Generate();

                user.Password = password;
                userRepository.Save(user);

                return password;
            };

            Post["/login"] = _ =>
            {
                var user = this.Bind<User>();

                var existingUser = userRepository.Get(user.UserId, user.Password);

                return (existingUser != null).ToString().ToLower();
            };
        }
    }

}
