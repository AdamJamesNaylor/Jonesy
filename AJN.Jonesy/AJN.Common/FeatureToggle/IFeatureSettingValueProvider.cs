namespace AJN.Common.FeatureToggle {
    public interface IFeatureSettingValueProvider {
        bool GetSetting(IFeature feature, IDefaultSettingStrategy defaultStrategy);
    }
}