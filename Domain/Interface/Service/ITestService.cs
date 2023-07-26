using Hvex.Domain.Dto;
using Hvex.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hvex.Domain.Interface.Services
{
    public interface ITestService
    {
        Task<TestDto> AdicionarTestAsync(TestDto test);
        Task<TestDto> AtualizarTestAsync(TestDto test);
        Task<TestDto> BuscarTestPorIdAsync(int? id);
        Task<TestDto[]> BuscarTestsAsync();
        Task<bool> DeletarTestPorIdAsync(TestDto test);
        void Validator(TestDto test);
        Task<bool> UpdateTest(int? id, TestDto test);
    }
}
