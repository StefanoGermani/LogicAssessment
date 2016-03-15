using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAssessment
{
    class UserRepository
    {
        private static UserRepository _userRepository = new UserRepository();
        private static ConcurrentBag<User> _users = new ConcurrentBag<User>();

        public static UserRepository Instance =>  _userRepository;
        

        private UserRepository() { }

        public void Save(User user)
        {
            _users.Add(user);
        }

        public User Get(int userId, string password)
        {
            return _users.FirstOrDefault(u => u.UserId == userId && u.Password == password);
        }
    }
}
