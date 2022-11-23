using FluentValidation;
using FluentValidation.Results;
using Dentistry.Entities.Models;
namespace Dentistry.WebAPI.Models;

public class CreateScheduleRequest
{
    #region Model
    public Guid DoctorId { get; set; }

    public DateTime ReceptionStart { get; set; }
    public DateTime ReceptionEnd { get; set; }

    public ICollection<Reception> Receptions { get; set; }


    #endregion

    #region Validator

    public class Validator : AbstractValidator<CreateScheduleRequest>
    {
        public Validator()
        {
            RuleFor(x => x.DoctorId).NotNull();
            RuleFor(x => x.ReceptionStart).NotNull().When(x => x.ReceptionStart < x.ReceptionEnd);
            RuleFor(x => x.ReceptionEnd).NotNull().When(x => x.ReceptionStart < x.ReceptionEnd);
            RuleFor(x => x.Receptions).NotNull();
        }
    }

    #endregion
}

public static class CreateScheduleRequestExtension
{
    public static ValidationResult Validate(this CreateScheduleRequest model)
    {
        return new CreateScheduleRequest.Validator().Validate(model);
    }
}