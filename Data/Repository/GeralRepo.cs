using Hvex.Data.Context;
using Hvex.Domain.Interface.Repository;
using System.Threading.Tasks;

namespace Hvex.Data.Repository {
    public class GeralRepo : IGeralRepo {

        private readonly DataContext _context;

        public GeralRepo(DataContext context) {
            _context = context;
        }

        public async void Adicionar<T>(T entity) where T : class {
            await _context.AddAsync(entity);
        }

        public void Atualizar<T>(T entity) where T : class {
            _context.Update(entity);
        }

        public void Deletar<T>(T entity) where T : class {
            _context.Remove(entity);
        }

        public void DeletarVarias<T>(T[] entityArray) where T : class {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SalvarMudancasAsync() {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
