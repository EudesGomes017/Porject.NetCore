using Microsoft.AspNetCore.Mvc;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Hvex.Exception.ExceptionBase;
using System.Collections.Generic;
using Hvex.Exception;
using Hvex.Domain.Dto;

namespace Hvex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        
        [HttpGet(Name = "Buscar todos os usuarios")]
        public async Task<IActionResult> Get([FromServices] IUserService userService) {
            var users = await userService.BuscarUsersAsync();
            return this.StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpGet("{id}", Name= "Buscar usuário pelo ID")]
        public async Task<IActionResult> Get([FromServices] IUserService userService, int id) {
            var user = await userService.BuscarUserPorIdAsync(id);
            return this.StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpPost(Name = "Adcionar Usuário")]
        public async Task<IActionResult> Post([FromServices] IUserService userService, UserDto model) {
            var user = await userService.AdicionarUserAsync(model);
            return this.StatusCode(StatusCodes.Status201Created,user);
        }

        [HttpGet("Buscar/{email}", Name = "Buscar usuário pelo email")]
        public async Task<IActionResult> GetEmail([FromServices] IUserService userService, string email) {
            var user = await userService.BuscarUserPorEmailAsync(email);

            if (user != null) {
                throw new ErroValidatorException(new List<string> { ResourceMessageErro.USER_EMAIL_EXIST });
            }
            return Ok();
        }

        [HttpPut("{id}", Name ="Atulizar usuário")]
        public async Task<IActionResult> Put([FromServices] IUserService userService, int id, UserDto model) {
           
             UserDto user;

            if (await userService.UpdateUser(id, model)) {
                user = await userService.AtualizarUserAsync(model);

                return this.StatusCode(StatusCodes.Status201Created, user);
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.USER_FAIL_DELETE });
        }

        [HttpDelete("{id}", Name="Deletar usuário pelo ID")]
        public async Task<IActionResult> Delete([FromServices] IUserService userService, int id) {
            var user = await userService.BuscarUserPorIdAsync(id);
            await userService.DeletarUserPorIdAsync(user);

            return this.StatusCode(StatusCodes.Status202Accepted, new { messages = ResourceMessageErro.USER_DELETE_SUCCESS});
        }
    }
}
