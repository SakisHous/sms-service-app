using AutoMapper;
using Events;
using Moq;
using SmsApp.Data;
using SmsApp.Repositories;
using SmsApp.Senders;

namespace SmsApp.Tests.Senders
{
    public class SmsVendorRestServiceTest
    {
        private readonly Mock<ISmsRepository> mockRepository;
        private readonly SmsVendorRestSender vendorRestSender;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;

        public SmsVendorRestServiceTest()
        {
            mockRepository = new Mock<ISmsRepository>();

            _config = new MapperConfiguration(cfg => cfg.AddMaps(new[] {
                    "SmsApp"
             }));

            _mapper = _config.CreateMapper();

            vendorRestSender = new SmsVendorRestSender(mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task TestSend()
        {
            mockRepository.Setup(r => r.AddSmsAsync(It.IsAny<ShortMessage>()))
                          .ReturnsAsync(true);

            // Sms Event Object
            var smsEvent = new SmsEvent()
            {
                MessageBody = "Test ShortMessage",
                SenderCountryCode = "+31",
                Sender = "6951234567",
                RecipientCountryCode = "+31",
                Recipient = "6901234567",
                Vendor = "smsRestVendor"
            };


            var result = await vendorRestSender.Send(smsEvent);

            Assert.True(result);
        }

        [Fact]
        public void TestMapper()
        {
            // Sms Event Object
            var smsEvent = new SmsEvent()
            {
                MessageBody = "Test ShortMessage",
                SenderCountryCode = "+30",
                Sender = "6951234567",
                RecipientCountryCode = "+30",
                Recipient = "6901234567",
                Vendor = "smsRestVendor"
            };

            var message = _mapper.Map<ShortMessage>(smsEvent);

            Assert.NotNull(message);
            Assert.Equal("Test ShortMessage", message.MessageBody);
            Assert.Equal("smsRestVendor", message.Vendor);
        }
    }
}
