using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;


namespace Celeste.Mod.DoubleLanguages {
    public class DoubleLanguagesModule : EverestModule {
        public static DoubleLanguagesModule Instance { get; private set; }

        public override Type SettingsType => typeof(DoubleLanguagesModuleSettings);
        public static DoubleLanguagesModuleSettings Settings => (DoubleLanguagesModuleSettings) Instance._Settings;

        public DoubleLanguagesModule() {
            Instance = this;
#if DEBUG
            // debug builds use verbose logging
            Logger.SetLogLevel(nameof(DoubleLanguagesModule), LogLevel.Verbose);
#else
            // release builds use info logging to reduce spam in log files
            Logger.SetLogLevel(nameof(DoubleLanguagesModule), LogLevel.Info);
#endif
        }

        public override void Load() {
            On.Celeste.Textbox.Say += customSay;
            On.Celeste.GameLoader.LoadThread += loadCustomFonts;
        }

        public override void Unload() {
            On.Celeste.Textbox.Say -= customSay;
        }

        private void loadCustomFonts(On.Celeste.GameLoader.orig_LoadThread orig, GameLoader loader)
        {
            orig(loader);
            Fonts.Load(Dialog.Languages[Settings.SecondLanguage].FontFace);
        }
        
        private static IEnumerator customSay(On.Celeste.Textbox.orig_Say orig, string dialog, params Func<IEnumerator>[] events)
        {
            if (Settings.Enabled)
            {
                Textbox textbox = new Textbox(dialog, Dialog.Languages[Settings.SecondLanguage], events);
                Engine.Scene.Add((Entity) textbox);
                while (textbox.Opened)
                    yield return (object) null;
            }
            
            yield return new SwapImmediately(orig(dialog, events));
        }
    }
}