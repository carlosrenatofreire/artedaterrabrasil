
#region Builder Configuration�

using LayeredArch.Data.Contexts;
using LayeredArch.Infra.Data;
using LayeredArch.Mvc.Configurations;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
 .SetBasePath(builder.Environment.ContentRootPath)
 .AddJsonFile("appsettings.json", true, true)
 .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
 .AddEnvironmentVariables();

#endregion

#region Configure Services�

builder.Services.AddHttpContextAccessor();
builder.Services.AddContextConfig(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMvcConfiguration(builder.Configuration);
builder.Services.ResolveDependenciesMvc(builder.Configuration);

#endregion

#region Configure Pipeline�

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseHsts();
}
else
{
    app.UseExceptionHandler("/error/500");              // Middleware to unknown errors
    app.UseStatusCodePagesWithRedirects("/error/{0}");  // Middleware to known errors | Politica de Seguran�a para ataques como SQL Injection
    app.UseHsts();                                      // Middleware | Politica de seguranca para ataques como (MitM)
}

//app.UseMiddleware<ExceptionMiddleware>();  // Middleware para capturar erros tipos 401, 403, 404 e 500
app.UseMvcConfiguration(app.Environment);
app.UseStaticFiles(); // NOTA: Este middleware permite que os ficheiros do wwwroot sejam acedidos pela url do browse.

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    await DbSeeder.SeedAsync(context);
}

app.Run();

#endregion
