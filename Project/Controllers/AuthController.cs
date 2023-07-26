using Hvex.Domain.Dto;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Services;
using Hvex.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hvex.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller {

        [HttpPost(Name ="Logar no sistema")]
        public async Task<IActionResult> Authentication([FromServices] IUserService userService, AuthDto user) {
            var userFind = await userService.BuscarUserPorEmailAsync(user.Email);

            if(userFind == null) {
                return this.StatusCode(StatusCodes.Status401Unauthorized, new { messages = ResourceMessageErro.USER_FAIL_LOGIN });
            }

          
            if (userFind.Password == user.Password) {
               var userReturn = new {Id = userFind.Id, Name = userFind.Name, Email = userFind.Email};
   
                return this.StatusCode(StatusCodes.Status200OK, userReturn);
            }
            return this.StatusCode(StatusCodes.Status401Unauthorized, new { messages = ResourceMessageErro.USER_FAIL_LOGIN });
        }
    }
}
