using API.Extensions;
using API.Middlewares;
using Aplication.Helpers.MappingProfiles;
using Aplication.Interfaces;
using Aplication.Posts;
using Aplication.Users;
using Doiman;
using FluentValidation.AspNetCore;
using Infrastruture.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

//primeiro paramento é a clase do user onde estendemos o identityUser
//o segudo é do role, como nao temos add oque te ja esta na classe do identity



builder.Services.AddAplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddAutoMapper(typeof(MappingProfiles));//add o Servico de auto map

//usamos para definir servico de DB
builder.Services.AddDbContext<DataContext>(optionsAction =>
{
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

//So fazemos uma vez
builder.Services.AddMediatR(typeof(CreatePost.CreatePostCommand).Assembly);//configuracao do mediatr


// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(x=>
    x.RegisterValidatorsFromAssemblyContaining<CreateUser.CreateUserCommand>()
    );



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options=>
{
	options.AddPolicy("MyCors",builder=>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();
Load();
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MyCors");
app.MapControllers();
app.Run();


void Load()
{
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService <UserManager<User>>();
    LoadSeeds.SeedAsync(userManager);
}