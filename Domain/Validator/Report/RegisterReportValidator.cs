using FluentValidation;
using Hvex.Domain.Dto;
using Hvex.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hvex.Domain.Validator.Report {
    public class RegisterReportValidator : AbstractValidator<ReportDto> {
        public RegisterReportValidator() {
            RuleFor(n => n.Name).NotEmpty().WithMessage(ResourceMessageErro.NAME_TRANSFORMER_EMPTY);
            RuleFor(t => t.TransformerId).NotEmpty().WithMessage(ResourceMessageErro.ID_TRANSFORMER_EMPTY);
            RuleFor(ts => ts.TestId).NotEmpty().WithMessage(ResourceMessageErro.ID_TEST_EMPTY);
            RuleFor(s => s.Status).NotEmpty().WithMessage(ResourceMessageErro.TEST_STATUS_EMPTY);
        }
    }
}
