using System.Collections.Generic;
using Copilot_Test.DTOs;

namespace Copilot_Test.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAll();
        UserDto? GetById(int id);
        UserDto Add(UserDto user);
        bool Update(UserDto user);
        bool Delete(int id);
    }
}
