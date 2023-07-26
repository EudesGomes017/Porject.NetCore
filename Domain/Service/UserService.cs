using AutoMapper;
using Hvex.Domain.Dto;
using Hvex.Domain.Entity;
using Hvex.Domain.Interface.Repository;
using Hvex.Domain.Interface.Services;
using Hvex.Domain.Validator.User;
using Hvex.Exception;
using Hvex.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hvex.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;

        }
       
        public async Task<Object> AdicionarUserAsync(UserDto user) {
            List<string> err = new List<string>();
            Validator(user);


            if (await BuscarUserPorIdAsync(user.Id) != null) { err.Add(ResourceMessageErro.USER_EXIST); }
            if(await BuscarUserPorEmailAsync(user.Email) != null) { err.Add(ResourceMessageErro.USER_EMAIL_EXIST); }
            if(err.Count() > 0) { throw new ErroValidatorException(err); }

            var result = _mapper.Map<User>(user); 
            result.UpdateAt = DateTime.Now; 
            result.CreatedAt = DateTime.Now; 
            _userRepo.Adicionar(result); 

            if (await _userRepo.SalvarMudancasAsync()) {
                /*aqui omitimos o id para retorna no objeto ele nao aparecer o id*/
                return new { Name = user.Name, Email = user.Email, Password = user.Password};
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ERRO_UNKNOWN });
        }
        public async Task<UserDto> AtualizarUserAsync(UserDto model) {

            Validator(model);

            var userEmail = await _userRepo.BuscarUserPorEmailAsync(model.Email);

            //esse email e diferente de null && esse diferente de email
            if ( userEmail != null && userEmail.Email != model.Email ) { 
                throw new ErroValidatorException(new List<string> { ResourceMessageErro.USER_EMAIL_EXIST }); 
            }
            var user = await _userRepo.BuscarUserPorIdAsync(model.Id);

            if (user != null){
                var result = _mapper.Map<User>(model);
                result.UpdateAt = DateTime.Now;
                result.CreatedAt = user.CreatedAt; // minha data atual vai receber a data do banco de dados

                _userRepo.Atualizar(result);

                if (await _userRepo.SalvarMudancasAsync())
                {
                    return model;
                }
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ERRO_UNKNOWN });
        }
        public async Task<UserDto> BuscarUserPorIdAsync(int? id){
            UserDto user;

            try {
               var result = await _userRepo.BuscarUserPorIdAsync(id);
                user = _mapper.Map<UserDto>(result);
            }
            catch (ErroValidatorException ex){
                throw ex;
            }

            return user;
            //throw new ErroValidatorException(new List<string> { ResourceMessageErro.USER_ID_NOT_FOUND });
        }
        public async Task<UserDto> BuscarUserPorEmailAsync(string email) {
            UserDto user;

            try {
                var result = await _userRepo.BuscarUserPorEmailAsync(email);
                user = _mapper.Map<UserDto>(result);
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }
            return user;
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.USER_EMAIL_NOT_FOUND });
        }
        public async Task<UserDto[]> BuscarUsersAsync(){
             UserDto[] users;

            try{
                var result = await _userRepo.BuscarUsersAsync();
                users = _mapper.Map<UserDto[]>(result);
            }
            catch (ErroValidatorException ex) {
                throw ex;
            }

            if (users.Length > 0) {
                return users;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.NOT_FOUND });
        }
        public async Task<bool> DeletarUserPorIdAsync(UserDto user)
        {
            try {
               var userCurrent = _mapper.Map<User>(user);
                _userRepo.Deletar(userCurrent);

                if (await _userRepo.SalvarMudancasAsync()) {
                    return true;
                }
            }
            catch (ErroValidatorException ex){
                throw ex;
            }

            return false;
        }
        public async Task<bool> UpdateUser(int? id, UserDto user) {
            var userId = await BuscarUserPorIdAsync(id);
            if (userId.Id == user.Id) { 
                return true;
            }
            throw new ErroValidatorException(new List<string> { ResourceMessageErro.ID_NOT_THE_SAME });
        }
        public void Validator(UserDto user) {
            var validator = new RegisterUserValidator();
            var resultado = validator.Validate(user);
            if (!resultado.IsValid) {
                var messageErro = resultado.Errors.Select(e => e.ErrorMessage);
                throw new ErroValidatorException(messageErro.ToList());
            }
        }
    }

}
