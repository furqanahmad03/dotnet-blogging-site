using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using X.Data;
using X.Hubs;
using X.Models;
using X.Models.Interfaces;
using X.Models.Repositories;
using X.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 3))));

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Redirect(options.LoginPath);
        return Task.CompletedTask;
    };
});

builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, NoOpEmailSender>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddSignalR();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("UserType", "Admin"));

    options.AddPolicy("UserOnly", policy =>
        policy.RequireClaim("UserType", "User"));
});

builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

var app = builder.Build();

app.MapHub<BlogHub>("/blogHub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "blog-id",
    pattern: "blogs/{id:int}",
    defaults: new { controller = "Blogs", action = "Blog" });

app.MapControllerRoute(
    name: "post-id",
    pattern: "community/post-{id:int}",
    defaults: new { controller = "Community", action = "Post" });

app.MapRazorPages();


// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapRazorPages();
// });


app.Run();


