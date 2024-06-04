using Microsoft.AspNetCore.ResponseCompression;
using BlazorSignalRApp.Hubs;
using blazor.Components;
using DotNetEnv;
Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GroupManager>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddResponseCompression(opts =>
{
opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseResponseCompression();
app.MapHub<ChatHub>("/chathub");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();