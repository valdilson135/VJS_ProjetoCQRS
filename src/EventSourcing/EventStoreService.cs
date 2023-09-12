using Microsoft.Extensions.Configuration;
using EventStore.ClientAPI;

namespace EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;       

        public EventStoreService(IConfiguration configuration)
        {

            var connStr = configuration.GetConnectionString("EventStoreConnection");
            _connection = EventStoreConnection.Create(connStr,
                ConnectionSettings.Create().DisableServerCertificateValidation().KeepReconnecting(), "VjsMVC");
            _connection.ConnectAsync();
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;           
        }      
    }
}
