using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicAssessment
{
    class UserRepository
    {
        private static UserRepository _userRepository = new UserRepository();
        private static List<User> _users = new List<User>();

        public static UserRepository Instance =>  _userRepository;
        

        private UserRepository() { }

        public void Save(User user)
        {
            user.CreatedDateTime = DateTime.Now;
            _users.Add(user);
        }

        public User Get(int userId, string password)
        {
            return _users.FirstOrDefault(u => u.UserId == userId && u.Password == password && u.CreatedDateTime.AddSeconds(30) > DateTime.Now);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }
    }
}
