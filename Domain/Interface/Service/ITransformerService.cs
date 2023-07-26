using Hvex.Domain.Dto;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hvex.Domain.Interface.Services
{
    public interface ITransformerService
    {
        Task<TransformerDto> AdicionarTransformerAsync(TransformerDto transformer);
        Task<TransformerDto> AtualizarTransformerAsync(TransformerDto transformer);
        Task<TransformerDto[]> BuscarTransformersAsync();
        Task<TransformerDto> BuscarTransformerPorIdAsync(int? id);
        Task<bool> DeletarTransformerPorIdAsync(TransformerDto transformer);
        void Validator(TransformerDto transformer);
        Task<bool> UpdateTransformer(int? id, TransformerDto transformer);


    }
}
