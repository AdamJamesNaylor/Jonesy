
namespace AJN.Jonesy.Website.Views {
    using System.Web.Mvc;
    using Features;

    public class LayoutViewBase
        : WebViewPage {

        public IAnalyticsFeature AnalyticsFeature { get; set; }

        public LayoutViewBase() {
            AnalyticsFeature = new AnalyticsFeature();
        }

        public override void Execute() { }
    }

}