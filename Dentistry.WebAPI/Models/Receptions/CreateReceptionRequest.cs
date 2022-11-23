using FluentValidation;
using FluentValidation.Results;
using Dentistry.Entities.Models;
namespace Dentistry.WebAPI.Models;

public class CreateReceptionRequest
{
   #region Model

    public Guid PatientId { get; set; }
    public DateTime ReceptionDateTimeStart { get; set; }    
    public DateTime ReceptionDateTimeFinish { get; set; }
    public Guid ScheduleId { get; set; }
    public Status Status { get; set; }

    #endregion

    #region Validator

    public class Validator : AbstractValidator<CreateReceptionRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Status).NotNull();
            RuleFor(x => x.PatientId).NotNull();
            RuleFor(x => x.ReceptionDateTimeStart).NotNull().When(x => x.ReceptionDateTimeStart < x.ReceptionDateTimeFinish);
            RuleFor(x => x.ReceptionDateTimeFinish).NotNull().When(x => x.ReceptionDateTimeStart < x.ReceptionDateTimeFinish);
            RuleFor(x => x.ScheduleId).NotNull();
        }
    }

    #endregion
}

public static class CreateReceptionRequestExtension
{
    public static ValidationResult Validate(this CreateReceptionRequest model)
    {
        return new CreateReceptionRequest.Validator().Validate(model);
    }
}