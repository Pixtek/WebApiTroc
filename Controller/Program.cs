
using Application.UseCases.Users;
using Application.UseCases.Users.Dto;
using Domain;
using Infrastructure.EF;
using Infrastructure.EF.Article;
using Infrastructure.EF.Commentary;
using Infrastructure.EF.User;
using WebApiTroc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
builder.Services.AddScoped<IArticle, ArticleRepository>();
builder.Services.AddScoped<IUsers, UsersRepository>();
builder.Services.AddScoped<TrocContextProvider>();

//users
builder.Services.AddScoped<UseCaseFetchById>();
builder.Services.AddScoped<UseCaseCreateUser>();
builder.Services.AddScoped<UseCaseFetchByPseudo>();

//Commentary
builder.Services.AddScoped<ICommentary, CommentaryRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Dev");


app.UseAuthorization();

app.MapControllers();

app.Run();