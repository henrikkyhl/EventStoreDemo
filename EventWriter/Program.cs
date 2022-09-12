using System.Text.Json;
using EventStore.Client;

// Connect to EventStoreDB.
string gRpcConnectionString = "esdb://localhost:2113?tls=false";
var settings = EventStoreClientSettings.Create(gRpcConnectionString);
var client = new EventStoreClient(settings);

Console.WriteLine("EventWriter connected to EventStoreDB");

int count = 1;

while (true)
{
    // Create the object that should be written as an event. You can use
    // any type that is serializable.
    var evt = new DemoEvent
    {
        Timestamp = DateTime.Now.ToString(),
        Data = "Event " + count++
    };

    // Create an event.
    // Members of EventData:
    // EventId (should be a UUID), Type and Data (should be a byte array)
    var eventData = new EventData(
        Uuid.NewUuid(),
        "DemoEvent",
        JsonSerializer.SerializeToUtf8Bytes(evt)
    );

    // Write the event to a stream.
    // Parameters: streamName, expectedState and eventData. The stream is
    // created automatically, if it doesn't already exist.
    client.AppendToStreamAsync(
        "demoStream",
        StreamState.Any,
        new[] { eventData }
    ).Wait();

    Console.WriteLine("New event written");

    // Wait a few seconds
    Thread.Sleep(5000);
}


class DemoEvent
{
    public string Timestamp { get; set; }
    public string Data { get; set; }
}
