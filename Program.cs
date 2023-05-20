using ShareNowBackend.Services;
using ShareNowBackend.Services.EventServices;
using ShareNowBackend.Services.InvitationServices;
using ShareNowBackend.Services.RequestServices;
using ShareNowBackend.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<IInvitationService, InvitationService>();
builder.Services.AddSingleton<IRequestService, RequestService>();
builder.Services.AddSingleton<IService, Service>();


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

app.Run();

