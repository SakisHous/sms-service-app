using AutoMapper;
using Events;
using Moq;
using SmsApp.Data;
using SmsApp.Repositories;
using SmsApp.Senders;

namespace SmsApp.Tests.Senders
{
    public class SmsVendorCYServiceTest
    {
        private readonly Mock<ISmsRepository> mockRepository;
        private readonly SmsVendorCYSender vendorCYSender;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;

        public SmsVendorCYServiceTest()
        {
            mockRepository = new Mock<ISmsRepository>();

            _config = new MapperConfiguration(cfg => cfg.AddMaps(new[] {
                    "SmsApp"
             }));

            _mapper = _config.CreateMapper();

            vendorCYSender = new SmsVendorCYSender(mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task TestSend_WithMessageOver160Characters()
        {
            mockRepository.Setup(r => r.AddSmsAsync(It.IsAny<ShortMessage>()))
                          .ReturnsAsync(true);

            // Sms Event Object
            var smsEvent = new SmsEvent()
            {
                MessageBody = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qu",
                SenderCountryCode = "+357",
                Sender = "6951234567",
                RecipientCountryCode = "+357",
                Recipient = "6901234567",
                Vendor = "smsCYVendor"
            };


            var result = await vendorCYSender.Send(smsEvent);



            Assert.True(result);
            Assert.Equal(2, mockRepository.Invocations.Count);
        }

        [Fact]
        public void TestMapper()
        {
            // Sms Event Object
            var smsEvent = new SmsEvent()
            {
                MessageBody = "Test ShortMessage",
                SenderCountryCode = "+357",
                Sender = "6951234567",
                RecipientCountryCode = "+357",
                Recipient = "6901234567",
                Vendor = "smsGrVendor"
            };

            var message = _mapper.Map<ShortMessage>(smsEvent);

            Assert.NotNull(message);
            Assert.Equal("Test ShortMessage", message.MessageBody);
        }
    }
}
