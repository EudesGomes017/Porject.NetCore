using AutoMapper;
using Hvex.Domain.Dto;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Repository;
using Hvex.Domain.Interface.Services;
using Hvex.Domain.Validator.Test;
using Hvex.Domain.Validator.User;
using Hvex.Exception;
using Hvex.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hvex.Domain.Services
{
    public class TestService : ITestService
    {
        public readonly ITestRepo _testRepo;
        private readonly ITransformerRepo _transformerRepo;
        private readonly IMapper _mapper;
        private ITestService @object;

        public TestService()
        {

        }

        public TestService(ITestService @object)
        {
            this.@object = @object;
        }

        public TestService(ITestRepo testRepo, ITransformerRepo transformerRepo, IMapper mapper)
        {
            _testRepo = testRepo;
            _transformerRepo = transformerRepo;
            _mapper = mapper;

        }
        public async Task<TestDto> AdicionarTestAsync(TestDto model)
        {
            Validator(model);



            if (await BuscarTestPorIdAsync(model.Id) != null) {
                throw new ErroValidatorException(new List<string> { ResourceMessageErro.TEST_ID_EXIST });
            }

            var result = _mapper.Map<Test>(model);

            result.UpdateAt = DateTime.Now;
            result.CreatedAt = DateTime.Now;
            _testRepo.Adicionar(result);
           


            if (await _testRepo.SalvarMudancasAsync()) {
                return model;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ERRO_UNKNOWN });
        }

        public async Task<TestDto> AtualizarTestAsync(TestDto model)
        {
            Validator(model);

            if (await BuscarTestPorIdAsync(model.Id) == null) {
                throw new ErroValidatorException(new List<string> { ResourceMessageErro.TEST_ID_EXIST });
            }

            var test = await BuscarTestPorIdAsync(model.Id);

            //criada
            if (test != null)
            {
                var result = _mapper.Map<Test>(model);

                result.UpdateAt = DateTime.Now;
                result.CreatedAt = DateTime.Now;

                _testRepo.Atualizar(result);

                if (await _testRepo.SalvarMudancasAsync())
                {

                    return model;
                }

                return null;
                

            }

            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ERRO_UNKNOWN });
        }
        public async Task<TestDto> BuscarTestPorIdAsync(int? id)
        {
            TestDto test;

            try {
               var result = await _testRepo.BuscarTestPorIdAsync(id);
                test = _mapper.Map<TestDto>(result);
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }

            return test;
        }
        public async Task<TestDto[]> BuscarTestsAsync()
        {
            TestDto[] tests;

            try {
              var  result = await _testRepo.BuscarTestsAsync();
                tests = _mapper.Map<TestDto[]>(result);
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }

            if (tests.Length > 0) {
                return tests;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.NOT_FOUND });
        }
        public async Task<bool> DeletarTestPorIdAsync(TestDto test) {
            try {
                var testf = _mapper.Map<Test>(test);
                _testRepo.Deletar(testf);

                if (await _testRepo.SalvarMudancasAsync()) {
                    return true;
                }
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }

            return false;
        }
        public async Task<bool> UpdateTest(int? id, TestDto test) {
            var testId = await BuscarTestPorIdAsync(id);
            if (testId.Id == test.Id) {
                return true;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ID_NOT_THE_SAME });
        }
        public void Validator(TestDto test) {
            var validator = new RegisterTestValidator();
            var resultado = validator.Validate(test);
            if (!resultado.IsValid) {
                var messageErro = resultado.Errors.Select(e => e.ErrorMessage);
                throw new ErroValidatorException(messageErro.ToList());
            }
        }
    }
}
