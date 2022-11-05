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
    //pipeline.AddScssBundle("/css/app.css", "./app.scss");
    pipeline.CompileScssFiles();
});

var url = "https://mnmobahtigndccqejpgq.supabase.co";
var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1ubW9iYWh0aWduZGNjcWVqcGdxIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NjY5MTkwODAsImV4cCI6MTk4MjQ5NTA4MH0.ajo5lDIIwIyd2TpI-z4SISVOq9HWBr2uDfLNk5gWMN0";
builder.Services.AddSingleton<Supabase.Client>((serviceProvider) =>
{
    return Supabase.Client.InitializeAsync(url, key, new Supabase.SupabaseOptions { }).Result;
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
