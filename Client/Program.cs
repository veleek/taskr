using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using taskr.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
var url = "https://mnmobahtigndccqejpgq.supabase.co";
var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1ubW9iYWh0aWduZGNjcWVqcGdxIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NjY5MTkwODAsImV4cCI6MTk4MjQ5NTA4MH0.ajo5lDIIwIyd2TpI-z4SISVOq9HWBr2uDfLNk5gWMN0";
builder.Services.AddScoped(sp => Supabase.Client.InitializeAsync(url, key).Result);

await builder.Build().RunAsync();
