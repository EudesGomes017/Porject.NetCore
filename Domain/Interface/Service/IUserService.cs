using Hvex.Domain.Dto;
using Hvex.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hvex.Domain.Interface.Services
{
    public interface IUserService
    {
        Task<Object> AdicionarUserAsync(UserDto user);
        Task<UserDto> AtualizarUserAsync(UserDto user);
        Task<UserDto> BuscarUserPorEmailAsync(string email);
        Task<UserDto> BuscarUserPorIdAsync(int? id);
        Task<UserDto[]> BuscarUsersAsync();
        Task<bool> DeletarUserPorIdAsync(UserDto user);
        void Validator(UserDto user);
        Task<bool> UpdateUser(int? id, UserDto user);


    }
}
