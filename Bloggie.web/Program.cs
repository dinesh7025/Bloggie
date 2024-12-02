using Bloggie.web.Data;
using Bloggie.web.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server connection string
builder.Services.AddDbContext<BloggieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add repository Services
builder.Services.AddScoped<IBlogPostrepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepositoryCloudinary>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
