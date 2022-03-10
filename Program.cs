using SignalR_Db_Listener.Database;
using SignalR_Db_Listener.Database.Entities;
using SignalR_Db_Listener.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
                         policy.AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials()
                         .SetIsOriginAllowed(origin => true)));
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DatabaseSubscription<Match>>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDatabasSubscription<DatabaseSubscription<Match>>("Matches");
app.UseRouting();
app.UseCors();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<HubExample>("/testHub");
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();


