﻿using AutoMapper;
using Events;
using SmsApp.Data;
using SmsApp.Repositories;

namespace SmsApp.Senders
{
    public class SmsVendorGRSender : ISmsSender
    {
        private readonly ISmsRepository _smsRepository;
        private readonly IMapper _mapper;

        public SmsVendorGRSender(ISmsRepository smsRepository, IMapper mapper)
        {
            _smsRepository = smsRepository;
            _mapper = mapper;
        }

        public async Task<bool> Send(SmsEvent smsEvent)
        {
            var message = _mapper.Map<ShortMessage>(smsEvent);
            return await _smsRepository.AddSmsAsync(message);
        }
    }
}
