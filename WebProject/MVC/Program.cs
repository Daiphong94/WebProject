using Data.Interface;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectString")));

builder.Services.AddScoped<AdminInterface, AdminRepository>();
builder.Services.AddScoped<FacultyInterface, FacultyRepository>();
builder.Services.AddScoped<StudentInterface, StudentRepisitory>();

builder.Services.AddScoped<FAQInterface, FAQRepository>();
builder.Services.AddScoped<EventInterface, EventRepository>();
builder.Services.AddScoped<AnswerInterface, AnswerRepository>();
builder.Services.AddScoped<CompetitionInterface, CompetitionRepository>();
builder.Services.AddScoped<QuestionInterface, QuestionRepository>();
builder.Services.AddScoped<ExamInterface, ExamRepository>();
builder.Services.AddScoped<SCInterface, SCRepository>();
builder.Services.AddScoped<UserInterface, UserRepository>();
builder.Services.AddScoped<RegistrationInterface, RegistrationRepository>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = false;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(option =>
{
    option.LoginPath = "/User/Login";
    option.AccessDeniedPath = "/Home/Index";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Faculty", policy => policy.RequireRole("Faculty"));
    options.AddPolicy("AdminFaculty", policy => policy.RequireRole("Admin", "Faculty"));
    options.AddPolicy("Student", policy => policy.RequireRole("Student"));
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
