using Hvex.Domain.Entity;
using System.Threading.Tasks;

namespace Hvex.Domain.Interface.Repository {
    public interface ITestRepo : IGeralRepo {
        Task<Test> BuscarTestPorIdAsync(int? testId);
        Task<Test[]> BuscarTestsAsync();
    }
}
