using MediatR;
using NServiceBus;
using StructureMap;
using System;
using System.Threading.Tasks;

namespace SecurityMonitor.Dispatcher
{
    class Program
    {
        private const string HostName = "SecurityMonitor.Dispatcher";
        public static Container Container { get; private set; }

        public static IEndpointInstance Endpoint { get; private set; }
        static async Task Main(string[] args)
        {
            Container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                });


                cfg.For<IMediator>().Use<Mediator>();
                cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
            });

            var endpointConfiguration = new EndpointConfiguration(HostName);

            endpointConfiguration.UseContainer<StructureMapBuilder>(customizations => customizations.ExistingContainer(Container));
            endpointConfiguration.EnableUniformSession();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host=localhost");

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            Endpoint = await NServiceBus.Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await Endpoint.Stop()
                .ConfigureAwait(false);
        }
    }
}
