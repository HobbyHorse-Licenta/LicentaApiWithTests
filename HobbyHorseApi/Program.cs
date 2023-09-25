using HobbyHorseApi.Entities.DBContext;
using HobbyHorseApi.Repositories.Implementations;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Implementations;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using HobbyHorseApi.JsonConverters;
using HobbyHorseApi.RabbitMQ;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using HobbyHorseApi.Authentication;
using FirebaseAdmin;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Google.Apis.Auth.OAuth2;
using Polly;

var corsPolicyName = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

builder.Services.AddCors(options =>
{

    options.AddPolicy(name: corsPolicyName,
                      builder =>
                      {
                          builder.AllowAnyOrigin().AllowAnyHeader().
                                             WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS");
                      });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (options) => { });

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new OutingConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.MaxDepth = 100;
    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    options.JsonSerializerOptions.IgnoreNullValues = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HobbyHorseContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"));
    //options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"));

    //if(String.Equals(Environment.GetEnvironmentVariable("USE_DATABASE"), "PostgreSQL") == true)
    //{
    //    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"));
    //}
    //else options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"));
});

//var firebaseCredential = GoogleCredential.FromFile("./appsettings.json");
builder.Services.AddSingleton(FirebaseApp.Create());

builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ISkateProfileService, SkateProfileService>();
builder.Services.AddScoped<ISkateProfileRepository, SkateProfileRepository>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ITrailService, TrailService>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<SenderAndReceiver>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HobbyHorseContext>();
    context.Database.Migrate();
}

app.UseCors(corsPolicyName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseStaticFiles();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //was not here from the start
app.UseAuthorization();

app.MapControllers();

app.Run();
