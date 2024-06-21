using AutoMapper;
using GraphQL.Address.Application.Dtos.Person;
using GraphQL.EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace GraphQL.Address.API.EventBusConsumer
{
    public class DeletePersonConsumer : IConsumer<DeletePersonEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePersonConsumer> _logger;

        public DeletePersonConsumer(IMediator mediator, IMapper mapper, ILogger<DeletePersonConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task Consume(ConsumeContext<DeletePersonEvent> context)
        {
            try
            {
                var command = _mapper.Map<DeletePersonEventDto>(context.Message);
                var result = await _mediator.Send(command);

                _logger.LogInformation("DeletePersonEvent consumed successfully. Deleted Person Id: {newPersonId}", result);

            }
            catch (Exception ex)
            {
                _logger.LogError("DeletePersonEvent was consumed unsuccessfully. {message}", ex.Message);
                throw;
            }
        }

    }
}
