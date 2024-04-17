using FluentValidation;
using SmsApp.DTO;

namespace SmsApp.Validators
{
    public class SmsValidator : AbstractValidator<SmsRequest>
    {
        public SmsValidator()
        {
            RuleFor(s => s.MessageBody).NotEmpty()
                .WithMessage("Sms should not be empty")
                .MaximumLength(480)
                .WithMessage("Sms should not exceed 480 characters")
                .Matches(@"^[Α-Ωα-ωίϊΐόάέύϋΰήώ0-9!?@#$%^&*\'\΄\s\t]*$")
                .When(x => x.RecipientCountryCode!.Equals("+30"))
                .WithMessage("Sms in Greek vendors support only greek characters and numbers");

            RuleFor(s => s.RecipientCountryCode).NotEmpty()
                .WithMessage("Recipient's phone code should not be empty")
                .Matches(@"^\+\d+$")
                .WithMessage("Invalid recipient's country code");

            RuleFor(s => s.Recipient).NotEmpty()
                .WithMessage("Recipient's phone should not be empty")
                .Matches(@"^\+?[0-9]{5,15}$")
                .WithMessage("Invalid mobile phone");

            RuleFor(s => s.SenderCountryCode).NotEmpty()
                .WithMessage("Sender's phone code should not be empty")
                .Matches(@"^\+\d+$")
                .WithMessage("Invalid sender's country code");

            RuleFor(s => s.Sender).NotEmpty()
                .WithMessage("Sender's phone should not be empty")
                .Matches(@"^\+?[0-9]{5,15}$")
                .WithMessage("Invalid mobile phone");
        }
    }
}
