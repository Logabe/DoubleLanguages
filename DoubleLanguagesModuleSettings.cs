using System;
using Celeste.Mod.UI;
using Monocle;

namespace Celeste.Mod.DoubleLanguages {
    [SettingName("modoptions_doublelanguages_title")]
    public class DoubleLanguagesModuleSettings : EverestModuleSettings {
        
        public bool Enabled { get; set; } = false;
        
        [SettingSubText("modoptions_secondlanguage_description")]  
        [SettingNeedsRelaunch]
        [SettingInGame(false)]
        public string SecondLanguage { get; set; } = "japanese";
    }
}
