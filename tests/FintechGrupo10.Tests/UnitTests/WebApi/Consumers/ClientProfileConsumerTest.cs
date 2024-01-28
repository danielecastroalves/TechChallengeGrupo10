using System.Text;
using FintechGrupo10.Application.Common.Configurations;
using FintechGrupo10.Application.Features.ClientProfile.SetClientProfile;
using FintechGrupo10.WebApi.Consumers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.WebApi.Consumers
{
    public class ClientProfileConsumerTest
    {
        //[Fact]
        //public async Task ExecuteAsync_WithValidClientProfileEvent_CallsMediatorAndAcks()
        //{
        //    // Arrange
        //    var rabbitConnection = Substitute.For<IConnection>();
        //    var serviceProvider = Substitute.For<IServiceProvider>();
        //    var mediator = Substitute.For<IMediator>();
        //    var options = Substitute.For<IOptions<RabbitMqConfig>>();

        //    var clientProfileEvent = new SetClientProfileEvent();
        //    var serializedEvent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientProfileEvent));

        //    options.Value.Returns(new RabbitMqConfig { ClientProfileQueue = "testQueue" });
        //    serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMediator>().Returns(mediator);

        //    var channel = Substitute.For<IModel>();
        //    rabbitConnection.CreateModel().Returns(channel);

        //    var consumer = new ClientProfileConsumer(rabbitConnection, serviceProvider, options);

        //    // Act
        //    await consumer.StartAsync(CancellationToken.None);

        //    // Simulate receiving a message
        //    var eventArgs = new BasicDeliverEventArgs
        //    {
        //        Body = serializedEvent,
        //        DeliveryTag = 1
        //    };

        //    // Manually call the ExecuteAsync method to simulate message processing
        //    await consumer.ExecuteAsync(CancellationToken.None, true);

        //    // Allow time for the message to be processed
        //    await Task.Delay(100);


        //    // Assert
        //    await mediator.Received(1).Send(Arg.Is<SetClientProfileEvent>(e =>
        //        e.ClientId == clientProfileEvent.ClientId));

        //    channel.Received(1).BasicAck(Arg.Any<ulong>(), Arg.Any<bool>());

        
    }
}
