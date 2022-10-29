using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestPath;
});
builder.Host.ConfigureLogging(logging =>
{
    logging.SetMinimumLevel(LogLevel.Trace);
});
builder.Services.AddWebOptimizer(pipeline =>
{
    Console.WriteLine("Adding bundle for scss");
    pipeline.AddScssBundle("/css/app.css", "./app.scss");
    pipeline.CompileScssFiles();
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
    options.ListenAnyIP(8080);
    options.ListenAnyIP(8443);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var f = app.Environment.WebRootFileProvider;
app.Logger.LogWarning("Sample");
app.Logger.LogWarning(f.GetType().Name);

app.UseHttpsRedirection();
app.UseHttpLogging();

app.UseBlazorFrameworkFiles();
app.UseWebOptimizer();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
