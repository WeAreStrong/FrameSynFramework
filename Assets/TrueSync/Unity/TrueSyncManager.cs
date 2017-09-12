using UnityEngine;

namespace TrueSync
{
    /**
     * @brief Manages creation of player prefabs and lockstep execution.
     **/
    public class TrueSyncManager
    {
        private const string serverSettingsAssetFile = "TrueSyncGlobalConfig";

        public static TrueSyncConfig _TrueSyncGlobalConfig;

        public static TrueSyncConfig TrueSyncGlobalConfig
        {
            get
            {
                if (_TrueSyncGlobalConfig == null)
                {
                    _TrueSyncGlobalConfig = (TrueSyncConfig)Resources.Load(serverSettingsAssetFile, typeof(TrueSyncConfig));
                }

                return _TrueSyncGlobalConfig;
            }
        }

        public static TrueSyncConfig TrueSyncCustomConfig = null;

        /** 
         * @brief Returns the active {@link TrueSyncConfig} used by the {@link TrueSyncManager}.
         **/
        public static TrueSyncConfig Config
        {
            get
            {
                if (TrueSyncCustomConfig != null)
                {
                    return TrueSyncCustomConfig;
                }

                return TrueSyncGlobalConfig;
            }
        }

        public static void Init()
        {
            TrueSyncConfig currentConfig = Config;

            if (currentConfig.physics2DEnabled || currentConfig.physics3DEnabled)
            {
                PhysicsManager.New(currentConfig);
                PhysicsManager.instance.LockedTimeStep = currentConfig.lockedTimeStep;
                PhysicsManager.instance.Init();
            }
        }
        public static void CleanUp()
        {
            ResourcePool.CleanUpAll();
            StateTracker.CleanUp();
        }
    }
}