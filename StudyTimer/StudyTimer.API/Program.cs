using Microsoft.AspNetCore.Identity;
using StudyTimer.Domain.Identity;
using StudyTimer.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddSession();


builder.Services.AddDbContext<StudyTimerDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StudyTimerDbContext>();

builder.Services.AddIdentity<User, Role>(options =>
    {
        // User Password Options
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        // User Username and Email Options
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@$";
        options.User.RequireUniqueEmail = true;

    }).AddEntityFrameworkStores<StudyTimerDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

//builder.Services.Configure<SecurityStampValidatorOptions>(options =>
//{
//    options.ValidationInterval = TimeSpan.FromMinutes(30);
//});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = new PathString("/Auth/Login");
//    options.LogoutPath = new PathString("/Auth/Logout");
//    options.Cookie = new CookieBuilder
//    {
//        Name = "StudyTimer",
//        HttpOnly = true,
//        SameSite = SameSiteMode.Strict,
//        SecurePolicy = CookieSecurePolicy.SameAsRequest // Always
//    };
//    options.SlidingExpiration = true;
//    options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
//    options.AccessDeniedPath = new PathString("/Auth/AccessDenied");
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseSession();


app.MapControllers();

app.Run();
