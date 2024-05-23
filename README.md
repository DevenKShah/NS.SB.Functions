# NS.SB.Functions


Installers throws errors the first time, but creates the subscription anyway :slightly_smiling_face: So second time onwards it works fine.

Message conventions works as well. Alternatively following interfaces can be used:

- `IMessage` is used for Request/Reply pattern. 
- `ICommand` is used for sending message to a particular queue. 
- `IEvent` is used for Pub/Sub pattern. 

When sending messages we have to specify serializer and  it say use Newtonsoft for json, but when handling you do not have to specify serliaizer and it by default uses System.Text.Json and it still works fine funnily.

The messages have to be in the same namespace, so copies in different assmeblies are fine as long as the namespace matches.

You can use mix of convention and interface as well, i.e publishers can use conventions, but subscribers can use interfaces.

In console, add `appsettings.json` as following
```json
{
  "AzureWebJobsServiceBus": "<Service bus connections string>"
}
```
