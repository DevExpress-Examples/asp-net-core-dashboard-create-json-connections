function onBeforeRender(dashboardControl) {
    dashboardControl.registerExtension(new DevExpress.Dashboard.DashboardPanelExtension(dashboardControl, { dashboardThumbnail: "./DashboardThumbnail/{0}.png" }));
}
function onCustomizeDataSourceWizard(args) {
    args.wizard.events.addHandler("beforePageInitialize", (args) => {
        args.state.dataSourceType = DevExpress.Dashboard.Designer.ToDataSourceTypeNumber('Json');
    })
    args.wizard.events.addHandler("afterPageInitialize", (args) => {
        args.wizard.pageFactory.unregisterMetadata(DevExpress.Dashboard.Designer.DataSourceWizardPageId.ChooseDataSourceTypePage);
        var defaultGetNextPageId = args.wizard.iterator.getNextPageId;
        args.wizard.iterator.getNextPageId = function (pageId) {
            if (!pageId) {
                return DevExpress.Analytics.Wizard.JsonDataSourceWizardPageId.ChooseConnectionPage;
            } else {
                return defaultGetNextPageId.apply(this, pageId);
            }
        }
    })
}