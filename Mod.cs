using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Input;
using Game.Modding;
using Game.SceneFlow;

namespace SelectToCenter
{
    public class Mod : IMod
    {
        public static ILog log = LogManager
            .GetLogger("SelectToCenter.Mod")
            .SetShowsErrorsInUI(false);

        public const string kArmActionName = "ArmNextSelection";

        public static Setting Settings { get; private set; }
        public static ProxyAction ArmAction { get; private set; }

        private static bool s_NextSelectionArmed;
        private static bool s_ClearSelectionRequested;

        private Setting m_Setting;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            m_Setting = new Setting(this);
            Settings = m_Setting;

            m_Setting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
            m_Setting.RegisterKeyBindings();

            ArmAction = m_Setting.GetAction(kArmActionName);
            if (ArmAction != null)
            {
                ArmAction.shouldBeEnabled = true;
                ArmAction.onInteraction += (_, __) =>
                {
                    if (Settings != null && Settings.Enabled && ArmAction.ReadValue<float>() > 0.5f)
                    {
                        s_NextSelectionArmed = true;
                        s_ClearSelectionRequested = true;
                    }
                };
            }

            AssetDatabase.global.LoadSettings("SelectToCenter", m_Setting, new Setting(this));

            updateSystem.UpdateAt<AutoCenterSelectionSystem>(SystemUpdatePhase.UIUpdate);
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));

            s_NextSelectionArmed = false;
            s_ClearSelectionRequested = false;
            ArmAction = null;

            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }

            Settings = null;
        }

        public static bool ConsumeNextSelectionArm()
        {
            if (!s_NextSelectionArmed)
                return false;

            s_NextSelectionArmed = false;
            return true;
        }

        public static void ClearNextSelectionArm()
        {
            s_NextSelectionArmed = false;
        }

        public static bool ConsumeClearSelectionRequest()
        {
            if (!s_ClearSelectionRequested)
                return false;

            s_ClearSelectionRequested = false;
            return true;
        }
    }
}