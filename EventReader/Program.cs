using System.Text;
using EventStore.Client;

// Connect to EventStoreDB.
string gRpcConnectionString = "esdb://localhost:2113?tls=false";
var settings = EventStoreClientSettings.Create(gRpcConnectionString);
var client = new EventStoreClient(settings);

Console.WriteLine("EventReader connected to EventStoreDB");

// Read all the events from a stream.
// Parameters: direction, stream name and start position.
var events = client.ReadStreamAsync(
    Direction.Forwards,
    "demoStream",
    StreamPosition.Start
);

// Iterate over the events obtained from the stream, and show data from
// each event on the screen.
await foreach (var @event in events)
{
    Console.WriteLine(Encoding.UTF8.GetString(@event.Event.Data.ToArray()));
}
