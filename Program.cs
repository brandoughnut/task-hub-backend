using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using task_hub_backend.Hubs;
using task_hub_backend.Services;
using task_hub_backend.Services.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<TaskService>();

var connectionString = builder.Configuration.GetConnectionString("MyBlogString");

builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));

builder.Services.AddCors(options => options.AddPolicy("BlogPolicy", 
builder => {
    builder.WithOrigins("http://localhost:3000", "http://localhost:5044", "http://localhost:5105", "https://task-hub-fullstack.vercel.app")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
}));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SharedDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("BlogPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/Chat");

app.Run();
