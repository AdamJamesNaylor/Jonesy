namespace AJN.Common.FeatureToggle {

    public class ReleaseConfigurationValueProvider
        : IFeatureSettingValueProvider {

        public bool GetSetting(IFeature feature, IDefaultSettingStrategy defaultStrategy) {
#if DEBUG
            return false;
#else
            return true;
#endif
        }
    }
}