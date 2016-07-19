namespace AJN.Common.FeatureToggle {
    public interface IFeature {
        bool IsEnabled { get; }
    }
}