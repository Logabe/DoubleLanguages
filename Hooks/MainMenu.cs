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
            On.Celeste.MainMenuClimb.ctor += climbButtonData;
            On.Celeste.MainMenuClimb.Render += climbButtonRender;
                
            On.Celeste.MainMenuSmallButton.ctor += menuButtonData;
            On.Celeste.MainMenuSmallButton.Render += menuButtonRender;
        }

        public static void Unload()
        {
            On.Celeste.MainMenuClimb.ctor -= climbButtonData;
            On.Celeste.MainMenuClimb.Render -= climbButtonRender;
            
            On.Celeste.MainMenuSmallButton.ctor -= menuButtonData;
            On.Celeste.MainMenuSmallButton.Render -= menuButtonRender;
        }
        
        private static void climbButtonData(On.Celeste.MainMenuClimb.orig_ctor orig, MainMenuClimb self, Oui oui, Vector2 targetposition, Vector2 tweenfrom, Action onconfirm)
        {
            orig(self, oui, targetposition, tweenfrom, onconfirm);
            self.label = DoubleLanguages.Translate("menu_begin");
        }

        private static void climbButtonRender(On.Celeste.MainMenuClimb.orig_Render orig, MainMenuClimb self)
        {
            Vector2 vector2_1 = new Vector2(0.0f, self.bounceWiggler.Value * 8f);
            Vector2 vector2_2 = Vector2.UnitY * (float) self.icon.Height + new Vector2(0.0f, -Math.Abs(self.bigBounceWiggler.Value * 40f));
            if (!self.confirmed)
                vector2_2 += vector2_1;
            self.icon.DrawOutlineJustified(self.Position + vector2_2, new Vector2(0.5f, 1f), Color.White, 1f, (float) ((double) self.rotateWiggler.Value * 10.0 * (Math.PI / 180.0)));
            DrawOutline(self.label, self.Position + vector2_1 + new Vector2(0.0f, (float) (48 + self.icon.Height)), new Vector2(0.5f, 0.5f), Vector2.One * 1.5f * self.labelScale, self.SelectionColor, 2f, Color.Black);
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
                DrawOutline(data.translatedString, button.Position + vector2 + new Vector2(84f, 0.0f), new Vector2(0.0f, 0.5f), Vector2.One * button.labelScale, button.SelectionColor,  2f, Color.Black);
            }
        }

        private static void DrawOutline(
            string text,
            Vector2 position,
            Vector2 justify,
            Vector2 scale,
            Color color,
            float stroke,
            Color strokeColor)
        {
            DoubleLanguages.Font.Draw(ActiveFont.BaseSize, text, position, justify, scale, color, 0.0f, Color.Transparent, stroke, strokeColor);
        }
    }
}