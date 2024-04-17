using AutoMapper;
using Events;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SmsApp.DTO;

namespace SmsApp.Controllers
{

    [Route("api/v1/message")]
    [ApiController]
    public class ShortMessageController : ControllerBase
    {
        private readonly IValidator<SmsRequest> _validator;
        private readonly IMapper? _mapper;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ShortMessageController(
            IValidator<SmsRequest> validator, 
            IMapper mapper,
            ISendEndpointProvider sendEndpointProvider)
        {
            _validator = validator;
            _mapper = mapper;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitMessage([FromBody] SmsRequest request)
        {
            // Validates Request ShortMessage
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            var messageEvent = _mapper!.Map<SmsEvent>(request);

            // Publish message to the specified endpoint
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(
                new Uri($"rabbitmq://localhost/{messageEvent.Vendor}")
            );

            // Send the message to the specified endpoint
            await endpoint.Send(messageEvent);

            return Ok(new {
                status = "success",
                message = "Sms has been received"
            });
        }
    }
}
