using System;
using System.Collections;
using Monocle;

namespace Celeste.Mod.DoubleLanguages.Hooks
{
    public static class Dialog
    {
        public static void Load()
        {
            On.Celeste.Textbox.Say += customSay;
        }

        public static void Unload()
        {
            On.Celeste.Textbox.Say -= customSay;
        }
        
        private static IEnumerator customSay(On.Celeste.Textbox.orig_Say orig, string dialog, params Func<IEnumerator>[] events)
        {
            if (DoubleLanguages.Settings.Enabled)
            {
                Textbox textbox = new Textbox(dialog, DoubleLanguages.Language, events);
                Engine.Scene.Add((Entity) textbox);
                while (textbox.Opened)
                    yield return (object) null;
            }
            
            yield return new SwapImmediately(orig(dialog, events));
        }
    }
}