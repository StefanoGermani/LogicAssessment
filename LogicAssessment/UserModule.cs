using Nancy;
using Nancy.ModelBinding;

namespace LogicAssessment
{
    public class UserModule : Nancy.NancyModule
    {
        public UserModule(IUserRepository userRepository)
        {
            var passwordGenerator = new PasswordGenerator();

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

                if (user.UserId < 1)
                {
                    return new Response() { StatusCode = HttpStatusCode.BadRequest };
                }

                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    return new Response() { StatusCode = HttpStatusCode.BadRequest };
                }

                var existingUser = userRepository.Find(user.UserId, user.Password);
                userRepository.Delete(existingUser);

                return (existingUser != null).ToString().ToLower();
            };
        }
    }

}
