using CleanArchitecture.Domain.Models;

using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> AddAsync(User user);
    }
}
