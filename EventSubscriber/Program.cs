using System.Text;
using EventStore.Client;

// Connect to EventStoreDB.
string gRpcConnectionString = "esdb://localhost:2113?tls=false";
var settings = EventStoreClientSettings.Create(gRpcConnectionString);
var client = new EventStoreClient(settings);

Console.WriteLine("EventSubscriber connected to EventStoreDB");

// Listen to events.
// Parameters:
// name of the stream on which we subscribe to events.
// the action to call when an event arrives (this is an asynchronous lambda expression).
// The event handler, "HandleEvent", will be called for every event in the stream.
// If events already exist, the handler will be called for each event one by one until it reaches
// the end of the stream. From there, the server will notify the handler whenever a new event appears.
client.SubscribeToStreamAsync("demoStream", FromStream.Start,
    async (subscription, evnt, cancellationToken) =>
    {
        await HandleEvent(evnt);
    }).Wait();

Console.WriteLine("waiting for events. ");
Console.ReadLine();


Task HandleEvent(ResolvedEvent evnt)
{
    Console.WriteLine(evnt.OriginalEventNumber + ": " +
        Encoding.UTF8.GetString(evnt.Event.Data.ToArray()));
    return Task.CompletedTask;
}
