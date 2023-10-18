using InventoryManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AppDbContext>((opts) =>
{
    var dbContext = new AppDbContext();
    dbContext.Database.EnsureCreated();
    return dbContext;
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers(options => options.InputFormatters.Add(new DaprRawPayloadInputFormatter()))
                .AddDapr();
builder.Services.AddDaprClient();

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

app.UseCloudEvents();

app.MapSubscribeHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
