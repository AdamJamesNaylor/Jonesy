namespace AJN.Common.FeatureToggle {
    public class DisabledByDefaultSettingStrategy
        : IDefaultSettingStrategy {

        public bool GetDefaultSetting() {
            return false;
        }
    }
}