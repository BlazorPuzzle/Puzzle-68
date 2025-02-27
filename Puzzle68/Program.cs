using Microsoft.AspNetCore.HttpOverrides;
using Puzzle68.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
		.AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

var headerOptions = new ForwardedHeadersOptions
{
	ForwardedHeaders = ForwardedHeaders.All
};
headerOptions.KnownNetworks.Clear();
headerOptions.KnownProxies.Clear();
app.UseForwardedHeaders(headerOptions);

app.UseAntiforgery();
app.UseHttpsRedirection();

app.MapStaticAssets();
app.MapRazorComponents<App>()
		.AddInteractiveServerRenderMode();

app.Run();
