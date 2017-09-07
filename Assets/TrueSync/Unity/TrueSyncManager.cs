namespace TrueSync {
    /**
     * @brief Manages creation of player prefabs and lockstep execution.
     **/
    public class TrueSyncManager{

        private const string serverSettingsAssetFile = "TrueSyncGlobalConfig";

        public static TrueSyncConfig _TrueSyncGlobalConfig;

        public static TrueSyncConfig TrueSyncGlobalConfig {
            get {
                if (_TrueSyncGlobalConfig == null) {
                    _TrueSyncGlobalConfig = (TrueSyncConfig) UnityEngine.Resources.Load(serverSettingsAssetFile, typeof(TrueSyncConfig));
                }

                return _TrueSyncGlobalConfig;
            }
        }

        public static void Init() {
            TrueSyncConfig currentConfig = TrueSyncGlobalConfig;

            TSRandom.Init();

            if (currentConfig.physics2DEnabled || currentConfig.physics3DEnabled) {
                PhysicsManager.New(currentConfig);
                PhysicsManager.instance.Init();
            }
        }
    }
}