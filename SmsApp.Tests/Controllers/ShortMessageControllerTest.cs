using AutoMapper;
using Events;
using FluentValidation;
using SmsApp.DTO;
using SmsApp.Validators;

namespace SmsApp.Tests.Controllers
{
    public class ShortMessageControllerTest
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;

        public ShortMessageControllerTest()
        {

            _config = new MapperConfiguration(cfg => cfg.AddMaps(new[] {
                "SmsApp"
            }));
            _mapper = _config.CreateMapper();
        }

        [Fact]
        public void TestMapper()
        {
            var smsRequest = new SmsRequest()
            {
                MessageBody = "Test ShortMessage",
                SenderCountryCode = "+357",
                Sender = "6951234567",
                RecipientCountryCode = "+357",
                Recipient = "6901234567",
            };

            var smsEvent = _mapper.Map<SmsEvent>(smsRequest);

            Assert.NotNull(smsEvent);
            Assert.Equal("Test ShortMessage", smsEvent.MessageBody);
            Assert.Equal("smsVendorCY", smsEvent.Vendor);
        }

        [Fact]
        public void SubmitMessage_BadValidation()
        {
            var validator = new SmsValidator();

            var smsRequest = new SmsRequest()
            {
                MessageBody = "Test ShortMessage",
                SenderCountryCode = "1234",
                Sender = "abcde",
                RecipientCountryCode = "+357",
                Recipient = "abcd",
            };

            var validationResult = validator.Validate(smsRequest);
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            Assert.Equal(3, validationResult.Errors.Count);
        }

        [Fact]
        public void SubmitMessage_SuccessValidation()
        {
            var validator = new SmsValidator();

            var smsRequest = new SmsRequest()
            {
                MessageBody = "Τεστ",
                SenderCountryCode = "+30",
                Sender = "6951234567",
                RecipientCountryCode = "+30",
                Recipient = "6951234567",
            };

            var validationResult = validator.Validate(smsRequest);
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            Assert.Empty(validationResult.Errors);
        }
    }
}
