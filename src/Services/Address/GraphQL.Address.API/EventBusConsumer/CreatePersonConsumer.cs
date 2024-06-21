using AutoMapper;
using GraphQL.Address.Application.Dtos.Person;
using GraphQL.EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace GraphQL.Address.API.EventBusConsumer
{
    public class CreatePersonConsumer : IConsumer<CreatePersonEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePersonConsumer> _logger;

        public CreatePersonConsumer(IMediator mediator, IMapper mapper, ILogger<CreatePersonConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task Consume(ConsumeContext<CreatePersonEvent> context)
        {
            try
            {
                var command = _mapper.Map<CreatePersonEventDto>(context.Message);
                var result = await _mediator.Send(command);

                _logger.LogInformation("CreatePersonEvent consumed successfully. Create Person Id: {newPersonId}", result);

            }
            catch (Exception ex)
            {
                _logger.LogError("CreatePersonEvent consumed unsuccessfully. {message}", ex.Message);
                throw;
            }
        }
    }
}
