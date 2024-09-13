using FluentValidation;
using FluentValidation.AspNetCore;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Kashmir.Captain.Server.Services;
using System.Net.Mail;
using Kashmir.Captain.Server.Config;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Kashmir.Captain.Server.Application.Mapping;
using Kashmir.Captain.Server.Common.Kashmir.Captain.Server.Common;
using Kashmir.Captain.Server.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Configure database context
builder.Services.AddDbContext<KcDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("KcDbContext")
	?? throw new InvalidOperationException("Connection string 'KcDbContext' not found.")));

// Configure Identity
builder.Services.AddIdentity<User, Role>(options =>
{
	// Configure password settings
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 6;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<KcDbContext>()
.AddDefaultTokenProviders();

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Add Role Policy
builder.Services.AddAuthorization(options =>
	{
		options.AddPolicy(nameof(RoleType.User), policy => policy.RequireRole(nameof(RoleType.SuperAdmin), nameof(RoleType.Admin), nameof(RoleType.User)));
		options.AddPolicy(nameof(RoleType.Admin), policy => policy.RequireRole(nameof(RoleType.SuperAdmin), nameof(RoleType.Admin)));
		options.AddPolicy(nameof(RoleType.SuperAdmin), policy => policy.RequireRole(nameof(RoleType.SuperAdmin)));

	});

// Configure authentication
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
	};
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
	// Configure cookie options if needed
	options.Cookie.Name = "KcCookies";
});

// Add authorization policies if needed
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp",
		builder =>
		{
			builder.WithOrigins("*") // Replace with your React app's URL
				   .AllowAnyHeader()
				   .AllowAnyMethod();
		});
});

builder.Services.AddControllers();
// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IUrlHelper>(factory =>
{
	var actionContext = factory.GetService<IActionContextAccessor>()?.ActionContext ?? new ActionContext();
	return new UrlHelper(actionContext);
});

// builder.Services.AddHttpClient<ApiClient>(client =>
//     {
//         client.BaseAddress = new Uri("https://localhost:5092/api/");
//         client.DefaultRequestHeaders.Add("Accept", "application/json");
// });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUrlHelperService, UrlHelperService>();

// Add services to the container.
builder.Services.AddFluentValidationAutoValidation()
				.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Configure EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Register SmtpClient as a Singleton service
builder.Services.AddSingleton<SmtpClient>(serviceProvider =>
{
	var emailSettings = serviceProvider.GetRequiredService<IOptions<EmailSettings>>().Value;

	return new SmtpClient(emailSettings.SmtpServer)
	{
		Port = emailSettings.SmtpPort,
		Credentials = new NetworkCredential(emailSettings.SenderEmail, emailSettings.SenderPassword),
		EnableSsl = emailSettings.EnableSsl,
	};
});
// Add Services
builder.Services.AddScoped<IEmailService, EmailService>();

// Register IHttpContextAccessor and CurrentUserService
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
	await InitializeRoles(roleManager);
	await CreateSuperAdminAssignRole(userManager);
}

// Middleware pipeline
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

await app.RunAsync();

async Task InitializeRoles(RoleManager<Role> roleManager)
{
	foreach (var roleName in GlobalConstants.roleNames)
	{
		if (!await roleManager.RoleExistsAsync(roleName))
		{
			var role = new Role(roleName);
			await roleManager.CreateAsync(role);
		}
	}
}

async Task CreateSuperAdminAssignRole(UserManager<User> userManager)
{
	var superAdmin = nameof(RoleType.SuperAdmin);
	var email = "test@test.com";

	var user = await userManager.FindByEmailAsync(email);

	if (user == null)
	{
		user = new User
		{
			UserName = email,
			Email = email,
			FirstName = "Bilal",
			LastName = "Sofi",
			PhoneNumber = "7006698882"
		};
		await userManager.CreateAsync(user, "Password@123");
	}

	if (!await userManager.IsInRoleAsync(user, superAdmin))
	{
		await userManager.AddToRoleAsync(user, superAdmin);
	}
}