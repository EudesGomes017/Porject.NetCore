using Hvex.Domain.Dto;
using Hvex.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hvex.Domain.Interface.Services
{
    public interface IReportService
    {
        Task<ReportDto> AdicionarReportAsync(ReportDto report);
        Task<ReportDto> AtualizarReportAsync(ReportDto report);
        Task<ReportDto> BuscarReportPorIdAsync(int? reportId);
        Task<ReportDto[]> BuscarReportsAsync();
        Task<bool> DeletarReportPorIdAsync(ReportDto report);
        void Validator(ReportDto report);
        Task<bool> UpdateReport(int? id, ReportDto report);
    }
}
