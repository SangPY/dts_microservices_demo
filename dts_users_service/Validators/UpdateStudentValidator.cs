using dts_users_service.Dto;
using FluentValidation;

namespace dts_users_service.Validators
{

    public class UpdateStudentValidator
        : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.StudentName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Age)
                .InclusiveBetween(5, 80);

            RuleFor(x => x.Class)
                .NotEmpty();

            RuleFor(x => x.City)
                .NotEmpty();
        }
    }
}
