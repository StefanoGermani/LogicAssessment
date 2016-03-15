using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicAssessment
{
    public interface IUserRepository
    {
        void Save(User user);

        User Find(int userId, string password);

        void Delete(User user);
    }

    public class FakeUserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();

        public void Save(User user)
        {
            user.CreatedDateTime = DateTime.Now;
            _users.Add(user);
        }

        public User Find(int userId, string password)
        {
            return _users.FirstOrDefault(u => u.UserId == userId && u.Password == password && u.CreatedDateTime.AddSeconds(30) > DateTime.Now);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }
    }
}
