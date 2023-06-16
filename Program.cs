using ShareNowBackend.Repositories;
using ShareNowBackend.Services;
using ShareNowBackend.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<EventService>();
builder.Services.AddSingleton<InvitationService>();
builder.Services.AddSingleton<RequestService>();
builder.Services.AddSingleton<Service>();
builder.Services.AddSingleton<Helpers>();

// Add repositories to the container.

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<EventRepository>();
builder.Services.AddSingleton<InvitationRepository>();
builder.Services.AddSingleton<RequestRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// Add mocks to db. Uncomment LINE 42 only if db is empty
var helpers = app.Services.GetRequiredService<Helpers>();
//helpers?.populateServicesWithMocks();

app.Run();

