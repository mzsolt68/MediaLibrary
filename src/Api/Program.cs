var builder = WebApplication.CreateBuilder(args);

// Pass the configuration to the Startup constructor
var startup = new Api.Startup(builder.Configuration);

// Call ConfigureServices to register services
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Call Configure to set up the middleware pipeline
startup.Configure(app, app.Environment);

app.Run();
