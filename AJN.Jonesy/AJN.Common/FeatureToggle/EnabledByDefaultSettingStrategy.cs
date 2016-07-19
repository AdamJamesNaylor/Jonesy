
namespace AJN.Common.FeatureToggle {
    public class EnabledByDefaultSettingStrategy
        : IDefaultSettingStrategy {

        public bool GetDefaultSetting() {
            return true;
        }
    }
}