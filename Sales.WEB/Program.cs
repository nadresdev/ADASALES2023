using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sales.WEB;
using Sales.WEB.Auth;
using Sales.WEB.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient  //se marca singleton para enviar el token una vez
{
    // BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
    BaseAddress = new Uri("https://localhost:7274/")
});
builder.Services.AddScoped<IRepository, Repository>();//inyecta  creando un objeto nuevo 
builder.Services.AddSweetAlert2();
builder.Services.AddAuthorizationCore();
/*

builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderTest>();

 Para agregar el  prpoveedor de autenticacion real*/

builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());


await builder.Build().RunAsync();
