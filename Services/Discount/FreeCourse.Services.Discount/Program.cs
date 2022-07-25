using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;
// Add services to the container.


#region Authentication
//var requireAuthorizePolicy_test = new AuthorizationPolicyBuilder().RequireClaim("scope", "discount_read");
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = Configuration["IdentityServerURL"];
    options.Audience = "resource_discount";
    options.RequireHttpsMetadata = false;
});
#endregion
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
}); 

#region DI
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
#endregion
#region Http
builder.Services.AddHttpContextAccessor();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
