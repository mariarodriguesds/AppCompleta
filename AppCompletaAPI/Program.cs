using AppCompletaAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls(
    "http://0.0.0.0:5257",
    "https://0.0.0.0:7179");

builder.Services.AddSingleton<DatabaseService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();