using StudentTracking.Business.Interfaces;
using StudentTracking.Business.Services;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.EntityFramework.Repositories;
using StudentTracking.Data.EntityFramework;
using StudentTracking.Data.EntityFramework.UnitOfWork;
using StudentTracking.Business.Configuraions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
    builder.Services.AddScoped<IGameService, GameService>();
    builder.Services.AddScoped<IRegisterService, RegisterService>();
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<ICheckWordService, CheckWordService>();
    builder.Services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
    builder.Services.AddScoped<IGameRepository, GameRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddDbContext<StudentTrackingContext>();
    builder.Services.AddSession();



    var emailConfig = builder.Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
    builder.Services.AddSingleton(emailConfig);

    var app = builder.Build();
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSession();

    app.UseRouting();
    app.UseStatusCodePagesWithReExecute("/Customer/ErrorPage", "?code=");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Customer}/{action=GameList}");

    app.Run();

}
catch (Exception ex)
{

    throw;
}

