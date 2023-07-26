using Microsoft.AspNetCore.Mvc;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Hvex.Exception.ExceptionBase;
using Hvex.Exception;
using System.Collections.Generic;
using Hvex.Domain.Dto;

namespace Hvex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
    
        [HttpGet(Name = "Buscar todos os relatorios")]
        public async Task<IActionResult> Get([FromServices] IReportService reportService) {
            var Tests = await reportService.BuscarReportsAsync();
            return this.StatusCode(StatusCodes.Status200OK, Tests);
        }

        [HttpGet("{id}", Name = "Buscar relatorio pelo ID")]
        public async Task<IActionResult> Get([FromServices] IReportService reportService, int id) {
            var Test = await reportService.BuscarReportPorIdAsync(id);
            return this.StatusCode(StatusCodes.Status200OK, Test);
        }

        [HttpPost(Name = "Adcionar relatorio")]
        public async Task<IActionResult> Post([FromServices] IReportService reportService, ReportDto model) {
            var Test = await reportService.AdicionarReportAsync(model);
            return this.StatusCode(StatusCodes.Status201Created, Test);
        }

        [HttpPut("{id}", Name = "Atulizar relatorio")]
        public async Task<IActionResult> Put([FromServices] IReportService reportService, int id, ReportDto model) {

            ReportDto report;

            if (await reportService.UpdateReport(id, model)) {
                report = await reportService.AtualizarReportAsync(model);

                return this.StatusCode(StatusCodes.Status201Created, report);
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.REPORT_FAIL_DELETE });
        }

        [HttpDelete("{id}", Name = "Deletar relatorio pelo ID")]
        public async Task<IActionResult> Delete([FromServices] IReportService reportService, int id) {
            var report = await reportService.BuscarReportPorIdAsync(id);
            await reportService.DeletarReportPorIdAsync(report);

            return this.StatusCode(StatusCodes.Status202Accepted, new { messages = ResourceMessageErro.REPORT_DELETE_SUCCESS });
        }
    }
}
