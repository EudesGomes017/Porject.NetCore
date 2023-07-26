using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hvex.Domain.Entity;

namespace Hvex.Domain.Interface.Repository {
    public interface IReportRepo : IGeralRepo {
        Task<Report> BuscarReportPorIdAsync(int? reportId);
        Task<Report[]> BuscarReportsAsync();
    }
}
