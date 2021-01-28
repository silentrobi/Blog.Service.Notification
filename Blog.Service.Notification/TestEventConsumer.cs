using MassTransit;
using SharedLibrary;
using System.Threading.Tasks;

namespace Blog.Service.NotificationSerder.Api
{
    public class TestEventConsumer : IConsumer<TestMessage>
    {
        public async Task Consume(ConsumeContext<TestMessage> context)
        {
            var data = context.Message;

            return ;
        }
    }
}
