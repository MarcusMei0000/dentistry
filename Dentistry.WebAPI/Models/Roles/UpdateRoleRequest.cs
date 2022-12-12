using FluentValidation;
using FluentValidation.Results;

namespace Dentistry.WebAPI.Models;

public class UpdateRoleRequest
{
    #region Model

    public string RoleName  { get; set; }

    #endregion

    #region Validator

    public class Validator : AbstractValidator<UpdateRoleRequest>
    {
        public Validator()
        {
            RuleFor(x => x.RoleName)
                .MaximumLength(255).WithMessage("Length must be less than 256");
        }
    }

    #endregion
}

public static class UpdateRoleRequestExtension
{
    public static ValidationResult Validate(this UpdateRoleRequest model)
    {
        return new UpdateRoleRequest.Validator().Validate(model);
    }
}