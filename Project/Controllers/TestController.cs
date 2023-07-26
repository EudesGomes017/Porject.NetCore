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
    public class TestController : ControllerBase
    {
        [HttpGet(Name = "Buscar todos os teste")]
        public async Task<IActionResult> Get([FromServices] ITestService testService) {
            var Tests = await testService.BuscarTestsAsync();
            return this.StatusCode(StatusCodes.Status200OK, Tests);
        }

        [HttpGet("{id}", Name = "Buscar teste pelo ID")]
        public async Task<IActionResult> Get([FromServices] ITestService testService, int id) {
            var Test = await testService.BuscarTestPorIdAsync(id);
            return this.StatusCode(StatusCodes.Status200OK, Test);
        }

        [HttpPost(Name = "Adcionar teste")]
        public async Task<IActionResult> Post([FromServices] ITestService testService, TestDto model) {
            var Test = await testService.AdicionarTestAsync(model);
            return this.StatusCode(StatusCodes.Status201Created, Test);
        }

        [HttpPut("{id}", Name = "Atulizar teste")]
        public async Task<IActionResult> Put([FromServices] ITestService testService, int id, TestDto model) {

            TestDto Test;

            if (await testService.UpdateTest(id, model)) {
                Test = await testService.AtualizarTestAsync(model);

                return this.StatusCode(StatusCodes.Status201Created, Test);
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.TEST_FAIL_DELETE });
        }

        [HttpDelete("{id}", Name = "Deletar teste pelo ID")]
        public async Task<IActionResult> Delete([FromServices] ITestService testService, int id) {
            var Test = await testService.BuscarTestPorIdAsync(id);
            await testService.DeletarTestPorIdAsync(Test);

            return this.StatusCode(StatusCodes.Status202Accepted, new { messages = ResourceMessageErro.TEST_DELETE_SUCCESS });
        }
    }
}
