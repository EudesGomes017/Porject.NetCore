using Hvex.Domain.Entity;
using System.Threading.Tasks;

namespace Hvex.Domain.Interface.Repository {
    public interface IUserRepo : IGeralRepo {
        Task<User> BuscarUserPorIdAsync(int? id);
        Task<User> BuscarUserPorEmailAsync(string? email);
        Task<User[]> BuscarUsersAsync();
    }
}
