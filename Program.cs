using AlgorithmsWebAPI.Models;
using AlgorithmsWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IBubbleSortService, BubbleSortService>();
builder.Services.AddScoped<ISelectionSortService, SelectionSortService>();
builder.Services.AddScoped<IInsertionSortService, InsertionSortService>();
builder.Services.AddScoped<IDFSService, DFSService>();
builder.Services.AddScoped<IPageRankService, PageRankService>();
builder.Services.AddScoped<IKMeansService, KMeansService>();
builder.Services.AddScoped<IKNNService, KNNService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>();

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);


var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
