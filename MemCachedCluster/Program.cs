using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddEnyimMemcached();
builder.Services.AddEnyimMemcached(options =>
{
  options.Servers.Add(new Server
  {
    Address = "mcrouter",
    Port = 5000
  });
  options.Protocol = MemcachedProtocol.Text;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseAuthorization();

app.MapControllers();

app.Run();

/*
{
  "pools": {
    "A": {
      "servers": [
        "memcached-node-1:11211",
        "memcached-node-2:11211",
        "memcached-node-3:11211",
        "memcached-node-4:11211"
      ]
    }
  },
  "route": "PoolRoute|A"
}
 */