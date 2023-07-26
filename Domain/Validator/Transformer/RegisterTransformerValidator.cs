using FluentValidation;
using Hvex.Domain.Dto;
using Hvex.Exception;


namespace Hvex.Domain.Validator.Transformer {
    public class RegisterTransformerValidator : AbstractValidator<TransformerDto> {
        public RegisterTransformerValidator() {
            RuleFor(n => n.Name).NotEmpty().WithMessage(ResourceMessageErro.NAME_TRANSFORMER_EMPTY);
            RuleFor(i => i.InternalNumber).NotEmpty().WithMessage(ResourceMessageErro.NUMBER_TRANSFORMER_EMPTY);
            RuleFor(tc => tc.TensionClass).NotEmpty().WithMessage(ResourceMessageErro.TENSION_CLASS_TRANSFORMER_EMPTY);
            RuleFor(p => p.Potency).NotEmpty().WithMessage(ResourceMessageErro.POTENCY_TRANSFORMER_EMPTY);
            RuleFor(c => c.Current).NotEmpty().WithMessage(ResourceMessageErro.CURRENT_TRANSFORMER_EMPTY);
            RuleFor(u => u.UserId).NotEmpty().WithMessage(ResourceMessageErro.ID_USER_TRANSFORMER_EMPTY);
        }
    }
}
