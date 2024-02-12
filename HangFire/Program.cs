using Hangfire;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();  
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(configuration: cnf =>
{
    cnf.UseSqlServerStorage(nameOrConnectionString: builder.Configuration["ConnectionStrings:HangFireConnection"]);
    cnf.UseSimpleAssemblyNameTypeSerializer();
    cnf.UseRecommendedSerializerSettings();
});

builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUi();
}
app.UseHttpsRedirection();
app.MapControllers();
//app.MapGet("/", () => "Hello World!");
app.UseHangfireDashboard(pathMatch: "/dashboard");
app.Run();
