using AutoMapper;
using Events;
using SmsApp.Data;
using SmsApp.Repositories;
using System.Text;

namespace SmsApp.Senders
{
    public class SmsVendorCYSender : ISmsSender
    {
        private readonly ISmsRepository _smsRepository;
        private readonly IMapper _mapper;

        public SmsVendorCYSender(ISmsRepository smsRepository, IMapper mapper)
        {
            _smsRepository = smsRepository;
            _mapper = mapper;
        }

        public async Task<bool> Send(SmsEvent smsEvent)
        {
            string msg = smsEvent.MessageBody!;

            char[] charsToBeSent = msg.ToCharArray();
            StringBuilder sb = new();

            foreach (char c in charsToBeSent)
            {
                sb.Append(c);

                if (sb.Length > 160)
                {
                    var message = _mapper.Map<ShortMessage>(smsEvent);
                    message.MessageBody = sb.ToString();
                    sb = new StringBuilder();

                    await _smsRepository.AddSmsAsync(message);

                }
            }

            if (sb.Length > 0)
            {
                var message = _mapper.Map<ShortMessage>(smsEvent);
                message.MessageBody = sb.ToString();
                await _smsRepository.AddSmsAsync(message);
            }

            return true;
        }
    }
}
