using FluentValidation;
using FluentValidation.Results;

namespace Dentistry.WebAPI.Models;
using Dentistry.Entities.Models;
public class UpdateScheduleRequest
{
    #region Model

    public DateTime ReceptionStart { get; set; }
    public DateTime ReceptionEnd { get; set; }

    public ICollection<Reception> Receptions { get; set; }

    #endregion

    #region Validator

    public class Validator : AbstractValidator<UpdateScheduleRequest>
    {
        public Validator()
        {
            RuleFor(x => x.ReceptionStart).NotNull().When(x => x.ReceptionStart <= x.ReceptionEnd);
            RuleFor(x => x.ReceptionEnd).NotNull().When(x => x.ReceptionStart <= x.ReceptionEnd);
        }
    }

    #endregion
}

public static class UpdateScheduleRequestExtension
{
    public static ValidationResult Validate(this UpdateScheduleRequest model)
    {
        return new UpdateScheduleRequest.Validator().Validate(model);
    }
}