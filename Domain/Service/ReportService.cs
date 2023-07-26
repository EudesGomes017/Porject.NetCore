using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Services;
using Hvex.Domain.Interface.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hvex.Exception.ExceptionBase;
using Hvex.Domain.Validator.Report;
using Hvex.Exception;
using AutoMapper;
using Hvex.Domain.Dto;
using System;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Hvex.Domain.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepo _reportRepo;
        private readonly IMapper _mapper;

        public ReportService(IReportRepo reportRepo, IMapper mapper)
        {
            _reportRepo = reportRepo;
            _mapper = mapper;

        }

        public async Task<ReportDto> AdicionarReportAsync(ReportDto model) {
            
            Validator(model);
            
            var result = _mapper.Map<Report>(model);

            result.UpdateAt = DateTime.Now;
            result.CreatedAt = DateTime.Now;
            _reportRepo.Adicionar(result);

            if (await _reportRepo.SalvarMudancasAsync()) {

                return model;

            }

            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ERRO_UNKNOWN });
        }

        public async Task<ReportDto> AtualizarReportAsync(ReportDto model) {

            Validator(model);

            //criada
            if (await BuscarReportPorIdAsync(model.Id) == null)
            {
                throw new ErroValidatorException(new List<string> { ResourceMessageErro.REPORT_ID_EXIST });
            }


            var report = await BuscarReportPorIdAsync(model.Id);

            if (report != null) {

                var result = _mapper.Map<Report>(model);

                result.UpdateAt = DateTime.Now;
                result.CreatedAt = DateTime.Now;

                _reportRepo.Atualizar(result);

                if (await _reportRepo.SalvarMudancasAsync()) {

                    return model;
                }
               // return null;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ERRO_UNKNOWN });
        }
        public async Task<ReportDto> BuscarReportPorIdAsync(int? id) 
        {
            ReportDto report;

            try {
               var result = await _reportRepo.BuscarReportPorIdAsync(id);
                report = _mapper.Map<ReportDto>(result);
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }

            if (report != null) {
                return report;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.REPORT_ID_NOT_FOUND });
        }
        public async Task<ReportDto[]> BuscarReportsAsync() 
        {
            ReportDto[] reports;

            try {

                var result = await _reportRepo.BuscarReportsAsync();
                reports = _mapper.Map<ReportDto[]>(result);
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }

            if (reports.Length > 0) {
                return reports;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.NOT_FOUND });
        }
        public async Task<bool> DeletarReportPorIdAsync(ReportDto report) {
            try {
                var reports = _mapper.Map<Report>(report);
                _reportRepo.Deletar(reports);

                if (await _reportRepo.SalvarMudancasAsync()) {
                    return true;
                }
            }
            catch (ErroValidatorException ex) 
            {
                throw ex;
            }

            return false;
        }
        public async Task<bool> UpdateReport(int? id, ReportDto report) {
            var reportId = await BuscarReportPorIdAsync(id);
            if (reportId.Id == report.Id) {
                return true;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ID_NOT_THE_SAME });
        }
        public void Validator(ReportDto report) {
            var validator = new RegisterReportValidator();
            var resultado = validator.Validate(report);
            if (!resultado.IsValid) {
                var messageErro = resultado.Errors.Select(e => e.ErrorMessage);
                throw new ErroValidatorException(messageErro.ToList());
            }
        }
    }
}
