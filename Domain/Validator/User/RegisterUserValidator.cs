using FluentValidation;
using System.Text.RegularExpressions;
using Hvex.Exception;
using System;
using Hvex.Domain.Dto;

namespace Hvex.Domain.Validator.User {
    public class RegisterUserValidator : AbstractValidator<UserDto> {
        public RegisterUserValidator() {
            RuleFor(n => n.Name).NotEmpty().WithMessage(ResourceMessageErro.NAME_USER_EMPTY);
            RuleFor(e => e.Email).NotEmpty().WithMessage(ResourceMessageErro.EMAIL_USER_EMPTY);
            When(e => !string.IsNullOrWhiteSpace(e.Email), () => {
                RuleFor(e => e.Email).EmailAddress().WithMessage(ResourceMessageErro.EMAIL_USER_INVALID);
            });
            RuleFor(p => p.Password).NotEmpty().WithMessage(ResourceMessageErro.PASSWORD_USER_EMPTY);
            When(p => !string.IsNullOrWhiteSpace(p.Password), () => {
                RuleFor(p => p.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessageErro.PASSWORD_MIN_SIX_CHARACTERS);
            });
        }
    }
}
