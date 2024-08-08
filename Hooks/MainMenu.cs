using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;

namespace Celeste.Mod.DoubleLanguages.Hooks
{
    public static class MainMenu
    {
        public static void Load()
        {
            On.Celeste.MainMenuSmallButton.ctor += menuButtonData;
            On.Celeste.MainMenuSmallButton.Render += menuButtonRender;
        }

        public static void Unload()
        {
            On.Celeste.MainMenuSmallButton.ctor -= menuButtonData;
            On.Celeste.MainMenuSmallButton.Render -= menuButtonRender;
        }
        
        private static void menuButtonData(On.Celeste.MainMenuSmallButton.orig_ctor orig, MainMenuSmallButton self, string labelName, string iconName, Oui oui, Vector2 targetPosition, Vector2 tweenFrom, Action onConfirm)
        {
            orig(self, labelName, iconName, oui, targetPosition, tweenFrom, onConfirm);
            self.Components.Add(new TranslationData(labelName));
        }
        
        private static void menuButtonRender(On.Celeste.MainMenuSmallButton.orig_Render orig, MainMenuSmallButton button)
        {
            float scale = 64f / (float) button.icon.Width;
            Vector2 vector2 = new Vector2(Monocle.Ease.CubeInOut(button.ease) * 32f, (float) ((double) ActiveFont.LineHeight / 2.0 + (double) button.wiggler.Value * 8.0));
            button.icon.DrawOutlineJustified(button.Position + vector2, new Vector2(0.0f, 0.5f), Color.White, scale);
            if (button.selected) {
                ActiveFont.DrawOutline(button.label, button.Position + vector2 + new Vector2(84f, 0.0f), new Vector2(0.0f, 0.5f), Vector2.One * button.labelScale, button.SelectionColor, 2f, Color.Black);
            }
            else
            {
                TranslationData data = button.Components.Get<TranslationData>();
                DoubleLanguages.Font.Draw(ActiveFont.BaseSize, data.translatedString, button.Position + vector2 + new Vector2(84f, 0.0f), new Vector2(0.0f, 0.5f), Vector2.One * button.labelScale, button.SelectionColor, 0.0f, Color.Transparent, 2f, Color.Black);
            }
        }
    }
}