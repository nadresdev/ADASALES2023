using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sales.WEB;
using Sales.WEB.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    // BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
    BaseAddress = new Uri("https://localhost:7274/")
});
builder.Services.AddScoped<IRepository, Repository>();//inyecta  creando un objeto nuevo 
builder.Services.AddSweetAlert2();
await builder.Build().RunAsync();
