using log4net;
using log4net.Config;
using System.Reflection;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

// 🔽 STEP 1: Setup Logging
builder.Logging.ClearProviders(); // optional: remove default loggers
builder.Logging.AddConsole();     // optional: keep console logs
builder.Logging.AddDebug();       // optional: keep debug output

// 🔽 STEP 2: Add log4net config
var log4netConfig = new XmlDocument();
log4netConfig.Load(File.OpenRead("log4net.config"));
var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

builder.Logging.AddLog4Net("log4net.config"); // use this to register with Microsoft ILogger

// 🔽 STEP 3: Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔽 STEP 4: Default middleware setup
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// 🔽 STEP 5: Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
