using dts_users_service.Dto;
using FluentValidation;

namespace dts_users_service.Validators
{
    public class CreateStudentValidator
        : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.StudentName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Age)
                .NotNull()
                .InclusiveBetween(5, 80);

            RuleFor(x => x.Class)
                .NotEmpty();

            RuleFor(x => x.City)
                .NotEmpty();
        }
    }
}
