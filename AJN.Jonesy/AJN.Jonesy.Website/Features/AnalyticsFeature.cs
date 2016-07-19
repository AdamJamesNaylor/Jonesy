
namespace AJN.Jonesy.Website.Features
{
    using AJN.Common.FeatureToggle;

    public class AnalyticsFeature
        : BaseFeature, IAnalyticsFeature {

        public AnalyticsFeature()
            : base(new ReleaseConfigurationValueProvider()) {
            
        }
    }

    public interface IAnalyticsFeature 
        : IFeature {
    }
}