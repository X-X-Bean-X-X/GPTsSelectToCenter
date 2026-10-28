using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Input;
using Game.Modding;
using Game.Settings;
using Game.UI;
using System.Collections.Generic;

namespace SelectToCenter
{
    [FileLocation("SelectToCenter")]
    [SettingsUIGroupOrder(kGeneralGroup, kKeybindingGroup)]
    [SettingsUIShowGroupName(kGeneralGroup, kKeybindingGroup)]
    [SettingsUIKeyboardAction(
        Mod.kArmActionName,
        ActionType.Button,
        usages: new string[] { Usages.kMenuUsage, "SelectToCenterArmNextSelection" },
        interactions: new string[] { "UIButton" })]
    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kGeneralGroup = "General";
        public const string kKeybindingGroup = "Key Binding";

        public const float kHoldSeconds = 0.444f;

        public Setting(IMod mod) : base(mod)
        {
            SetDefaults();
        }

        [SettingsUISection(kSection, kGeneralGroup)]
        public bool Enabled { get; set; }

        [SettingsUISection(kSection, kGeneralGroup)]
        public bool RequireArmForNextSelection { get; set; }

        [SettingsUISection(kSection, kGeneralGroup)]
        public bool KeepFocusOnSelect { get; set; }

        [SettingsUIKeyboardBinding(BindingKeyboard.Z, Mod.kArmActionName, shift: true, alt: true)]
        [SettingsUISection(kSection, kKeybindingGroup)]
        public ProxyBinding ArmNextSelectionBinding { get; set; }

        [SettingsUISection(kSection, kKeybindingGroup)]
        public bool ResetBindings
        {
            set { ResetKeyBindings(); }
        }

        public override void SetDefaults()
        {
            Enabled = true;
            RequireArmForNextSelection = false;
            KeepFocusOnSelect = false;
        }
    }

    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Select to Center" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Main" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kGeneralGroup), "General" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kKeybindingGroup), "Key Binding" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Enabled)), "Enabled" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Enabled)), "Turn the mod on or off." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RequireArmForNextSelection)), "Only center when SHIFT + ALT + Z is pressed before selecting an object" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RequireArmForNextSelection)), "When off, the mod centers on every selected object" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.KeepFocusOnSelect)), "Keep object in focus when selecting (Will not work w/game controllers)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.KeepFocusOnSelect)), "Keep object in focus when selecting (Will not work w/game controllers)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ArmNextSelectionBinding)), "Arm next selection hotkey" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ArmNextSelectionBinding)), "Press SHIFT + ALT + Z, then select an object to center it" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetBindings)), "Reset key bindings" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetBindings)), "Reset all Select to Center key bindings." },

                { m_Setting.GetBindingKeyLocaleID(Mod.kArmActionName), "Arm next selection key" },
                { m_Setting.GetBindingMapLocaleID(), "Select to Center key bindings" },
            };
        }

        public void Unload()
        {
        }
    }
}