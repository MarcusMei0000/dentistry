using FluentValidation;
using FluentValidation.Results;
using Dentistry.Entities.Models;
namespace Dentistry.WebAPI.Models;

public class CreateUserRequest
{
    #region Model
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Patronymic { get; set; }
    public Role Role { get; set; }


    #endregion

    #region Validator

    public class Validator : AbstractValidator<CreateUserRequest>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(255).WithMessage("Length must be less than 256");
            RuleFor(x => x.LastName)
                .MaximumLength(255).WithMessage("Length must be less than 256");
            RuleFor(x => x.Patronymic)
                .MaximumLength(255).WithMessage("Length must be less than 256");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Role).NotNull();
        }
    }

    #endregion
}

public static class CreateUserRequestExtension
{
    public static ValidationResult Validate(this CreateUserRequest model)
    {
        return new CreateUserRequest.Validator().Validate(model);
    }
}