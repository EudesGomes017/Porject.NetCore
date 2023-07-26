using Hvex.Data.Context;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hvex.Data.Repository {
    public class TransformerRepo : GeralRepo, ITransformerRepo {
        private readonly DataContext _context;
        public TransformerRepo(DataContext context) : base(context) {
            _context = context;
        }

        public async Task<Transformer> BuscarTransformerPorIdAsync(int? transformerId) {
            IQueryable<Transformer> query = _context.Transformer;

            query = query.AsNoTracking()
                         .Include(c => c.User)
                         .Include(c => c.Tests)
                         .Where(x => x.Id == transformerId)
                         .OrderBy(x => x.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Transformer[]> BuscarTransformersAsync() {
            IQueryable<Transformer> query = _context.Transformer;

            query = query
                         .Include(c => c.User)
                         .Include(c => c.Tests)
                         .OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }
    }
}
