using AutoMapper;
using Hvex.Domain.Dto;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Repository;
using Hvex.Domain.Interface.Services;
using Hvex.Domain.Validator.Transformer;
using Hvex.Exception;
using Hvex.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hvex.Domain.Services
{
    public class TransformerService : ITransformerService
    {
        private readonly ITransformerRepo _transformerRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public TransformerService(ITransformerRepo transformerRepo, IUserRepo userRepo, IMapper mapper)
        {
            _transformerRepo = transformerRepo;
            _userRepo = userRepo;
            _mapper = mapper;

        }

        public async Task<TransformerDto> AdicionarTransformerAsync(TransformerDto model) {

            List<string> err = new List<string>();
            Validator(model);



            if (await _userRepo.BuscarUserPorIdAsync(model.UserId) == null) { err.Add(ResourceMessageErro.USER_ID_NOT_FOUND); }
            if (await _transformerRepo.BuscarTransformerPorIdAsync(model.Id) != null) { err.Add(ResourceMessageErro.TRANSFORMER_EXIST); }
            if (err.Count() > 0) { throw new ErroValidatorException(err); }

            var result = _mapper.Map<Transformer>(model);
            result.UpdateAt = DateTime.Now;
            result.CreatedAt = DateTime.Now;
            _userRepo.Adicionar(result);


            try {
                _transformerRepo.Adicionar(model);

                if (await _transformerRepo.SalvarMudancasAsync()) {
                    return model;
                }
            }catch (ErroValidatorException ex) {
                throw ex;
            }

            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ERRO_UNKNOWN });

        }
        public async Task<TransformerDto> AtualizarTransformerAsync(TransformerDto model) {

            Validator(model);



            var transformer = await _transformerRepo.BuscarTransformerPorIdAsync(model.Id);



            if (transformer != null)
            {
                var result = _mapper.Map<Transformer>(model);
                result.UpdateAt = DateTime.Now;
                result.CreatedAt = transformer.CreatedAt;

                _transformerRepo.Atualizar(result);

                if (await _transformerRepo.SalvarMudancasAsync())
                {
                    return model;
                }
            }
            return null;
        }
        public async Task<TransformerDto> BuscarTransformerPorIdAsync(int? id)
        {
            TransformerDto transf;

            try {
              var  resul = await _transformerRepo.BuscarTransformerPorIdAsync(id);
                transf = _mapper.Map<TransformerDto>(resul);
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }

            if (transf != null) {
                return transf;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.TRANSFORMER_ID_NOT_FOUND });
        }
        public async Task<TransformerDto[]> BuscarTransformersAsync()
        {
            TransformerDto[] transformers;

            try {
              var  result = await _transformerRepo.BuscarTransformersAsync();
                transformers = _mapper.Map<TransformerDto[]>(result);
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }

            if (transformers.Length > 0) {
                return transformers;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.NOT_FOUND });
        }
        public async Task<bool> DeletarTransformerPorIdAsync(TransformerDto transformer){
            try {
                var transf = _mapper.Map<Transformer>(transformer);
                _transformerRepo.Deletar(transf);

                if (await _userRepo.SalvarMudancasAsync()) {
                    return true;
                }
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }
            return false;
        }
        public async Task<bool> UpdateTransformer(int? id, TransformerDto transformer) {
            var transformerId = await BuscarTransformerPorIdAsync(id);
            if (transformerId.Id == transformer.Id) {
                return true;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ID_NOT_THE_SAME });
        }
        public void Validator(TransformerDto transformer) {
            var validator = new RegisterTransformerValidator();
            var resultado = validator.Validate(transformer);
            if (!resultado.IsValid) {
                var messageErro = resultado.Errors.Select(e => e.ErrorMessage);
                throw new ErroValidatorException(messageErro.ToList());
            }
        }
    }
}
