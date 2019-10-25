<!-- default file list -->
*Files to look at*:
* [Startup.cs](./CS/AspNetCoreDashboard2.2/Startup.cs)
* [Index.cshtml](./CS/AspNetCoreDashboard2.2/Pages/Index.cshtml)
<!-- default file list end -->

# Web Dashboard - How to create new JSON data sources at runtime

You can provide end users with the capability to create a new data connection for a [JSON Data Source](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardJsonDataSource) at runtime.

To enable the capability for end users, set the **canCreateNewJsonDataSource** property to **true**:

Platform | API
-----|------
 HTML JavaScript | [DataSourceWizardExtensionOptions.canCreateNewJsonDataSource](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.Designer.DataSourceWizardExtensionOptions#js_DevExpress_Dashboard_Designer_DataSourceWizardExtensionOptions_canCreateNewJsonDataSource)
 ASP.NET Core | [DataSourceWizardOptionBuilder.CanCreateNewJsonDataSource(Boolean)](https://docs.devexpress.com/Dashboard/DevExpress.DashboardAspNetCore.DataSourceWizardOptionBuilder.CanCreateNewJsonDataSource(System.Boolean))
 MVC | [DashboardExtensionSettings.CanCreateNewJsonDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.Mvc.DashboardExtensionSettings.CanCreateNewJsonDataSource) 
 Web Forms | [ASPxDashboard.CanCreateNewJsonDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.ASPxDashboard.CanCreateNewJsonDataSource) 

After that, the "Choose Connection (JSON)" page allows end users to create new data connections:

![Can create new data connections](img.png)

On the server side, implement the [IDataSourceWizardConnectionStringsStorage](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.IDataSourceWizardConnectionStringsStorage) interface to provide an end user with the capability to save the created JSON data connections. Use the created class instance as the [ASPxDashboard.SetConnectionStringsProvider](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.ASPxDashboard.SetConnectionStringsProvider(DevExpress.DashboardWeb.IDataSourceWizardConnectionStringsStorage)) / [DashboardConfigurator.SetConnectionStringsProvider](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.SetConnectionStringsProvider(DevExpress.DashboardWeb.IDataSourceWizardConnectionStringsStorage)) method's parameter.

## See Also
- [Dashboard Data Source Wizard](https://docs.devexpress.com/Dashboard/117680/)
- [Customize the Dashboard Data Source Wizard](https://docs.devexpress.com/Dashboard/401330/)
