using Hvex.Data.Context;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using System.Threading.Tasks;

namespace Hvex.Data.Repository {
    public class ReportRepo : GeralRepo, IReportRepo {
        private readonly DataContext _context;
        public ReportRepo(DataContext context) : base(context) {
            _context = context;
        }

        public async Task<Report> BuscarReportPorIdAsync(int? reportId) {
            IQueryable<Report> query = _context.Report;

            query = query.AsNoTracking()
                         .Include(tr => tr.Transformer).ThenInclude(t => t.Tests)
                         .Where(x => x.Id == reportId)
                         .OrderBy(x => x.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Report[]> BuscarReportsAsync() {
            IQueryable<Report> query = _context.Report;

            query = query.AsNoTracking()
                         .Include(tr => tr.Transformer).ThenInclude(t => t.Tests)
                         .OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }
    }
}
