using eBookStore.Repository.AutoMapper;
using eBookStore.Repository.Context;
using eBookStore.Repository.Entity;
using eBookStore.Repository.Model.ResponseModel;
using eBookStore.Service.Service.Implement;
using eBookStore.Service.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
#region Authen
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var key = builder.Configuration.GetValue<string>("ApiSetting:Secret");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion


//add Db
builder.Services.AddDbContext<EBookStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#region OData
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<BookResponseModel>("Books");
modelBuilder.EntitySet<AuthorResponseModel>("Authors");
modelBuilder.EntitySet<PublisherResponseModel>("Publishers");
modelBuilder.EntitySet<RoleResponseModel>("Roles");
modelBuilder.EntitySet<UserResponseModel>("Users");
//modelBuilder.EntitySet<LoginResponseModel>("AuthenUsers");
builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100)
    .AddRouteComponents(routePrefix: "odata", model: modelBuilder.GetEdmModel());
});
#endregion
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

#region DI_REPO
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenService, AuthenService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseODataBatching();

app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
