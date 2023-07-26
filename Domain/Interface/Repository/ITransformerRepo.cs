using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hvex.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Hvex.Domain.Interface.Repository {
    public interface ITransformerRepo : IGeralRepo {
        Task<Transformer[]> BuscarTransformersAsync();
        Task<Transformer> BuscarTransformerPorIdAsync(int? transformerId);
    }
}
