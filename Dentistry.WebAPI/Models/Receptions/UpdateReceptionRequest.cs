using FluentValidation;
using FluentValidation.Results;

namespace Dentistry.WebAPI.Models;
using Dentistry.Entities.Models;
public class UpdateReceptionRequest
{
    #region Model

    public Status Status { get; set; }

    #endregion

    #region Validator

    public class Validator : AbstractValidator<UpdateReceptionRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Status).NotNull();
        }
    }

    #endregion
}

public static class UpdateReceptionRequestExtension
{
    public static ValidationResult Validate(this UpdateReceptionRequest model)
    {
        return new UpdateReceptionRequest.Validator().Validate(model);
    }
}