using AutoMapper;
using GraphQL.Address.Application.Dtos.Person;
using GraphQL.EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace GraphQL.Address.API.EventBusConsumer
{
    public class UpdatePersonConsumer : IConsumer<UpdatePersonEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePersonConsumer> _logger;

        public UpdatePersonConsumer(IMediator mediator, IMapper mapper, ILogger<UpdatePersonConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task Consume(ConsumeContext<UpdatePersonEvent> context)
        {
            try
            {
                var command = _mapper.Map<UpdatePersonEventDto>(context.Message);
                var result = await _mediator.Send(command);

                _logger.LogInformation("UpdatePersonEvent consumed successfully. Updated Person Id: {newPersonId}", result);

            }
            catch (Exception ex)
            {
                _logger.LogError("DeletePersonEvent was consumed unsuccessfully. {message}", ex.Message);
                throw;
            }
        }
    }
}
