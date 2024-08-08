using System;
using System.Collections;
using Celeste.Mod.DoubleLanguages.Hooks;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;


namespace Celeste.Mod.DoubleLanguages {
    public class DoubleLanguages : EverestModule {
        public static DoubleLanguages Instance { get; private set; }

        public override Type SettingsType => typeof(DoubleLanguagesModuleSettings);
        public static DoubleLanguagesModuleSettings Settings => (DoubleLanguagesModuleSettings) Instance._Settings;

        public static Language Language = null;
        public static PixelFont Font => Fonts.Get(Language.FontFace);

        public DoubleLanguages() {
            Instance = this;
#if DEBUG
            // debug builds use verbose logging
            Logger.SetLogLevel(nameof(DoubleLanguages), LogLevel.Verbose);
#else
            // release builds use info logging to reduce spam in log files
            Logger.SetLogLevel(nameof(DoubleLanguagesModule), LogLevel.Info);
#endif
        }

        public override void Load() {
            On.Celeste.GameLoader.LoadThread += loadLanguage;
            MainMenu.Load();
            Hooks.Dialog.Unload();
        }

        public override void Unload()
        {
            MainMenu.Unload();
            Hooks.Dialog.Unload();
        }

        private void loadLanguage(On.Celeste.GameLoader.orig_LoadThread orig, GameLoader loader)
        {
            orig(loader);
            Language = Dialog.Languages[Settings.SecondLanguage];
            Fonts.Load(Language.FontFace);
        }
    }
}