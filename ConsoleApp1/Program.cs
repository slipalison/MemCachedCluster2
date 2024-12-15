using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Microsoft.Extensions.Logging;


var l = LoggerFactory.Create(x =>
{
    x.AddConsole();
});

IMemcachedClientConfiguration config = new MemcachedClientConfiguration( l, new MemcachedClientOptions()
{
    Servers = new List<Server>()
    {
        new Server()
        {
            Address = "localhost",
            Port = 5000
        },
        // new Server()
        // {
        //     Address = "localhost",
        //     Port = 11212
        // },
        // new Server()
        // {
        //     Address = "localhost",
        //     Port = 11213
        // },
        // new Server()
        // {
        //     Address = "localhost",
        //     Port = 11214
        // }
        
    }, Protocol = MemcachedProtocol.Text
});


using var client = new MemcachedClient(l,config);



client.Store(Enyim.Caching.Memcached.StoreMode.Set, "key1", "value1");

// Recuperar valor
var value = client.Get<string>("key1");
Console.WriteLine("Valor recuperado: " + value);

// Excluir valor
client.Remove("key1");

Console.WriteLine("Hello, World!");