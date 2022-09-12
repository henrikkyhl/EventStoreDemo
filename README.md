# EventStoreDemo
Demonstrates how to use the EventStore framework
The solution consists of three console applications:
1.	EventWriter: Writes an event to the EventStore database every 5 seconds.
2.	EventReader: Reads all the events in the EventStore database.
3.	EventSubscriber: Is notified every time an event is written to the EventStore database. Information about the event is shown on the screen.
To run the application, you must first get EventStoreDB. You can either download EventStoreDB binaries at https://eventstore.org/ and then install it on your computer, or you can run EventStoreDB as a Docker container. It is far easier to run it as a Docker container, and this setup is fine for development environments. To run EventStoreDB as a Docker container, do the following:
Pull the Docker image: docker pull eventstore/eventstore
Run the container: docker run --name esdb-node -it -p 2113:2113 eventstore/eventstore:latest --insecure
This command will run EventStoreDB as a container named "esdb-node", and it will listen on HTTP port 2113, which is the default HTTP port. The "--insecure" parameter means that HTTPS will not be used.
You can also run EventStoreDB as part of a multi-container application using docker-compose. To do that, you need to add EventStoreDB as a service in the docker compose file. The link below shows how to do that:
https://github.com/EventStore/EventStore/blob/master/samples/server/docker-compose.yaml
You need an EventStore client to communicate with EventStoreDB. Client libraries are available for several programming languages and two communication protocols: gRPC and TCP. Since there are plans to phase out TCP in the near future, we will use the gRPC client for this example.
We will use the .NET gRPC client, which is available as a NuGet package named "EventStore.Client.Grpc.Streams". Since we are running in insecure mode (i.e. without HTTPS), we also need to add the "Grpc.Net.Client" NuGet package.
Useful links for further information:
