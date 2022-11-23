using FluentValidation;
using FluentValidation.Results;
using Dentistry.Entities.Models;
namespace Dentistry.WebAPI.Models;

public class UpdateDoctorRequest : UpdateUserRequest
{
    
    #region Model

    public short ReceptionRoom { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
    #endregion

    #region Validator
    public static short MAX_RECEPTION_ROOM = 255;
    public class Validator : AbstractValidator<UpdateDoctorRequest>
    {
        public Validator()
        {
            RuleFor(x => x.ReceptionRoom).NotNull().When(x => x.ReceptionRoom >=1 && x.ReceptionRoom <=MAX_RECEPTION_ROOM);
        }
    }

    #endregion
}

public static class UpdateDoctorRequestExtension
{
    public static ValidationResult Validate(this UpdateDoctorRequest model)
    {
        return new UpdateDoctorRequest.Validator().Validate(model);
    }
}