var builder = WebApplication.CreateBuilder(args);

// Create an instance of the Startup class
var startup = new Api.Startup();

// Call ConfigureServices to register services
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Call Configure to set up the middleware pipeline
startup.Configure(app, app.Environment);

app.Run();
