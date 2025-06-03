using BuisnessLogicLayer.IServiceContracts;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //Apply the response type (all responses type will be a 'pplication/json' type)
    options.Filters.Add(new ProducesAttribute("application/json"));

    //Applyl the request type (the server will recevie the only 'application/json' type requests)
    options.Filters.Add(new ConsumesAttribute("application/json"));

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,"api.xml"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<ApplicationUser,ApplicationRole,ApplicationDbContext,Guid>>()
.AddRoleStore<RoleStore<ApplicationRole,ApplicationDbContext,Guid>>();    

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});

builder.Services.AddScoped<IStudentService,StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
