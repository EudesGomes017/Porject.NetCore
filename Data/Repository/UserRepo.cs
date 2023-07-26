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
    public class UserRepo : GeralRepo, IUserRepo {
        private readonly DataContext _context;
        public UserRepo(DataContext context) : base(context) {
            _context = context;
        }

        public async Task<User> BuscarUserPorIdAsync(int? userId) {
            IQueryable<User> query = _context.User;

            query = query.AsNoTracking()
                         .Include(c => c.Transformers)
                         .Where(x => x.Id == userId)
                         .OrderBy(x => x.Id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<User> BuscarUserPorEmailAsync(string? email) {
            IQueryable<User> query = _context.User;

            query = query.AsNoTracking()
                         .Include(c => c.Transformers)
                         .Where(x => x.Email == email)
                         .OrderBy(x => x.Id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<User[]> BuscarUsersAsync() {
            IQueryable<User> query = _context.User;

            query = query.AsNoTracking()
                         .Include(c => c.Transformers).ThenInclude(c => c.Tests)
                         .OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

    }
}
