using FluentValidation;
using FluentValidation.Results;

namespace Dentistry.WebAPI.Models;

public class CreateDoctorRequest : CreateUserRequest
{
    #region Model
    public string Speciality { get; set; }
    public string? PhotoLink { get; set; }


    #endregion

    #region Validator

    public class Validator : AbstractValidator<CreateDoctorRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Speciality).NotNull();
        }
    }

    #endregion
}

public static class CreateDoctorRequestExtension
{
    public static ValidationResult Validate(this CreateDoctorRequest model)
    {
        return new CreateDoctorRequest.Validator().Validate(model);
    }
}