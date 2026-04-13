using System.Collections.Generic;
using System.Linq;
using Copilot_Test.DTOs;

namespace Copilot_Test.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<UserDto> _store = new();

        public IEnumerable<UserDto> GetAll() => _store;

        public UserDto? GetById(int id) => _store.FirstOrDefault(u => u.Id == id);

        public UserDto Add(UserDto user)
        {
            _store.Add(user);
            return user;
        }

        public bool Update(UserDto user)
        {
            var existing = _store.FirstOrDefault(u => u.Id == user.Id);
            if (existing is null) return false;
            existing.Name = user.Name;
            existing.Email = user.Email;
            return true;
        }

        public bool Delete(int id)
        {
            var existing = _store.FirstOrDefault(u => u.Id == id);
            if (existing is null) return false;
            _store.Remove(existing);
            return true;
        }
    }
}
