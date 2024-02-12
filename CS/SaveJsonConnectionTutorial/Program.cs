using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardWeb;
using Microsoft.Extensions.FileProviders;
using SaveJsonConnectionTutorial;
using DevExpress.DashboardCommon;
using System;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;

var builder = WebApplication.CreateBuilder(args);

IFileProvider? fileProvider = builder.Environment.ContentRootFileProvider;
IConfiguration? configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDevExpressControls();
builder.Services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) => {

    DashboardConfigurator configurator = new DashboardConfigurator();
    // Use the ConnectionStringProvider class to allow end users to use only connections created at runtime.
    //configurator.SetConnectionStringsProvider(new ConnectionStringProvider());

    // Use the ConnectionStringProviderEx class to allow end users to use connections created at runtime in addition to predefined connection strings.
    configurator.SetConnectionStringsProvider(new ConnectionStringsProviderEx(new DashboardConnectionStringsProvider(configuration)));

    DashboardFileStorage dashboardFileStorage = new DashboardFileStorage(fileProvider.GetFileInfo("Data/Dashboards").PhysicalPath);
    configurator.SetDashboardStorage(dashboardFileStorage);

    DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();

    // Registers an SQL data source.
    DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "nwindConnectionString");
    sqlDataSource.DataProcessingMode = DataProcessingMode.Client;
    SelectQuery query = SelectQueryFluentBuilder
        .AddTable("Categories")
        .Join("Products", "CategoryID")
        .SelectAllColumns()
        .Build("Products_Categories");
    sqlDataSource.Queries.Add(query);
    dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());

    configurator.SetDataSourceStorage(dataSourceStorage);

    return configurator;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDevExpressControls();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.MapDashboardRoute("dashboardControl", "DefaultDashboard");

app.Run();