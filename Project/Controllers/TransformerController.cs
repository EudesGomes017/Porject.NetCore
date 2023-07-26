using Hvex.Domain.Dto;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Services;
using Hvex.Domain.Services;
using Hvex.Exception;
using Hvex.Exception.ExceptionBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hvex.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TransformerController : ControllerBase  {

        [HttpGet(Name ="Buscar todos os transformer")]
        public async Task<IActionResult> Get([FromServices] ITransformerService transformerService) {
            var transformers = await transformerService.BuscarTransformersAsync();
            return this.StatusCode(StatusCodes.Status200OK, transformers);
        }

        [HttpGet("{id}", Name ="Buscar transformer pelo ID")]
        public async Task<IActionResult> Get([FromServices] ITransformerService transformerService, int id) {
            var transformer = await transformerService.BuscarTransformerPorIdAsync(id);
            return this.StatusCode(StatusCodes.Status200OK, transformer);
        }

        [HttpPost(Name ="Adicionar Transformer")]
        public async Task<IActionResult> Post([FromServices] ITransformerService transformerService, TransformerDto model) {
            var transformer = await transformerService.AdicionarTransformerAsync(model);
            return this.StatusCode(StatusCodes.Status201Created, transformer);
        }

        [HttpPut("{id}", Name ="Atualizar Transformer")]
        public async Task<IActionResult> Put([FromServices] ITransformerService transformerService, int id, TransformerDto model) {
            TransformerDto transformer;

            if (await transformerService.UpdateTransformer(id, model)) {
                transformer = await transformerService.AtualizarTransformerAsync(model);

                return this.StatusCode(StatusCodes.Status201Created, transformer);
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.TRANSFORMER_FAIL_DELETE });
        }

        [HttpDelete("{id}", Name="Deletar Transformer pelo ID")]
        public async Task<IActionResult> Delete([FromServices] ITransformerService transformerService, int id) {
            var transformer = await transformerService.BuscarTransformerPorIdAsync(id);
            await transformerService.DeletarTransformerPorIdAsync(transformer);

            return this.StatusCode(StatusCodes.Status202Accepted, new { messages = ResourceMessageErro.TRANSFORMER_DELETE_SUCCESS });
        }
    }
}
