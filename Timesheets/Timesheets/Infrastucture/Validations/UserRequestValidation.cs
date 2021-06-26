using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Timesheets.Models.Dto;

namespace Timesheets.Infrastucture.Validations
{
    public class UserRequestValidation : AbstractValidator<CreateUserRequest>
    {
        public UserRequestValidation()
        {
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage(ErrorMessage.PasswordLenght);
        }
    }
}